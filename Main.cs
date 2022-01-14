using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

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
            try
            {
               GlobalVar.camera.releaseCam();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Environment.Exit(0);
        }


        private void btnImageView_Click(object sender, EventArgs e)
        {
            ImageView f = new ImageView();
            f.ShowDialog();
        }

        private void chkOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutput.Checked)
            {
                //outPut = "true";
                database.setValue("output", "true");
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Text = "启用输出"));
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Image = Properties.Resources.enablealarm));
            }
            else
            {
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Text = "禁用输出"));
                chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Image = Properties.Resources.disablealarm));
                //outPut = "false";
                database.setValue("output", "false");
                GlobalVar.camera.resultOutput(false);
            }
            //opini.WriteIniData("Parameters", "Output", outPut, iniFilePath);
            //readParas();
        }

        private void chkConnectCam_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkConnectCam.Checked)
            {
                if (GlobalVar.camera.initialCamera())
                {
                    chkConnectCam.BackColor = Color.Lime;
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.online));
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "联机"));
                }
                else
                {
                    chkConnectCam.BackColor = Color.Red;
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.offline));
                    chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "脱机"));
                }
            }
            else
            {
                chkConnectCam.BackColor = Color.Red;
                chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Image = Properties.Resources.offline));
                chkConnectCam.BeginInvoke(new MethodInvoker(() => chkConnectCam.Text = "脱机"));
                //releaseCam(1);
                if (null!=GlobalVar.camera)
                    GlobalVar.camera.releaseCam();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            GlobalVar.frmSetting = new Setting();
            GlobalVar.frmSetting.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GlobalVar.camera.resultOutput(false);
            GlobalVar.iCount = 0;
            GlobalVar.iNG = 0;
            GlobalVar.camera.arrOutvalue = new System.Collections.ArrayList();
            GlobalVar.camera.updateData();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            GlobalVar.camera = new Camera();
            GlobalVar.ZAppParam_CCDs = ZDatabase.Instance().GetParams<AppParam_CCD>("app_ccd");
            Parameters parameters = new Parameters();
            parameters.initialParameters();
            
        }
    }
}
