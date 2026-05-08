using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CV_app
{
    public partial class ImageView : Form
    {
        private string filePath;
        private Image newbitmap;
        private List<string> imagelist;
        private string imgPath;
        private Database database = new Database();

        public ImageView()
        {
            InitializeComponent();
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            ShowDirectory();
        }

        public static List<string> GetImgCollection(string path)
        {
            try
            {
                string[] imgarray = Directory.GetFiles(path);
                var result = from imgstring in imgarray
                             where imgstring.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                             imgstring.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                             imgstring.EndsWith("bmp", StringComparison.OrdinalIgnoreCase)
                             select imgstring;
                return result.ToList();
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("获取图像列表失败: " + path + " - " + ex.Message);
                return new List<string>();
            }
        }

        private int GetIndex(string imagepath)
        {
            int index = 0;
            for (int i = 0; i < imagelist.Count; i++)
            {
                if (imagelist[i].Equals(imagepath))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void SwitchImg(int index)
        {
            try
            {
                newbitmap = Image.FromFile(imagelist[index]);
                picBoxView.Image = newbitmap;
                imgPath = imagelist[index];
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("加载图像失败: " + imagelist[index] + " - " + ex.Message);
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            try
            {
                int index = GetIndex(imgPath);
                if (null != newbitmap)
                {
                    newbitmap.Dispose();
                }

                if (index == 0)
                    SwitchImg(imagelist.Count - 1);
                else
                    SwitchImg(index - 1);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("切换图像失败: " + ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int index = GetIndex(imgPath);
                if (null != newbitmap)
                {
                    newbitmap.Dispose();
                }

                if (index != imagelist.Count - 1)
                    SwitchImg(index + 1);
                else
                    SwitchImg(0);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("切换图像失败: " + ex.Message);
            }
        }

        private void ImageView_Load(object sender, EventArgs e)
        {
            listView1.Dock = DockStyle.Fill;
            ShowDirectory();
        }

        public void ShowDirectory()
        {
            listView1.Clear();
            listView1.Show();
            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            IntPtr hSysImgList = NativeMethods.SHGetFileInfo("",
                                                             0,
                                                             ref shfi,
                                                             (uint)Marshal.SizeOf(shfi),
                                                             NativeMethods.SHGFI_SYSICONINDEX
                                                              | NativeMethods.SHGFI_LARGEICON);
            Debug.Assert(hSysImgList != IntPtr.Zero);

            IntPtr hOldImgList = NativeMethods.SendMessage(listView1.Handle,
                                                           NativeMethods.LVM_SETIMAGELIST,
                                                           NativeMethods.LVSIL_SMALL,
                                                           hSysImgList);

            if (hOldImgList != IntPtr.Zero)
            {
                NativeMethods.ImageList_Destroy(hOldImgList);
            }

            listView1.View = View.Details;
            listView1.Columns.Add("Name", 500);
            NativeMethods.SetWindowTheme(listView1.Handle, "Explorer", null);

            string basePath = database.getValue("imagespath");
            if (string.IsNullOrEmpty(basePath) || !Directory.Exists(basePath))
            {
                GlobalVar.log?.AppandText("图像目录不存在: " + basePath);
                return;
            }

            string[] s = Directory.GetFileSystemEntries(basePath);
            foreach (string file in s)
            {
                IntPtr himl = NativeMethods.SHGetFileInfo(file,
                                                          0,
                                                          ref shfi,
                                                          (uint)Marshal.SizeOf(shfi),
                                                          NativeMethods.SHGFI_DISPLAYNAME
                                                            | NativeMethods.SHGFI_SYSICONINDEX
                                                            | NativeMethods.SHGFI_LARGEICON);
                Debug.Assert(himl == hSysImgList);
                listView1.Items.Add(shfi.szDisplayName, shfi.iIcon);
            }
        }

        internal static class NativeMethods
        {
            public const uint LVM_FIRST = 0x1000;
            public const uint LVM_GETIMAGELIST = (LVM_FIRST + 2);
            public const uint LVM_SETIMAGELIST = (LVM_FIRST + 3);

            public const uint LVSIL_NORMAL = 0;
            public const uint LVSIL_SMALL = 1;
            public const uint LVSIL_STATE = 2;
            public const uint LVSIL_GROUPHEADER = 3;

            [DllImport("user32")]
            public static extern IntPtr SendMessage(IntPtr hWnd,
                                                    uint msg,
                                                    uint wParam,
                                                    IntPtr lParam);

            [DllImport("comctl32")]
            public static extern bool ImageList_Destroy(IntPtr hImageList);

            public const uint SHGFI_DISPLAYNAME = 0x200;
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0;
            public const uint SHGFI_SMALLICON = 0x1;
            public const uint SHGFI_SYSICONINDEX = 0x4000;

            [StructLayout(LayoutKind.Sequential)]
            public struct SHFILEINFO
            {
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            };

            [DllImport("shell32")]
            public static extern IntPtr SHGetFileInfo(string pszPath,
                                                      uint dwFileAttributes,
                                                      ref SHFILEINFO psfi,
                                                      uint cbSizeFileInfo,
                                                      uint uFlags);

            [DllImport("uxtheme", CharSet = CharSet.Unicode)]
            public static extern int SetWindowTheme(IntPtr hWnd,
                                                    string pszSubAppName,
                                                    string pszSubIdList);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string sfolder = listView1.SelectedItems[0].Text;
            string basePath = database.getValue("imagespath");
            if (string.IsNullOrEmpty(basePath))
            {
                GlobalVar.log?.AppandText("图像路径未配置");
                return;
            }
            filePath = Path.Combine(basePath, sfolder);
            imagelist = GetImgCollection(filePath);
            listView1.Hide();
        }
    }
}
