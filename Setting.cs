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
        //Camera camera = new Camera();
        private Database database = new Database();
        private bool m_bTriggerMode = false;
        private bool m_bTriggerSource = false;
        private bool m_bTriggerActive = false;
        private bool m_bReverse = false;
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
            GlobalVar.log.AppandText("加载设置");
            //隐藏tbpRecipe
            tbpRecipe.Parent = null;
            BuildDataGridView();
        }

        private void Setting_Shown(object sender, EventArgs e)
        {
            if (null != GlobalVar.camera)
            {
                __InitEnumComBoxUI(m_cb_TriggerMode, "TriggerMode", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerMode);                      //触发模式初始化

                __InitEnumComBoxUI(m_cb_TriggerSource, "TriggerSource", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerSource);                //触发源初始化

                __InitEnumComBoxUI(m_cb_TriggerActivation, "TriggerActivation", GlobalVar.camera.m_objIGXFeatureControl, ref m_bTriggerActive);        //触发极性初始化

                __InitImageProcrss();

                __InitShutterUI();

                __InitGainUI();

                __InitOutput();
                txtCaptureDelay.Enabled = true;
                txtCaptureDelay.Text = database.getValue("capturedelay");
                //GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("LineSelector").SetValue("Line3");
                //GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("LineMode").SetValue("Output");
                Console.WriteLine("camera初始化成功");
            }
            GlobalVar.log.AppandText("加载设置完成");
        }

        private void m_cb_TriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerMode.Text;
                __SetEnumValue("TriggerMode", strValue, GlobalVar.camera.m_objIGXFeatureControl);

                // 更新界面UI
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                //MessageBox.Show(ex.Message);
            }
        }

        private void m_cb_TriggerSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerSource.Text;
                __SetEnumValue("TriggerSource", strValue, GlobalVar.camera.m_objIGXFeatureControl);

                // 更新界面UI
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                //MessageBox.Show(ex.Message);
            }
        }

        private void m_btn_SoftTriggerCommand_Click(object sender, EventArgs e)
        {
            try
            {
                //发送软触发命令
                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
                }

                // 更新界面UI
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                //MessageBox.Show(ex.Message);
            }
        }

        private void m_cb_TriggerActivation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strValue = m_cb_TriggerActivation.Text;
                __SetEnumValue("TriggerActivation", strValue, GlobalVar.camera.m_objIGXFeatureControl);

                // 更新界面UI
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                //MessageBox.Show(ex.Message);
            }
        }

        private void __SetEnumValue(string strFeatureName, string strValue, IGXFeatureControl objIGXFeatureControl)
        {
            if (null != objIGXFeatureControl)
            {
                //设置当前功能值
                objIGXFeatureControl.GetEnumFeature(strFeatureName).SetValue(strValue);
            }
        }

        private void cmbReversX_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bValue = bool.Parse(cmbReverseX.Text);
            GlobalVar.camera.m_objIGXFeatureControl.GetBoolFeature("ReverseX").SetValue(bValue);
        }

        private void cmbReverseY_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bValue = bool.Parse(cmbReverseY.Text);
            GlobalVar.camera.m_objIGXFeatureControl.GetBoolFeature("ReverseY").SetValue(bValue);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetSelector").SetValue("UserSet0");
                GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("UserSetSave").Execute();
                GlobalVar.camera.m_objIGXFeatureControl.GetEnumFeature("UserSetDefault").SetValue("UserSet0");
                //GlobalVar.camera.m_objIGXFeatureControl.GetCommandFeature("UserSetLoad").Execute();
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                throw;
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
                //Mat src = CvInvoke.Imread(stfFileName);
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
                    //Console.WriteLine("未检测到校准圆，请优化调试图像");
                    MessageBox.Show("未检测到校准圆，请优化调试图像");
                    GlobalVar.log.AppandText("校准失败");
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
                //throw new NotImplementedException();
                Point txtLocation = new Point((int)(circles[0].Center.X - circles[0].Radius), (int)(circles[0].Center.Y - circles[0].Radius));
                MCvScalar txtColor = new MCvScalar(0, 0, 255);
                CvInvoke.PutText(src, "Calib Success!", txtLocation, FontFace.HersheySimplex, 1, txtColor, 2);
                pictureBox1.Image = src.ToBitmap();
                GlobalVar.log.AppandText("校准成功");
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                throw;
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
            double dShutterValue = 0.0;              //曝光值
            double dMin = 0.0;                       //最小值
            double dMax = 0.0;                       //最大值

            try
            {
                try
                {
                    dShutterValue = Convert.ToDouble(m_txt_Shutter.Text);
                }
                catch (Exception)
                {
                    //__InitShutterUI();
                    //Console.WriteLine("请输入正确的曝光时间");
                    MessageBox.Show("请输入正确的曝光时间");
                    return;
                }

                //获取当前相机的曝光值、最小值、最大值和单位
                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
                    dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
                    //判断输入值是否在曝光时间的范围内
                    //若大于最大值则将曝光值设为最大值
                    if (dShutterValue > dMax)
                    {
                        dShutterValue = dMax;
                    }
                    //若小于最小值将曝光值设为最小值
                    if (dShutterValue < dMin)
                    {
                        dShutterValue = dMin;
                    }

                    m_txt_Shutter.Text = dShutterValue.ToString("F2");
                    GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(dShutterValue);
                }
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                // MessageBox.Show(ex.Message);
            }
        }

        private void m_txt_Gain_Leave(object sender, EventArgs e)
        {
            double dGain = 0;            //增益值
            double dMin = 0.0;           //最小值
            double dMax = 0.0;           //最大值
            try
            {
                try
                {
                    dGain = Convert.ToDouble(m_txt_Gain.Text);
                }
                catch (Exception)
                {
                    //__InitGainUI();
                    MessageBox.Show("请输入正确的增益值");
                    return;
                }

                //当前相机的增益值、最小值、最大值
                if (null != GlobalVar.camera.m_objIGXFeatureControl)
                {
                    dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMin();
                    dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMax();

                    //判断输入值是否在增益值的范围内
                    //若输入的值大于最大值则将增益值设置成最大值
                    if (dGain > dMax)
                    {
                        dGain = dMax;
                    }

                    //若输入的值小于最小值则将增益的值设置成最小值
                    if (dGain < dMin)
                    {
                        dGain = dMin;
                    }

                    m_txt_Gain.Text = dGain.ToString("F2");
                    GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").SetValue(dGain);
                }
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                //MessageBox.Show(ex.Message);
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
                //GlobalVar.cLSICCD.OnCCDException(ex);
                throw;
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
                //GlobalVar.cLSICCD.OnCCDException(ex);
            }
        }

        private void __InitEnumComBoxUI(ComboBox cbEnum, string strFeatureName, IGXFeatureControl objIGXFeatureControl, ref bool bIsImplemented)
        {
            string strTriggerValue = "";                   //当前选择项
            List<string> list = new List<string>();   //Combox将要填入的列表
            bool bIsReadable = false;                //是否可读
                                                     // 获取是否支持
            if (null != objIGXFeatureControl)
            {
                bIsImplemented = objIGXFeatureControl.IsImplemented(strFeatureName);
                // 如果不支持则直接返回
                if (!bIsImplemented)
                {
                    return;
                }

                bIsReadable = objIGXFeatureControl.IsReadable(strFeatureName);

                if (bIsReadable)
                {
                    list.AddRange(objIGXFeatureControl.GetEnumFeature(strFeatureName).GetEnumEntryList());
                    //获取当前功能值
                    strTriggerValue = objIGXFeatureControl.GetEnumFeature(strFeatureName).GetValue();
                }
            }

            //清空组合框并更新数据到窗体
            cbEnum.Items.Clear();
            foreach (string str in list)
            {
                cbEnum.Items.Add(str);
            }

            //获得相机值和枚举到值进行比较，刷新对话框
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
            double dCurShuter = 0.0;                       //当前曝光值
            double dMin = 0.0;                       //最小值
            double dMax = 0.0;                       //最大值
            string strUnit = "";                        //单位
            string strText = "";                        //显示内容

            //获取当前相机的曝光值、最小值、最大值和单位
            if (null != GlobalVar.camera.m_objIGXFeatureControl)
            {
                dCurShuter = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetValue();
                dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
                dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
                strUnit = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetUnit();
            }

            //刷新曝光范围及单位到界面上
            strText = string.Format("曝光时间({0}~{1}){2}", dMin.ToString("0.00"), dMax.ToString("0.00"), strUnit);
            m_lbl_Shutter.Text = strText;

            //当前的曝光值刷新到曝光的编辑框
            m_txt_Shutter.Text = dCurShuter.ToString("0.00");
        }

        private void __InitGainUI()
        {
            double dCurGain = 0;             //当前增益值
            double dMin = 0.0;           //最小值
            double dMax = 0.0;           //最大值
            string strUnit = "";            //单位
            string strText = "";            //显示内容

            //获取当前相机的增益值、最小值、最大值和单位
            if (null != GlobalVar.camera.m_objIGXFeatureControl)
            {
                dCurGain = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetValue();
                dMin = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMin();
                dMax = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetMax();
                strUnit = GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("Gain").GetUnit();
            }

            //更新增益值范围到界面
            strText = string.Format("增益({0}~{1}){2}", dMin.ToString("0.00"), dMax.ToString("0.00"), strUnit);
            m_lbl_Gain.Text = strText;

            //当前的增益值刷新到增益的编辑框
            string strCurGain = dCurGain.ToString("0.00");
            m_txt_Gain.Text = strCurGain;
        }

        private void __InitOutput()
        {
            chkOutput.BeginInvoke(new MethodInvoker(() => chkOutput.Checked = Convert.ToBoolean(database.getValue("output"))));
        }

        private void __InitImageProcrss()
        {
            chkImageProcess.Checked = Convert.ToBoolean(database.getValue("imageprocess"));
            if (chkImageProcess.Checked)
            {
                grpImageProcess.Enabled = true;
            }
            else
            {
                grpImageProcess.Enabled = false;
            }

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
            string fName = AppDomain.CurrentDomain.BaseDirectory + "\\Models\\Model1.bmp";   // 获取文件名
            CvInvoke.Imwrite(fName, GlobalVar.camera.matImage);
            //saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory().ToString() + "\\Models";  // 获取文件路径
            ////MessageBox.Show(saveFileDialog1.InitialDirectory);
            //saveFileDialog1.Filter = "*.bmp|bmp file";   // 设置文件类型
            //saveFileDialog1.DefaultExt = ".bmp";   // 默认文件的拓展名
            //saveFileDialog1.FileName = "Model1.bmp";   // 文件默认名
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)   // 显示文件框，并且选择文件
            //{
            //    string fName = saveFileDialog1.FileName;   // 获取文件名
            //    CvInvoke.Imwrite(fName,GlobalVar.camera.matImage);          // 参数1：写入文件的文件名；参数2：写入文件的内容
            //    //GlobalVar.camera.m_objGxBitmap.SaveBmp(mImage, fName);   // 向文件中写入内容
            //}
        }

        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            string ImageModel = AppDomain.CurrentDomain.BaseDirectory + "\\Models\\Model1.bmp";
            Image m = Image.FromFile(ImageModel);
            pictureBox1.Image = m;
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string ImageModel = openFileDialog1.FileName;
            //    Image m = Image.FromFile(ImageModel);
            //    pictureBox1.Image = m;
            //}
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
            if (e.Button != MouseButtons.Left)//左键一直按着
            {
                return;
            }
            if (m_bDraw)
            {
                int x = m_oldPt.X > m_newPt.X ? m_newPt.X : m_oldPt.X;
                int y = m_oldPt.Y > m_newPt.Y ? m_newPt.Y : m_oldPt.Y;
                int hight = Math.Abs(m_oldPt.X - m_newPt.X);
                int width = Math.Abs(m_oldPt.Y - m_newPt.Y);
                m_draw_rect = new Rectangle(x, y, hight, width);
                pictureBox1.Invalidate();
            }
        }

        private void btnCreatePattern_Click(object sender, EventArgs e)
        {
            m_enable = true;
            m_pattern = true;
            GlobalVar.log.AppandText("创建特征");
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
            GlobalVar.log.AppandText("设置结束");
            //GlobalVar.frmSetting = null;
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
                    throw;
                }
            }

            GlobalVar.frmSetting = null;
            Parameters parameters = new Parameters();
            parameters.initialParameters();
            GlobalVar.log.AppandText("关闭设置");
        }

        private void dgvCCDParams_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dv = sender as DataGridView;
            int index = e.RowIndex;
            if (dv.SelectedRows == null || dv.SelectedRows.Count == 0)
            {
                return;
            }
            //首先取得DataGridView的坐标位置：
            int dgvX = this.Location.X + dv.Location.X;// + (this.Width - this.ClientSize.Width);
            int dgvY = this.Location.Y + tabControl1.ItemSize.Height + dv.Location.Y + (this.Height - this.ClientSize.Height);

            ////然后取得选中单元格的坐标在DataGridView中的坐标位置：
            int _cellY = dv.SelectedRows[0].Cells[4].RowIndex;
            Rectangle rect = dv.GetCellDisplayRectangle(3, _cellY, true);
            ////最后可以得到每个单元格相对于form的坐标为：
            int x = dgvX + rect.X;
            int y = dgvY + rect.Y + rect.Height;
            frmEditText frm = new frmEditText();
            frm.Location = new Point(x, y);
            //string tablename = tabControl1.SelectedTab.Tag.ToString();
            //frm.tablename = tablename;
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
            if (txtCaptureDelay.Text != null)
            {
                database.setValue("capturedelay", txtCaptureDelay.Text);
                GlobalVar.camera.m_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(Convert.ToInt32(txtCaptureDelay.Text) * 1000);
            }
            GlobalVar.log.AppandText("修改拍照延时");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                database.setValue("readqr", "true");
                string detProto = database.getValue("detproto");
                string detModel = database.getValue("detmodel");
                string supRsProto = database.getValue("suprsproto");
                string supRsModel = database.getValue("suprsmodel");
                bool isDetProtoExists = File.Exists(detProto);
                if (!isDetProtoExists)
                {
                    MessageBox.Show("模型文件不存在:" + detProto);
                }
                bool isDetModelExists = File.Exists(detModel);
                if (!isDetModelExists)
                {
                    MessageBox.Show("模型文件不存在:" + detModel);
                }
                bool isSRProtoExists = File.Exists(supRsProto);
                if (!isSRProtoExists)
                {
                    MessageBox.Show("模型文件不存在:" + supRsProto);
                }
                bool isSRModelExists = File.Exists(supRsModel);
                if (!isSRModelExists)
                {
                    MessageBox.Show("Keys文件不存在:" + supRsModel);
                }
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
        #region 操作Tips
        
        private void btnCreatePattern_MouseEnter(object sender, EventArgs e)
        {
            if ((this.picTips.Image != null)) //先释放
            {
                picTips.Image.Dispose();
                picTips.Image = null;
            }
            Image img = Properties.Resources.selectpattern;   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            img.Save(mstr, ImageFormat.Gif);        // 保存这个对象
            picTips.Image = Image.FromStream(mstr); //显示
            img.Dispose();//释放占用
        }

        #endregion 操作Tips


        private void btnSearchRegion_MouseEnter(object sender, EventArgs e)
        {
            if ((this.picTips.Image != null)) //先释放
            {
                picTips.Image.Dispose();
                picTips.Image = null;
            }
            Image img = Properties.Resources.selectregion;   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            img.Save(mstr, ImageFormat.Gif);        // 保存这个对象
            picTips.Image = Image.FromStream(mstr); //显示
            img.Dispose();//释放占用
        }

        private void btnOCRRegion_MouseEnter(object sender, EventArgs e)
        {
            if ((this.picTips.Image != null)) //先释放
            {
                picTips.Image.Dispose();
                picTips.Image = null;
            }
            Image img = Properties.Resources.selectocr;   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            img.Save(mstr, ImageFormat.Gif);        // 保存这个对象
            picTips.Image = Image.FromStream(mstr); //显示
            img.Dispose();//释放占用
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
            string fName = AppDomain.CurrentDomain.BaseDirectory + "\\Models\\Pattern1.bmp";   // 获取文件名
            CvInvoke.Imwrite(fName, pattern);
            GlobalVar.log.AppandText("保存特征:"+fName);
            //saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory().ToString() + "\\Patterns";  // 获取文件路径                                                                                           //MessageBox.Show(saveFileDialog1.InitialDirectory);
            //saveFileDialog1.Filter = "*.bmp|bmp file";   // 设置文件类型
            //saveFileDialog1.DefaultExt = ".bmp";   // 默认文件的拓展名
            //saveFileDialog1.FileName = "Pattern1.bmp";   // 文件默认名
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)   // 显示文件框，并且选择文件
            //{
            //    string fName = saveFileDialog1.FileName;   // 获取文件名
            //    CvInvoke.Imwrite(fName, pattern);          // 参数1：写入文件的文件名；参数2：写入文件的内容
            //}
        }
        private void saveSearchRegion()
        {
            int x, y, w, h;
            ConvertCoordinates(pictureBox1, out x, out y, m_draw_rect.X, m_draw_rect.Y);
            ConvertCoordinates(pictureBox1, out w, out h, m_draw_rect.Width, m_draw_rect.Height);
            //Rectangle roi = new Rectangle(x, y, w, h);
            database.setValue("xstart", x.ToString());
            database.setValue("ystart", y.ToString());
            database.setValue("swidth", w.ToString());
            database.setValue("sheight", h.ToString());
            GlobalVar.log.AppandText("修改搜索区域");
        }
        private void saveOCRRegion()
        {
            int x, y, w, h;
            ConvertCoordinates(pictureBox1, out x, out y, m_draw_rect.X, m_draw_rect.Y);
            ConvertCoordinates(pictureBox1, out w, out h, m_draw_rect.Width, m_draw_rect.Height);
            int Xoffset, Yoffset, Woffset, Hoffset;
            Xoffset = px - x;
            Yoffset = py - y;
            Woffset = w;
            Hoffset = h;
            database.setValue("xoffset", Xoffset.ToString());
            database.setValue("yoffset", Yoffset.ToString());
            database.setValue("woffset", Woffset.ToString());
            database.setValue("hoffset", Hoffset.ToString());
            GlobalVar.log.AppandText("修改OCR区域");
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
