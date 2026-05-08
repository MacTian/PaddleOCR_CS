using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using GxIAPINET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CV_app
{
    internal class Camera : IDisposable
    {
        public IGXFactory m_objIGXFactory = null;
        public IGXDevice m_objIGXDevice = null;
        public IGXStream m_objIGXStream = null;
        public IGXFeatureControl m_objIGXStreamFeatureControl;
        public IGXFeatureControl m_objIGXFeatureControl = null;
        public GxBitmap m_objGxBitmap = null;
        public Mat matImage = null;
        public string outPut = null;
        private Database database = new Database();
        private ImageProcess imageProcess = new ImageProcess();
        private MCvScalar charColor = new MCvScalar(255, 0, 0);
        private MCvScalar featureColor = new MCvScalar(0, 255, 0);
        private MCvScalar regionColor = new MCvScalar(0, 0, 255);
        private readonly object _lock = new object();
        public List<int> arrOutvalue;
        bool result;
        private bool _disposed = false;

        public Camera()
        {
            arrOutvalue = new List<int>();
        }

        private void CloseDevice()
        {
            try
            {
                if (null != m_objIGXFeatureControl)
                {
                    m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                }
                if (null != m_objIGXStream)
                {
                    m_objIGXStream.StopGrab();
                    m_objIGXStream.UnregisterCaptureCallback();
                    m_objIGXStream = null;
                    m_objIGXStreamFeatureControl = null;
                }
                if (null != m_objIGXDevice)
                {
                    m_objIGXDevice.Close();
                    m_objIGXDevice = null;
                }
                if (null != m_objIGXFactory)
                {
                    m_objIGXFactory.Uninit();
                    m_objIGXFactory = null;
                }
                if (null != m_objGxBitmap)
                {
                    m_objGxBitmap.Dispose();
                    m_objGxBitmap = null;
                }
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("关闭相机设备失败: " + ex.Message);
            }
        }

        ~Camera()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                CloseDevice();
                _disposed = true;
            }
        }

        public bool releaseCam()
        {
            CloseDevice();
            return true;
        }

        public bool initialCamera()
        {
            try
            {
                m_objIGXFactory = IGXFactory.GetInstance();
                m_objIGXFactory.Init();

                List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
                m_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);

                if (listGXDeviceInfo.Count <= 0)
                {
                    GlobalVar.log?.AppandText("当前设备未连接");
                    return false;
                }
                if (listGXDeviceInfo.Count == 1)
                {
                    if (listGXDeviceInfo[0].GetUserID() != "CCD1")
                    {
                        GlobalVar.log?.AppandText("当前设备未连接(CCD1)");
                        return false;
                    }
                }
                foreach (IGXDeviceInfo dev in listGXDeviceInfo)
                {
                    string camName = dev.GetUserID();
                    string strMac = dev.GetMAC();
                    GX_ACCESS_STATUS status = dev.GetAccessStatus();
                    if (status != GX_ACCESS_STATUS.GX_ACCESS_STATUS_READWRITE)
                    {
                        m_objIGXFactory.GigEResetDevice(strMac, GX_RESET_DEVICE_MODE.GX_MANUFACTURER_SPECIFIC_RECONNECT);
                    }

                    if (camName == "CCD1")
                    {
                        if (null != m_objIGXDevice)
                        {
                            m_objIGXDevice.Close();
                            m_objIGXDevice = null;
                        }
                        m_objIGXFactory.Init();
                        m_objIGXFactory.UpdateDeviceList(100);
                        m_objIGXDevice = m_objIGXFactory.OpenDeviceByUserID(camName, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                        m_objIGXFeatureControl = m_objIGXDevice.GetRemoteFeatureControl();

                        if (null != m_objIGXDevice)
                        {
                            m_objIGXStream = m_objIGXDevice.OpenStream(0);
                            m_objIGXStreamFeatureControl = m_objIGXStream.GetFeatureControl();
                        }
                        m_objGxBitmap = new GxBitmap(m_objIGXDevice);
                        if (null != m_objIGXDevice)
                        {
                            m_objIGXDevice.RegisterDeviceOfflineCallback(this, __DeviceOfflineCallbackPro);
                            if (null != m_objIGXStreamFeatureControl)
                            {
                                try
                                {
                                    m_objIGXStreamFeatureControl.GetEnumFeature("StreamBufferHandlingMode").SetValue("OldestFirst");
                                }
                                catch (Exception ex)
                                {
                                    GlobalVar.log?.AppandText("设置缓冲模式失败: " + ex.Message);
                                }
                            }
                            if (null != m_objIGXStream)
                            {
                                m_objIGXStream.RegisterCaptureCallback(this, __CaptureCallbackPro);
                                m_objIGXStream.StartGrab();
                            }
                            if (null != m_objIGXFeatureControl)
                            {
                                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
                            }
                        }
                    }
                }
                resultOutput(false);
                return true;
            }
            catch (CGalaxyException ex)
            {
                GlobalVar.log?.AppandText("相机初始化失败: " + ex.Message);
                return false;
            }
        }

        private void __CaptureCallbackPro(object objUserParam, IFrameData objIFrameData)
        {
            GlobalVar.log?.AppandText("采集图像");
            try
            {
                Bitmap img = m_objGxBitmap.GetBmp(objIFrameData);
                Image<Bgr, Byte> image = img.ToImage<Bgr, byte>();
                matImage = image.Mat;

                if (GlobalVar.frmSetting != null)
                {
                    if (GlobalVar.frmSetting.TopLevel)
                    {
                        GlobalVar.frmSetting.pictureBox1.Image = img;
                    }
                    return;
                }
                ReadOCR readOCR = new ReadOCR();
                Rectangle region = readOCR.matchTemplate(matImage);
                Mat ocrImg = new Mat(matImage, region);
                if (Convert.ToBoolean(database.getValue("imageprocess")))
                    imageProcess.process(ocrImg);
                GlobalVar.ocrResult = readOCR.recognizeText(ocrImg);

                string qRstatus = database.getValue("readqr");
                if (qRstatus == "true")
                {
                    ReadQR qR = new ReadQR();
                    qR.decode(matImage);
                }

                result = readOCR.IsResultNG(GlobalVar.ocrResult.StrRes);
                GlobalVar.log?.AppandText("是否输出:" + result);
                outPut = database.getValue("output");
                if (outPut == "true")
                {
                    lock (_lock)
                    {
                        if (result)
                        {
                            int tmp = GlobalVar.iCount + Convert.ToInt32(database.getValue("outgap"));
                            arrOutvalue.Add(tmp);
                        }

                        foreach (int i in arrOutvalue)
                        {
                            if (GlobalVar.iCount == i)
                            {
                                resultOutput(true);
                                break;
                            }
                            else
                                resultOutput(false);
                        }
                    }
                }

                Point txtLocation = new Point(region.X, region.Y + 200);
                if (!result)
                    CvInvoke.PutText(matImage, "OK", txtLocation, FontFace.HersheyComplex, 4.0, featureColor, 4, LineType.Filled);
                else
                    CvInvoke.PutText(matImage, "NG", txtLocation, FontFace.HersheyComplex, 4.0, regionColor, 4, LineType.Filled);
                CvInvoke.Rectangle(matImage, region, regionColor, 1);

                SafeBeginInvoke(GlobalVar.frmMain, () => GlobalVar.frmMain.picImage.Image = matImage.ToBitmap());
                SafeBeginInvoke(GlobalVar.frmMain, () => GlobalVar.frmMain.picRegion.Image = ocrImg.ToBitmap());
                SafeBeginInvoke(GlobalVar.frmMain, () => GlobalVar.frmMain.lblResultstr.Text = GlobalVar.ocrResult.StrRes);
                GlobalVar.iCount += 1;
                updateData();
                saveImage(objIFrameData);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("图像处理异常: " + ex.Message);
            }
        }

        private static void SafeBeginInvoke(Control control, Action action)
        {
            if (control != null && control.IsHandleCreated && !control.IsDisposed)
            {
                try
                {
                    control.BeginInvoke(new MethodInvoker(action));
                }
                catch (ObjectDisposedException) { }
                catch (InvalidOperationException) { }
            }
        }

        private void __SetEnumValue(string strFeatureName, bool bValue, IGXFeatureControl objIGXFeatureControl)
        {
            if (null != objIGXFeatureControl)
            {
                try
                {
                    objIGXFeatureControl.GetBoolFeature(strFeatureName).SetValue(bValue);
                }
                catch (Exception ex)
                {
                    GlobalVar.log?.AppandText("设置相机属性失败 " + strFeatureName + ": " + ex.Message);
                }
            }
        }

        public void resultOutput(bool result)
        {
            try
            {
                if (result)
                    __SetEnumValue("UserOutputValue", true, m_objIGXFeatureControl);
                else
                    __SetEnumValue("UserOutputValue", false, m_objIGXFeatureControl);
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("输出结果失败: " + ex.Message);
            }
        }

        private void __DeviceOfflineCallbackPro(object obj)
        {
            GlobalVar.log?.AppandText("相机离线");
        }

        public void updateData()
        {
            SafeBeginInvoke(GlobalVar.frmMain, () => GlobalVar.frmMain.lblTotal.Text = GlobalVar.iCount.ToString());
            SafeBeginInvoke(GlobalVar.frmMain, () => GlobalVar.frmMain.lblNG.Text = GlobalVar.iNG.ToString());
        }

        public void saveImage(IFrameData objIFrameData)
        {
            try
            {
                string m_strFilePath = database.getValue("imagespath");
                string imageSave = database.getValue("imagesave");
                if (string.IsNullOrEmpty(m_strFilePath)) return;

                DateTime dtNow = DateTime.Now;
                string strDateTime = dtNow.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                string strDateFile = dtNow.ToString("yyyy_MM_dd");
                string dateDir = Path.Combine(m_strFilePath, strDateFile);
                if (!Directory.Exists(dateDir))
                {
                    Directory.CreateDirectory(dateDir);
                }

                string prefix = result ? "bad_" : "good_";
                string stfFileName = Path.Combine(dateDir, prefix + strDateTime + ".bmp");

                if (imageSave == "null")
                {
                    return;
                }
                if (imageSave == "ng" && result)
                {
                    if (DiskSpace.GetHardDiskFreeSpace("D") < 1000)
                        GlobalVar.log?.AppandText("磁盘空间不足1000M");
                    m_objGxBitmap.SaveBmp(objIFrameData, stfFileName);
                    GlobalVar.log?.AppandText("保存NG图像:" + stfFileName);
                }
                if (imageSave == "all")
                {
                    m_objGxBitmap.SaveBmp(objIFrameData, stfFileName);
                }
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("保存图像失败: " + ex.Message);
            }
        }
    }
}
