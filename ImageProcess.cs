using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace CV_app
{
    internal class ImageProcess
    {
        private Database database = new Database();

        public Mat process(Mat img)
        {
            try
            {
                string binary = database.getValue("binary");
                string binaryInv = database.getValue("binaryinverse");
                int iblocksize = Convert.ToInt32(database.getValue("blocksize"));
                int ifilterC = Convert.ToInt32(database.getValue("filtersize"));
                int ierodex = Convert.ToInt32(database.getValue("erodex"));
                int ierodey = Convert.ToInt32(database.getValue("erodey"));
                int idilatex = Convert.ToInt32(database.getValue("dilatex"));
                int idilatey = Convert.ToInt32(database.getValue("dilatey"));

                CvInvoke.CvtColor(img, img, ColorConversion.Bgr2Gray);
                if (binary == "true")
                {
                    CvInvoke.AdaptiveThreshold(img, img, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, iblocksize, ifilterC);
                }

                if (binaryInv == "true")
                {
                    CvInvoke.AdaptiveThreshold(img, img, 255, AdaptiveThresholdType.MeanC, ThresholdType.BinaryInv, iblocksize, ifilterC);
                }

                Mat element1 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(ierodex, ierodey), new Point(-1, -1));
                Mat element2 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(idilatex, idilatey), new Point(-1, -1));

                CvInvoke.Erode(img, img, element1, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                CvInvoke.Dilate(img, img, element2, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("图像处理失败: " + ex.Message);
            }
            return img;
        }
    }
}
