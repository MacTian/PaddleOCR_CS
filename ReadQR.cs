using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_app
{
    class ReadQR
    {
        private MCvScalar regionColor = new MCvScalar(0, 0, 255);
        public void decode(Mat image)
        {
            WeChatQRCode.QRCode[] rst = GlobalVar.qRCode.DetectAndDecode(image);
            foreach (WeChatQRCode.QRCode qR in rst)
            {
                CvInvoke.Polylines(image, qR.Region, true, regionColor, 1);
                Point txtLocation = new Point(qR.Region[3].X, qR.Region[3].Y + 30);
                string strcode = qR.Code;
                CvInvoke.PutText(image, strcode, txtLocation, FontFace.HersheyComplex, 1.0, regionColor, 1, LineType.Filled);
                GlobalVar.log.AppandText("读取QR:" + strcode);
            }
        }
    }
}
