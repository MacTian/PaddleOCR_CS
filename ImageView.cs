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

        public ImageView()
        {
            InitializeComponent();
        }

        private void btnListView_Click(object sender, EventArgs e)
        {
            ShowDirectory();
        }

        // 第一步
        // 获得预览图片文件路径下的图片集合
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
            catch (Exception)
            {
                throw;
            }
        }

        // 第二步
        // 获得打开图片在图片集合中的索引
        private int GetIndex(string imagepath)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        // 切换图片的方法
        private void SwitchImg(int index)
        {
            try
            {
                newbitmap = Image.FromFile(imagelist[index]);
                picBoxView.Image = newbitmap;
                imgPath = imagelist[index];
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 第三步
        // 上一张图片
        private void btnPre_Click(object sender, EventArgs e)
        {
            try
            {
                int index = GetIndex(imgPath);
                // 释放上一张图片的资源，避免保存的时候出现ExternalException异常
                if (null != newbitmap)
                {
                    newbitmap.Dispose();
                }

                if (index == 0)
                {
                    SwitchImg(imagelist.Count - 1);
                }
                else
                {
                    SwitchImg(index - 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 下一张图片
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int index = GetIndex(imgPath);
                // 释放上一张图片的资源，避免保存的时候出现ExternalException异常            // 经常在调用Save方法的时候都会出现 一个GDI一般性错误,主要原因是文件没有被释放,当保存到原位置时,就会出现该异常,要避免这个错误就要释放图片占有的资源
                if (null != newbitmap)
                {
                    newbitmap.Dispose();
                }

                if (index != imagelist.Count - 1)
                {
                    SwitchImg(index + 1);
                }
                else
                {
                    SwitchImg(0);
                }
            }
            catch (Exception)
            {
                throw;
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
            // Obtain a handle to the system image list.
            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            IntPtr hSysImgList = NativeMethods.SHGetFileInfo("",
                                                             0,
                                                             ref shfi,
                                                             (uint)Marshal.SizeOf(shfi),
                                                             NativeMethods.SHGFI_SYSICONINDEX
                                                              | NativeMethods.SHGFI_LARGEICON);
            Debug.Assert(hSysImgList != IntPtr.Zero);  // cross our fingers and hope to succeed!

            // Set the ListView control to use that image list.
            IntPtr hOldImgList = NativeMethods.SendMessage(listView1.Handle,
                                                           NativeMethods.LVM_SETIMAGELIST,
                                                           NativeMethods.LVSIL_SMALL,
                                                           hSysImgList);

            // If the ListView control already had an image list, delete the old one.
            if (hOldImgList != IntPtr.Zero)
            {
                NativeMethods.ImageList_Destroy(hOldImgList);
            }

            // Set up the ListView control's basic properties.
            // Put it in "Details" mode, create a column so that "Details" mode will work,
            // and set its theme so it will look like the one used by Explorer.
            listView1.View = View.Details;
            listView1.Columns.Add("Name", 500);
            NativeMethods.SetWindowTheme(listView1.Handle, "Explorer", null);

            // Get the items from the file system, and add each of them to the ListView,
            // complete with their corresponding name and icon indices.
            string[] s = Directory.GetFileSystemEntries(@"D:\NG");
            foreach (string file in s)
            {
                IntPtr himl = NativeMethods.SHGetFileInfo(file,
                                                          0,
                                                          ref shfi,
                                                          (uint)Marshal.SizeOf(shfi),
                                                          NativeMethods.SHGFI_DISPLAYNAME
                                                            | NativeMethods.SHGFI_SYSICONINDEX
                                                            | NativeMethods.SHGFI_LARGEICON);
                Debug.Assert(himl == hSysImgList); // should be the same imagelist as the one we set
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

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 /* MAX_PATH */)]
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
            string sfolder = listView1.SelectedItems[0].Text; //SelectedItems.Count就是：取得值，表示SelectedItems集合的物件数目。
            filePath = "D:/NG/" + sfolder;//获取目录path
            imagelist = GetImgCollection(filePath);
            listView1.Hide();
        }
    }
}