using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OcrLiteLib;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CV_app
{
    internal class ReadOCR
    {
        private Database database = new Database();

        public Rectangle createROI(Mat image)
        {
            try
            {
                double visionox, visionoy, visionor, laserox, laseroy, scale, lasercx, lasercy, laserca, lasercb, rangex, rangey;
                string[] info = database.getValue("mark_data").Split('$');
                lasercx = Convert.ToDouble(info[1]);
                lasercy = Convert.ToDouble(info[2]);
                laserca = Convert.ToDouble(info[3]);
                lasercb = Convert.ToDouble(info[4]);

                visionox = Convert.ToDouble(database.getValue("visionox"));
                visionoy = Convert.ToDouble(database.getValue("visionoy"));
                visionor = Convert.ToDouble(database.getValue("visionor"));
                laserox = Convert.ToDouble(database.getValue("laserox"));
                laseroy = Convert.ToDouble(database.getValue("laseroy"));
                scale = Convert.ToDouble(database.getValue("scale"));
                rangex = Convert.ToDouble(database.getValue("rangex"));
                rangey = Convert.ToDouble(database.getValue("rangey"));

                double rx = visionox + (lasercx - laserox) * scale - visionor - rangex * scale;
                double ry = visionoy - (lasercy - laseroy) * scale + visionor + rangey * scale;
                double ra = laserca * scale + rangex * scale * 2;
                double rb = lasercb * scale + rangey * scale * 2;
                Rectangle tmp = new Rectangle((int)rx, (int)ry - (int)rb, (int)ra, (int)rb);
                CvInvoke.Rectangle(image, tmp, new MCvScalar(255, 0, 0), 1);
                return tmp;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("createROI 失败: " + ex.Message);
                throw;
            }
        }

        public OcrResult recognizeText(Mat image)
        {
            if (GlobalVar.ocrEngin == null)
            {
                GlobalVar.log?.AppandText("OCR引擎未初始化");
                MessageBox.Show("未初始化，无法执行!");
                return null;
            }
            if (image.Height < 96 || image.Width < 96)
                resizeImage(image);
            int padding = 0;
            int imgResize = 1024;
            float boxScoreThresh = 0.7f;
            float boxThresh = 0.3f;
            float unClipRatio = 2.0f;
            bool doAngle = true;
            bool mostAngle = false;
            return GlobalVar.ocrEngin.DetectAndRecognize(image, padding, imgResize, boxScoreThresh, boxThresh, unClipRatio, doAngle, mostAngle);
        }

        /// <summary>
        /// 检查OCR结果是否为NG（与标刻内容不匹配）
        /// </summary>
        /// <returns>true=NG（不良品），false=OK（良品）</returns>
        public bool IsResultNG(string str)
        {
            string markcontent = database.getValue("lasercontent");
            GlobalVar.log?.AppandText("标刻OCR:" + markcontent + " 读取OCR:" + str);
            if (null != markcontent)
            {
                markcontent = Regex.Replace(markcontent, @"[\s]", "");
            }
            if (null != str)
            {
                str = Regex.Replace(str, @"[\s]", "");
            }
            if (str == markcontent)
            {
                return false;
            }
            else
            {
                GlobalVar.iNG += 1;
                return true;
            }
        }

        public Rectangle matchTemplate(Mat source)
        {
            int Xstart, Ystart, Swidth, Sheight, Xoffset, Yoffset, Woffset, Hoffset;
            Xstart = Convert.ToInt32(database.getValue("xstart"));
            Ystart = Convert.ToInt32(database.getValue("ystart"));
            Swidth = Convert.ToInt32(database.getValue("swidth"));
            Sheight = Convert.ToInt32(database.getValue("sheight"));
            Xoffset = Convert.ToInt32(database.getValue("xoffset"));
            Yoffset = Convert.ToInt32(database.getValue("yoffset"));
            Woffset = Convert.ToInt32(database.getValue("woffset"));
            Hoffset = Convert.ToInt32(database.getValue("hoffset"));
            Mat srcImage = new Mat(source, new Rectangle(Xstart, Ystart, Swidth, Sheight));
            Mat template = GlobalVar.templateImg;
            Mat dstImage = new Mat();

            CvInvoke.MatchTemplate(srcImage, template, dstImage, TemplateMatchingType.CcoeffNormed);
            CvInvoke.Normalize(dstImage, dstImage, 0, 1, NormType.MinMax);
            double minValue = 0, maxValue = 0;
            Point minLocation = new Point(0, 0), maxLocation = new Point(0, 0);
            CvInvoke.MinMaxLoc(dstImage, ref minValue, ref maxValue, ref minLocation, ref maxLocation);
            Rectangle rstRegion = new Rectangle(maxLocation.X + Xstart, maxLocation.Y + Ystart, template.Width, template.Height);
            Rectangle ocrRegion = new Rectangle(rstRegion.X - Xoffset, rstRegion.Y - Yoffset, Woffset, Hoffset);
            return ocrRegion;
        }

        public void resizeImage(Mat src)
        {
            double scale;
            if (src.Height < 96)
                scale = 256.0 / src.Height;
            else
                scale = 256.0 / src.Width;

            CvInvoke.Resize(src, src, new Size((int)(src.Width * scale), (int)(src.Height * scale)));
            GlobalVar.log?.AppandText("区域较小，缩放:" + scale.ToString("F2"));
        }
    }
}
