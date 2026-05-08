using Emgu.CV;
using OcrLiteLib;
using System.Collections.Generic;
using System.Threading;
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

        // 使用 Interlocked 保证多线程原子访问
        private static int _iCount;
        private static int _iNG;

        public static int iCount
        {
            get => Interlocked.CompareExchange(ref _iCount, 0, 0);
            set => Interlocked.Exchange(ref _iCount, value);
        }

        public static int iNG
        {
            get => Interlocked.CompareExchange(ref _iNG, 0, 0);
            set => Interlocked.Exchange(ref _iNG, value);
        }

        public static Log log;
        public static Dictionary<string, AppParam_CCD> ZAppParam_CCDs = new Dictionary<string, AppParam_CCD>();
    }
}
