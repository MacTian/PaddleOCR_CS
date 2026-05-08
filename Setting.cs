using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using GxIAPINET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace CV_app
{
    public partial class Setting : Form
    {
        private Database database = new Database();
        private bool m_bTriggerMode = false;
        private bool m_bTriggerSource = false;
        private bool m_bTriggerActive = false;

        public Setting()
        {
            InitializeComponent();
        }

        public void BuildDataGridView()
        {
            dgvCCDParams.Rows.Clear();

            foreach (var appParamLaser in GlobalVar.ZAppParam_CCDs)
            {
                int index = dgvCCDParams.Rows.Add();
                dgvCCDParams.Rows[index].Cells[0].Value = appParamLaser.Value.id;
                dgvCCDParams.Rows[index].Cells[1].Value = appParamLaser.Value.name;
                dgvCCDParams.Rows[index].Cells[2].Value = appParamLaser.Value.ename;
                dgvCCDParams.Rows[index].Cells[3].Value = appParamLaser.Value.value;
                dgvCCDParams.Rows[index].Cells[4].Value = appParamLaser.Value.setting;
                dgvCCDParams.Rows[index].Cells[5].Value = appParamLaser.Value.note;
                dgvCCDParams.Rows[index].Cells[6].Value = appParamLaser.Value.type;
                appParamLaser.Value.index = index;
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            GlobalVar.log?.AppandText("加载设置");
            tbpRecipe.Parent = null;
            BuildDataGridView();
        }

        private void Setting_Shown(object sender, EventArgs e)
        {
            if (null != GlobalVar.camera)
            {
                __InitEnumComBoxUI(m_cb_TriggerMode, "TriggerMode", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerMode);
                __InitEnumComBoxUI(m_cb_TriggerSource, "TriggerSource", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerSource);
                __InitEnumComBoxUI(m_cb_TriggerActivation, "TriggerActivation", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerActive);
                __InitImageProcess();
                __InitShutterUI();
                __InitGainUI();
                __InitOutput();
                txtCaptureDelay.Enabled = true;
                txtCaptureDelay.Text = database.getValue("capturedelay");
            }
            GlobalVar.log?.AppandText("加载设置完成");
        }

        private void m_cb_TriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerMode.Text;
                __SetEnumValue("TriggerMode", strValue, GlobalVar.camera.m_objIGXFeatureControl);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置触发模式失败: " + ex.Message);
            }
        }

        private void m_cb_TriggerSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerSource.Text;
                __SetEnumValue("TriggerSource", strValue, GlobalVar.camera.m_objIGXFeatureControl);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置触发源失败: " + ex.Message);
            }
        }

        private void m_btn_SoftTriggerCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
                }
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("软触发失败: " + ex.Message);
            }
        }

        private void m_cb_TriggerActivation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerActivation.Text;
                __SetEnumValue("TriggerActivation", strValue, GlobalVar.camera.m_objIGXFeatureControl);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置触发极性失败: " + ex.Message);
            }
        }

        private void __SetEnumValue(string strFeatureName, string strValue, IGXFeatureControl objIGXFeatureControl)
        {
            if (null != objIGXFeatureControl)
            {
                objIGXFeatureControl.GetEnumFeature(strFeatureName).SetValue(strValue);
            }
        }

        private void cmbReversX_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool bValue = bool.Parse(cmbReverseX.Text);
                GlobalVar.camera.m_objIGXFeatureControl.GetBoolFeature("ReverseX").SetValue(bValue);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置ReverseX失败: " + ex.Message);
            }
        }

        private void cmbReverseY_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool bValue = bool.Parse(cmbReverseY.Text);
                GlobalVar.camera.m_objIGXFeatureControl.GetBoolFeature("ReverseY").SetValue(bValue);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置ReverseY失败: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetSelector").SetValue("UserSet0");
                GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("UserSetSave").Execute();
                GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetDefault").SetValue("UserSet0");
                GlobalVar.log?.AppandText("保存相机设置成功");
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("保存相机设置失败: " + ex.Message);
            }
        }

        private void btnCalib_Click(object sender, EventArgs e)
        {
            try
            {
                string laserox, laseroy, laseroa, laserob;
                string[] info = database.getValue("mark_data").Split('$');
                laserox = info[1];
                laseroy = info[2];
                laseroa = info[3];
                laserob = info[4];
                database.setValue("laserox", laserox);
                database.setValue("laseroy", laseroy);
                database.setValue("laseroa", laseroa);
                database.setValue("laserob", laserob);
                Mat src = GlobalVar.camera.matImage;
                CvInvoke.CvtColor(src, src, ColorConversion.Bgr2Gray);
                CircleF[] circles = CvInvoke.HoughCircles(src, HoughModes.Gradient, 2, 50);
                CvInvoke.CvtColor(src, src, ColorConversion.Gray2Bgr);
                foreach (CircleF circle in circles)
                {
                    CvInvoke.Circle(src, Point.Round(circle.Center), (int)circle.Radius, new MCvScalar(0, 255, 0), 2);
                }

                if (circles.Count() != 1)
                {
                    MessageBox.Show("未检测到校准圆，请优化调试图像");
                    GlobalVar.log?.AppandText("校准失败");
                    return;
                }
                double visionox = circles[0].Center.X;
                double visionoy = circles[0].Center.Y;
                double visionor = circles[0].Radius;
                double dlaseroa = Convert.ToDouble(database.getValue("laseroa"));
                double scale = visionor / (dlaseroa / 2);

                database.setValue("visionox", visionox.ToString());
                database.setValue("visionoy", visionoy.ToString());
                database.setValue("visionor", visionor.ToString());
                database.setValue("scale", scale.ToString());
                Point txtLocation = new Point((int)(circles[0].Center.X - circles[0].Radius), (int)(circles[0].Center.Y - circles[0].Radius));
                MCvScalar txtColor = new MCvScalar(0, 0, 255);
                CvInvoke.PutText(src, "Calib Success!", txtLocation, FontFace.HersheySimplex, 1, txtColor, 2);
                pictureBox1.Image = src.ToBitmap();
                GlobalVar.log?.AppandText("校准成功");
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("校准异常: " + ex.Message);
            }
        }

        private void chkOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutput.Checked)
            {
                GlobalVar.camera.outPut = "true";
                database.setValue("output", "true");
            }
            else
            {
                GlobalVar.camera.outPut = "false";
                database.setValue("output", "false");
                GlobalVar.camera.resultOutput(false);
            }
        }

        private void m_txt_Shutter_Leave(object sender, EventArgs e)
        {
            double dShutterValue = 0.0;
            double dMin = 0.0;
            double dMax = 0.0;

            try
            {
                try
                {
                    dShutterValue = Convert.ToDouble(m_txt_Shutter.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("请输入正确的曝光时间");
                    return;
                }

                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
                    dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
                    if (dShutterValue > dMax) dShutterValue = dMax;
                    if (dShutterValue < dMin) dShutterValue = dMin;

                    m_txt_Shutter.Text = dShutterValue.ToString("F2");
                    GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(dShutterValue);
                }
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置曝光失败: " + ex.Message);
            }
        }

        private void m_txt_Gain_Leave(object sender, EventArgs e)
        {
            double dGain = 0;
            double dMin = 0.0;
            double dMax = 0.0;
            try
            {
                try
                {
                    dGain = Convert.ToDouble(m_txt_Gain.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("请输入正确的增益值");
                    return;
                }

                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMin();
                    dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMax();
                    if (dGain > dMax) dGain = dMax;
                    if (dGain < dMin) dGain = dMin;

                    m_txt_Gain.Text = dGain.ToString("F2");
                    GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").SetValue(dGain);
                }
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置增益失败: " + ex.Message);
            }
        }

        private void chkImageProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImageProcess.Checked)
            {
                database.setValue("imageprocess", "true");
                grpImageProcess.Enabled = true;
                rdbtnBinary.Enabled = true;
                rdbtnBinaryInv.Enabled = true;
                txtBlocksize.Enabled = true;
                txtFilterC.Enabled = true;
                txtErodeX.Enabled = true;
                txtErodeY.Enabled = true;
                txtDilateX.Enabled = true;
                txtDilateY.Enabled = true;
                btnPreview.Enabled = true;
            }
            else
            {
                database.setValue("imageprocess", "false");
                grpImageProcess.Enabled = false;
                rdbtnBinary.Enabled = false;
                rdbtnBinaryInv.Enabled = false;
                txtBlocksize.Enabled = false;
                txtFilterC.Enabled = false;
                txtErodeX.Enabled = false;
                txtErodeY.Enabled = false;
                txtDilateX.Enabled = false;
                txtDilateY.Enabled = false;
                btnPreview.Enabled = false;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                database.setValue("binary", rdbtnBinary.Checked ? "true" : "false");
                database.setValue("binaryinverse", rdbtnBinaryInv.Checked ? "true" : "false");
                database.setValue("blocksize", txtBlocksize.Text);
                database.setValue("filtersize", txtFilterC.Text);
                database.setValue("erodex", txtErodeX.Text);
                database.setValue("erodey", txtErodeY.Text);
                database.setValue("dilatex", txtDilateX.Text);
                database.setValue("dilatey", txtDilateY.Text);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("保存图像处理参数失败: " + ex.Message);
                return;
            }
            try
            {
                ImageProcess imageProcess = new ImageProcess();
                Bitmap bitmap = (Bitmap)pictureBox1.Image;
                Image<Bgr, Byte> image = bitmap.ToImage<Bgr, Byte>();
                Mat matImage = image.Mat;
                imageProcess.process(matImage);
                CvInvoke.Imshow("Preview", matImage);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("图像预览失败: " + ex.Message);
            }
        }

        private void __InitEnumComBoxUI(ComboBox cbEnum, string strFeatureName, IGXFeatureControl objIGXFeatureControl, ref bool bIsImplemented)
        {
            string strTriggerValue = "";
            List<string> list = new List<string>();
            bool bIsReadable = false;
            if (null != objIGXFeatureControl)
            {
                bIsImplemented = objIGXFeatureControl.IsImplemented(strFeatureName);
                if (!bIsImplemented) return;

                bIsReadable = objIGXFeatureControl.IsReadable(strFeatureName);
                if (bIsReadable)
                {
                    list.AddRange(objIGXFeatureControl.GetEnumFeature(strFeatureName).GetEnumEntryList());
                    strTriggerValue = objIGXFeatureControl.GetEnumFeature(strFeatureName).GetValue();
                }
            }

            cbEnum.Items.Clear();
            foreach (string str in list)
            {
                cbEnum.Items.Add(str);
            }

            for (int i = 0; i < cbEnum.Items.Count; i++)
            {
                string strTemp = cbEnum.Items[i].ToString();
                if (strTemp == strTriggerValue)
                {
                    cbEnum.SelectedIndex = i;
                    break;
                }
            }
        }

        private void __InitShutterUI()
        {
            double dCurShuter = 0.0;
            double dMin = 0.0;
            double dMax = 0.0;
            string strUnit = "";

            if (null != GlobalVar.camera.m_objIGXFeatureControl)
            {
                dCurShuter = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetValue();
                dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
                dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
                strUnit = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetUnit();
            }

            string strText = string.Format("曝光时间({0}~{1}){2}", dMin.ToString("0.00"), dMax.ToString("0.00"), strUnit);
            m_lbl_Shutter.Text = strText;
            m_txt_Shutter.Text = dCurShuter.ToString("0.00");
        }

        private void __InitGainUI()
        {
            double dCurGain = 0;
            double dMin = 0.0;
            double dMax = 0.0;
            string strUnit = "";

            if (null != GlobalVar.camera.m_objIGXFeatureControl)
            {
                dCurGain = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetValue();
                dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMin();
                dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMax();
                strUnit = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetUnit();
            }

            string strText = string.Format("增益({0}~{1}){2}", dMin.ToString("0.00"), dMax.ToString("0.00"), strUnit);
            m_lbl_Gain.Text = strText;
            m_txt_Gain.Text = dCurGain.ToString("0.00");
        }

        private void __InitOutput()
        {
            chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Checked = Convert.ToBoolean(database.getValue("output"))));
        }

        private void __InitImageProcess()
        {
            chkImageProcess.Checked = Convert.ToBoolean(database.getValue("imageprocess"));
            grpImageProcess.Enabled = chkImageProcess.Checked;

            rdbtnBinary.Checked = Convert.ToBoolean(database.getValue("binary"));
            rdbtnBinaryInv.Checked = Convert.ToBoolean(database.getValue("binaryinverse"));
            txtBlocksize.Text = database.getValue("blocksize");
            txtFilterC.Text = database.getValue("filtersize");
            txtErodeX.Text = database.getValue("erodex");
            txtErodeY.Text = database.getValue("erodey");
            txtDilateX.Text = database.getValue("dilatex");
            txtDilateY.Text = database.getValue("dilatey");
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "Model1.bmp");
                CvInvoke.Imwrite(fName, GlobalVar.camera.matImage);
                GlobalVar.log?.AppandText("保存模型: " + fName);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("保存模型失败: " + ex.Message);
            }
        }

        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            try
            {
                string ImageModel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "Model1.bmp");
                Image m = Image.FromFile(ImageModel);
                pictureBox1.Image = m;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("加载模型失败: " + ex.Message);
            }
        }

        Rectangle m_draw_rect;
        bool m_bDraw, m_enable = false, m_pattern = false, m_searchregion = false, m_ocrregion = false;
        Point m_oldPt, m_newPt;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_enable)
            {
                m_oldPt = e.Location;
                m_bDraw = true;
                Invalidate();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            m_newPt = e.Location;
            if (e.Button != MouseButtons.Left) return;
            if (m_bDraw)
            {
                int x = m_oldPt.X > m_newPt.X ? m_newPt.X : m_oldPt.X;
                int y = m_oldPt.Y > m_newPt.Y ? m_newPt.Y : m_oldPt.Y;
                int width = Math.Abs(m_oldPt.X - m_newPt.X);
                int height = Math.Abs(m_oldPt.Y - m_newPt.Y);
                m_draw_rect = new Rectangle(x, y, width, height);
                pictureBox1.Invalidate();
            }
        }

        private void btnCreatePattern_Click(object sender, EventArgs e)
        {
            m_enable = true;
            m_pattern = true;
            GlobalVar.log?.AppandText("创建特征");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null && m_enable)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 3), m_draw_rect);
            }
        }

        private void btnFinishSet_Click(object sender, EventArgs e)
        {
            GlobalVar.log?.AppandText("设置结束");
            this.Close();
        }

        private void btnSearchRegion_Click(object sender, EventArgs e)
        {
            m_enable = true;
            m_searchregion = true;
        }

        private void btnOCRRegion_Click(object sender, EventArgs e)
        {
            m_enable = true;
            m_ocrregion = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_enable)
            {
                m_newPt = e.Location;
                m_bDraw = false;
                if (MessageBox.Show("确认", "Tips", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (m_pattern)
                    {
                        m_pattern = false;
                        m_enable = false;
                        savePattern();
                    }

                    if (m_searchregion)
                    {
                        m_searchregion = false;
                        m_enable = false;
                        saveSearchRegion();
                    }

                    if (m_ocrregion)
                    {
                        m_ocrregion = false;
                        m_enable = false;
                        saveOCRRegion();
                    }
                }
            }
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != GlobalVar.camera)
            {
                try
                {
                    GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetSelector").SetValue("UserSet0");
                    GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("UserSetSave").Execute();
                    GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetDefault").SetValue("UserSet0");
                }
                catch (Exception ex)
                {
                    GlobalVar.log?.AppandText("关闭设置时保存失败: " + ex.Message);
                }
            }

            GlobalVar.frmSetting = null;
            Parameters parameters = new Parameters();
            parameters.initialParameters();
            GlobalVar.log?.AppandText("关闭设置");
        }

        private void dgvCCDParams_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dv = sender as DataGridView;
            int index = e.RowIndex;
            if (dv.SelectedRows == null || dv.SelectedRows.Count == 0) return;

            int dgvX = this.Location.X + dv.Location.X;
            int dgvY = this.Location.Y + tabControl1.ItemSize.Height + dv.Location.Y + (this.Height - this.ClientSize.Height);

            int _cellY = dv.SelectedRows[0].Cells[4].RowIndex;
            Rectangle rect = dv.GetCellDisplayRectangle(3, _cellY, true);
            int x = dgvX + rect.X;
            int y = dgvY + rect.Y + rect.Height;
            frmEditText frm = new frmEditText();
            frm.Location = new Point(x, y);
            frm.id = dv.SelectedRows[0].Cells[0].Value.ToString();
            frm.Text = dv.SelectedRows[0].Cells[1].Value.ToString();
            frm.ename = dv.SelectedRows[0].Cells[2].Value.ToString();
            frm.value = dv.SelectedRows[0].Cells[3].Value.ToString();
            frm.type = dv.SelectedRows[0].Cells[6].Value.ToString();
            frm.setting = dv.SelectedRows[0].Cells[4].Value.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                BuildDataGridView();
                dv.Rows[index].Selected = true;
            }
        }

        private void txtCaptureDelay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCaptureDelay.Text != null)
                {
                    database.setValue("capturedelay", txtCaptureDelay.Text);
                    GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(Convert.ToInt32(txtCaptureDelay.Text) * 1000);
                }
                GlobalVar.log?.AppandText("修改拍照延时");
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("设置拍照延时失败: " + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                database.setValue("readqr", "true");
                string detProto = database.getValue("detproto");
                string detModel = database.getValue("detmodel");
                string supRsProto = database.getValue("suprsproto");
                string supRsModel = database.getValue("suprsmodel");
                bool isDetProtoExists = File.Exists(detProto);
                if (!isDetProtoExists) MessageBox.Show("模型文件不存在:" + detProto);
                bool isDetModelExists = File.Exists(detModel);
                if (!isDetModelExists) MessageBox.Show("模型文件不存在:" + detModel);
                bool isSRProtoExists = File.Exists(supRsProto);
                if (!isSRProtoExists) MessageBox.Show("模型文件不存在:" + supRsProto);
                bool isSRModelExists = File.Exists(supRsModel);
                if (!isSRModelExists) MessageBox.Show("Keys文件不存在:" + supRsModel);
                if (isDetProtoExists && isDetModelExists && isSRProtoExists && isSRModelExists)
                {
                    GlobalVar.qRCode = new WeChatQRCode(detProto, detModel, supRsProto, supRsModel);
                }
                else
                {
                    MessageBox.Show("初始化失败，请确认解码模型文件夹和文件后，重新初始化！");
                }
            }
            else
                database.setValue("readqr", "false");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tbpAllParams")
                BuildDataGridView();
        }

        private void btnCreatePattern_MouseEnter(object sender, EventArgs e)
        {
            ShowTipImage(Properties.Resources.selectpattern);
        }

        private void btnSearchRegion_MouseEnter(object sender, EventArgs e)
        {
            ShowTipImage(Properties.Resources.selectregion);
        }

        private void btnOCRRegion_MouseEnter(object sender, EventArgs e)
        {
            ShowTipImage(Properties.Resources.selectocr);
        }

        private void ShowTipImage(Image img)
        {
            if (picTips.Image != null)
            {
                picTips.Image.Dispose();
                picTips.Image = null;
            }
            MemoryStream mstr = new MemoryStream();
            img.Save(mstr, ImageFormat.Gif);
            picTips.Image = Image.FromStream(mstr);
            img.Dispose();
        }

        int px, py, pw, ph;

        private void savePattern()
        {
            ConvertCoordinates(pictureBox1, out px, out py, m_draw_rect.X, m_draw_rect.Y);
            ConvertCoordinates(pictureBox1, out pw, out ph, m_draw_rect.Width, m_draw_rect.Height);
            Rectangle roi = new Rectangle(px, py, pw, ph);
            Bitmap img = (Bitmap)pictureBox1.Image;
            Image<Bgr, Byte> image = img.ToImage<Bgr, byte>();
            Mat pattern = new Mat(image.Mat, roi);
            string fName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "Pattern1.bmp");
            CvInvoke.Imwrite(fName, pattern);
            GlobalVar.log?.AppandText("保存特征:" + fName);
        }

        private void saveSearchRegion()
        {
            int x, y, w, h;
            ConvertCoordinates(pictureBox1, out x, out y, m_draw_rect.X, m_draw_rect.Y);
            ConvertCoordinates(pictureBox1, out w, out h, m_draw_rect.Width, m_draw_rect.Height);
            database.setValue("xstart", x.ToString());
            database.setValue("ystart", y.ToString());
            database.setValue("swidth", w.ToString());
            database.setValue("sheight", h.ToString());
            GlobalVar.log?.AppandText("修改搜索区域");
        }

        private void saveOCRRegion()
        {
            int x, y, w, h;
            ConvertCoordinates(pictureBox1, out x, out y, m_draw_rect.X, m_draw_rect.Y);
            ConvertCoordinates(pictureBox1, out w, out h, m_draw_rect.Width, m_draw_rect.Height);
            int Xoffset = px - x;
            int Yoffset = py - y;
            database.setValue("xoffset", Xoffset.ToString());
            database.setValue("yoffset", Yoffset.ToString());
            database.setValue("woffset", w.ToString());
            database.setValue("hoffset", h.ToString());
            GlobalVar.log?.AppandText("修改OCR区域");
        }

        private static void ConvertCoordinates(PictureBox pic, out int X0, out int Y0, int x, int y)
        {
            int pic_hgt = pic.ClientSize.Height;
            int pic_wid = pic.ClientSize.Width;
            int img_hgt = pic.Image.Height;
            int img_wid = pic.Image.Width;

            X0 = x;
            Y0 = y;
            switch (pic.SizeMode)
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.StretchImage:
                    X0 = (int)(img_wid * x / (float)pic_wid);
                    Y0 = (int)(img_hgt * y / (float)pic_hgt);
                    break;
            }
        }
    }
}
