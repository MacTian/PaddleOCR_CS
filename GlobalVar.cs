using Emgu.CV;
using OcrLiteLib;
using System.Collections.Generic;
using Tool;

namespace CV_app
{
    class GlobalVar
    {
        public static OcrLite ocrEngin;
        public static TextLine textLine;
        public static Setting frmSetting;
        public static Main frmMain;
        public static Camera camera = null;
        public static OcrResult ocrResult;
        internal static WeChatQRCode qRCode;
        public static Mat templateImg;
        public static int iCount, iNG;
        public static Log log;
        public static Dictionary<string, AppParam_CCD> ZAppParam_CCDs = new Dictionary<string, AppParam_CCD>();    // 应用程序配置-ccd
    }
}
