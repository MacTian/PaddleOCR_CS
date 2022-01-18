using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using GxIAPINET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CV_app
{
    internal class Camera
    {
        public IGXFactory m_objIGXFactory = null;                            ///<Factory对像
        public IGXDevice m_objIGXDevice = null;                            ///<设备对像
        public IGXStream m_objIGXStream = null;
        public IGXFeatureControl m_objIGXStreamFeatureControl;
        public IGXFeatureControl m_objIGXFeatureControl = null;                            ///<远端设备属性控制器对像
        public GxBitmap m_objGxBitmap = null;
        public Mat matImage = null;
        public string outPut = null;
        private Database database = new Database();
        private ImageProcess imageProcess = new ImageProcess();
        private MCvScalar charColor = new MCvScalar(255, 0, 0);
        private MCvScalar featureColor = new MCvScalar(0, 255, 0);
        private MCvScalar regionColor = new MCvScalar(0, 0, 255);
        public ArrayList arrOutvalue;
        bool result;
        public Camera()
        {
            arrOutvalue = new ArrayList();
        }

        ~Camera()
        {
            try
            {
                //关闭设备
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
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
            }
        }
        public bool releaseCam()
        {
            try
            {
                //关闭设备
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
                if(null!=m_objIGXFactory)
                {
                    m_objIGXFactory.Uninit();
                    m_objIGXFactory = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                //GlobalVar.cLSICCD.OnCCDException(ex);
                return false;
            }
        }
        public bool initialCamera()
        {
            try
            {
                m_objIGXFactory = IGXFactory.GetInstance();
                m_objIGXFactory.Init();

                List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
                m_objIGXFactory.UpdateDeviceList(200, listGXDeviceInfo);

                // 判断当前连接设备个数
                if (listGXDeviceInfo.Count <= 0)
                {
                    Console.WriteLine("當前設備未連接！");
                    //MessageBox.Show("當前設備未連接！");
                    return false;
                }
                if (listGXDeviceInfo.Count == 1)
                {
                    if (listGXDeviceInfo[0].GetUserID() != "CCD1")
                    {
                        Console.WriteLine("當前設備未連接！");
                        //MessageBox.Show("當前設備未連接！");
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
                        //m_objIGXFactory.Uninit();
                        m_objIGXFactory.GigEResetDevice(strMac, GX_RESET_DEVICE_MODE.GX_MANUFACTURER_SPECIFIC_RECONNECT);
                    }

                    Console.WriteLine("设备状态ok");

                    if (camName == "CCD1")
                    {
                        //打开第1个设备
                        if (null != m_objIGXDevice)
                        {
                            m_objIGXDevice.Close();
                            m_objIGXDevice = null;
                        }
                        //m_objIGXFactory.Uninit();
                        m_objIGXFactory.Init();
                        m_objIGXFactory.UpdateDeviceList(100);
                        m_objIGXDevice = m_objIGXFactory.OpenDeviceByUserID(camName, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                        m_objIGXFeatureControl = m_objIGXDevice.GetRemoteFeatureControl();

                        if (null != m_objIGXDevice)
                        {
                            m_objIGXStream = m_objIGXDevice.OpenStream(0);
                            m_objIGXStreamFeatureControl = m_objIGXStream.GetFeatureControl();
                        }
                        Console.WriteLine("bitmap null");
                        m_objGxBitmap = new GxBitmap(m_objIGXDevice);
                        Console.WriteLine("bitmap ok");
                        if (null != m_objIGXDevice)
                        {
                            m_objIGXDevice.RegisterDeviceOfflineCallback(this, __DeviceOfflineCallbackPro);
                            if (null != m_objIGXStreamFeatureControl)
                            {
                                try
                                {
                                    //设置流层Buffer处理模式为OldestFirst
                                    m_objIGXStreamFeatureControl.GetEnumFeature("StreamBufferHandlingMode").SetValue("OldestFirst");
                                }
                                catch (Exception)
                                {
                                }
                            }
                            //开启采集流通道
                            if (null != m_objIGXStream)
                            {
                                //RegisterCaptureCallback第一个参数属于用户自定参数(类型必须为引用
                                //类型)，若用户想用这个参数可以在委托函数中进行使用
                                m_objIGXStream.RegisterCaptureCallback(this, __CaptureCallbackPro);
                                m_objIGXStream.StartGrab();
                                Console.WriteLine("执行采集指令");
                            }
                            //发送开采命令
                            if (null != m_objIGXFeatureControl)
                            {
                                //m_objIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(Convert.ToInt32(captureDelay) * 1000);
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
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        private void __CaptureCallbackPro(object objUserParam, IFrameData objIFrameData)
        {
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
                //Rectangle region = readOCR.createROI(matImage);
                Rectangle region = readOCR.matchTemplate(matImage);
                Mat ocrImg = new Mat(matImage, region);
                if (Convert.ToBoolean(database.getValue("imageprocess")))
                    imageProcess.process(ocrImg);
                GlobalVar.ocrResult = readOCR.recognizeText(ocrImg);
                //读取二维码
                string qRstatus = database.getValue("readqr");
                if(qRstatus=="true")
                {
                    ReadQR qR = new ReadQR();
                    qR.decode(matImage);
                }
                
                result = readOCR.checkResult(GlobalVar.ocrResult.StrRes);
                Console.WriteLine("result:" + result);
                outPut = database.getValue("output");
                if (outPut == "true")
                {
                    if (result)
                    {
                        int tmp = GlobalVar. iCount + Convert.ToInt32(database.getValue("outgap"));
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
                    //resultOutput(result);
                }

                Point txtLocation = new Point(region.X, region.Y + 200);
                if (!result)
                    CvInvoke.PutText(matImage, "OK", txtLocation, FontFace.HersheyComplex, 4.0, featureColor, 4, LineType.Filled);
                else
                    CvInvoke.PutText(matImage, "NG", txtLocation, FontFace.HersheyComplex, 4.0, regionColor, 4, LineType.Filled);
                CvInvoke.Rectangle(matImage, region, regionColor, 1);
                GlobalVar.frmMain.picImage.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.picImage.Image = matImage.ToBitmap()));
                GlobalVar.frmMain.picRegion.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.picRegion.Image=ocrImg.ToBitmap()));
                GlobalVar.frmMain.lblResultstr.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.lblResultstr.Text = GlobalVar.ocrResult.StrRes));
                GlobalVar.iCount += 1;
                updateData();
                saveImage(objIFrameData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
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
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        public void resultOutput(bool result)
        {
            try
            {
                if (result)
                {
                    __SetEnumValue("UserOutputValue", true, m_objIGXFeatureControl);
                }
                else
                {
                    __SetEnumValue("UserOutputValue", false, m_objIGXFeatureControl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void __DeviceOfflineCallbackPro(object obj)
        {
            //GlobalVar.cLSICCD.OnCCDChanged(false);
        }

        public void updateData()
        {
            GlobalVar.frmMain.lblTotal.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.lblTotal.Text=GlobalVar.iCount.ToString()));
            GlobalVar.frmMain.lblNG.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.lblNG.Text = GlobalVar.iNG.ToString()));
            //DateTime dateTime = DateTime.Now;
            //GlobalVar.frmMain.chart1.BeginInvoke(new MethodInvoker(()=>GlobalVar.frmMain.chart1.Series[0].Points.AddXY(dateTime, GlobalVar.iCount)));
            //GlobalVar.frmMain.chart1.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.chart1.Series[1].Points.AddXY(dateTime, GlobalVar.iNG)));
            //GlobalVar.frmMain.chart1.BeginInvoke(new MethodInvoker(() => GlobalVar.frmMain.chart1.Series[2].Points.AddXY(dateTime, 100-GlobalVar.iCount/GlobalVar.iCount*100)));
        }

        public void saveImage(IFrameData objIFrameData)
        {
            string m_strFilePath = database.getValue("imagespath");
            string imageSave = database.getValue("imagesave");
            DateTime dtNow = DateTime.Now;  // 获取系统当前时间
            string strDateTime = dtNow.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            string strDateFile = dtNow.ToString("yyyy_MM_dd");
            if (!Directory.Exists(m_strFilePath + "\\" + strDateFile))
            {
                Directory.CreateDirectory(m_strFilePath + "\\" + strDateFile);
            }

            string stfFileName = m_strFilePath + "\\" + strDateFile + "\\";
            if (!result)
            {
                stfFileName += "good_";
            }

            if (result)
            {
                stfFileName += "bad_";
            }

            stfFileName += strDateTime + ".bmp";  // 默认的图像保存名称
            // 是否需要进行图像保存
            if (imageSave == "null")
            {
                return;
            }
            if (imageSave == "ng" && result)
            {
                m_objGxBitmap.SaveBmp(objIFrameData, stfFileName);
            }
            if (imageSave == "all")
            {
                m_objGxBitmap.SaveBmp(objIFrameData, stfFileName);
            }
        }
    }
}