using System;
using System.Drawing;
using System.Windows.Forms;

namespace CV_app
{
    public partial class Main : Form
    {
        private Database database = new Database();

        public Main()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (null != GlobalVar.camera)
            {
                try
                {
                    GlobalVar.camera.releaseCam();
                }
                catch (Exception ex)
                {
                    GlobalVar.log?.AppandText("关闭相机失败: " + ex.Message);
                }
            }
            GlobalVar.log?.AppandText("关闭程序");
            Environment.Exit(0);
        }

        private void btnImageView_Click(object sender, EventArgs e)
        {
            ImageView f = new ImageView();
            f.ShowDialog();
            GlobalVar.log?.AppandText("打开图像记录");
        }

        private void chkOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutput.Checked)
            {
                database.setValue("output", "true");
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Text = "启用输出"));
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Image = Properties.Resources.enablealarm));
                GlobalVar.log?.AppandText("启用输出");
            }
            else
            {
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Text = "禁用输出"));
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Image = Properties.Resources.disablealarm));
                database.setValue("output", "false");
                GlobalVar.camera.resultOutput(false);
                GlobalVar.log?.AppandText("禁用输出");
            }
        }

        private void chkConnectCam_CheckedChanged(object sender, EventArgs e)
        {
            if (null == GlobalVar.camera)
            {
                GlobalVar.camera = new Camera();
            }

            if (chkConnectCam.Checked)
            {
                if (GlobalVar.camera.initialCamera())
                {
                    chkConnectCam.BackColor = Color.Lime;
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.online));
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "联机"));
                    GlobalVar.log?.AppandText("连接相机");
                }
                else
                {
                    chkConnectCam.BackColor = Color.Red;
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.offline));
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "脱机"));
                    GlobalVar.log?.AppandText("断开相机");
                }
            }
            else
            {
                chkConnectCam.BackColor = Color.Red;
                chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.offline));
                chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "脱机"));
                if (null != GlobalVar.camera)
                {
                    GlobalVar.camera.releaseCam();
                }
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            GlobalVar.log?.AppandText("打开设置页");
            GlobalVar.frmSetting = new Setting();
            GlobalVar.frmSetting.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GlobalVar.camera.resultOutput(false);
            GlobalVar.iCount = 0;
            GlobalVar.iNG = 0;
            GlobalVar.camera.arrOutvalue = new System.Collections.Generic.List<int>();
            GlobalVar.camera.updateData();
            GlobalVar.log?.AppandText("清除数据");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string fPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            GlobalVar.log = new Tool.Log(fPath);
            GlobalVar.log?.AppandText("运行程序");
            GlobalVar.ZAppParam_CCDs = ZDatabase.Instance().GetParams<AppParam_CCD>("app_ccd");
            Parameters parameters = new Parameters();
            parameters.initialParameters();
            GlobalVar.log?.AppandText("初始化完成");
        }
    }
}
