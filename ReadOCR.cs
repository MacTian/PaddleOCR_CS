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
        string markcontent;
        public Rectangle createROI(Mat image)
        {
            try
            {
                double visionox, visionoy, visionor, laserox, laseroy, scale, lasercx, lasercy, laserca, lasercb, rangex, rangey;
                string[] info = database.getValue("mark_data").Split('$');
                lasercx = Convert.ToDouble((info[1]));
                lasercy = Convert.ToDouble((info[2]));
                laserca = Convert.ToDouble((info[3]));
                lasercb = Convert.ToDouble((info[4]));
                Console.WriteLine("cx:" + lasercx + "\r\ncy:" + lasercy + "\r\nca:" + laserca + "\r\ncb:" + lasercb);
                for (int i = 5; i < info.Length; i++)
                {
                    markcontent += info[i];
                }

                visionox = Convert.ToDouble(database.getValue("visionox"));
                visionoy = Convert.ToDouble(database.getValue("visionoy"));
                visionor = Convert.ToDouble(database.getValue("visionor"));
                laserox = Convert.ToDouble(database.getValue("laserox"));
                laseroy = Convert.ToDouble(database.getValue("laseroy"));
                scale = Convert.ToDouble(database.getValue("scale"));
                rangex = Convert.ToDouble(database.getValue("rangex"));
                rangey = Convert.ToDouble(database.getValue("rangey"));

                //lasercx = Convert.ToDouble(database.getValue("lasercx"));
                //lasercy = Convert.ToDouble(database.getValue("visionoy"));
                //laserca = Convert.ToDouble(database.getValue("laserca"));
                //lasercb = Convert.ToDouble(database.getValue("lasercb"));

                double rx = visionox + (lasercx - laserox) * scale - visionor - rangex * scale;
                double ry = visionoy - (lasercy - laseroy) * scale + visionor + rangey * scale;
                double ra = laserca * scale + rangex * scale * 2;
                double rb = lasercb * scale + rangey * scale * 2;
                Console.WriteLine("rx:" + rx + "\r\nry:" + ry + "\r\nra:" + ra + "\r\nrb:" + rb);
                Rectangle tmp = new Rectangle((int)rx, (int)ry - (int)rb, (int)ra, (int)rb);
                //Rectangle tmp = new Rectangle((int)rx, (int)ry, (int)ra, (int)rb);
                CvInvoke.Rectangle(image, tmp, new MCvScalar(255, 0, 0), 1);
                return tmp;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public OcrResult recognizeText(Mat image)
        {
            if (GlobalVar.ocrEngin == null)
            {
                MessageBox.Show("未初始化，无法执行!");
                return null;
            }
            if (image.Height < 96 || image.Width<96)
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

        public bool checkResult(string str)
        {
            markcontent = database.getValue("lasercontent");
            Console.WriteLine("result:" + str + "\r\nmarkcontent:" + markcontent);
            if (null != markcontent)
            {
                markcontent = Regex.Replace(markcontent, @"[\s]", "");
                GlobalVar.log.AppandText("标刻OCR:" + markcontent);
            }
            if (null != str)
            {
                str = Regex.Replace(str, @"[\s]", "");
                GlobalVar.log.AppandText("读取OCR:" +str);
            }
            Console.WriteLine("[" + str + "][" + markcontent + "]");
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
            int Xstart, Ystart, Swidth, Sheight,Xoffset,Yoffset,Woffset,Hoffset;
            Xstart = Convert.ToInt32(database.getValue("xstart"));
            Ystart = Convert.ToInt32(database.getValue("ystart"));
            Swidth = Convert.ToInt32(database.getValue("swidth"));
            Sheight = Convert.ToInt32(database.getValue("sheight"));
            Xoffset = Convert.ToInt32(database.getValue("xoffset"));
            Yoffset = Convert.ToInt32(database.getValue("yoffset"));
            Woffset = Convert.ToInt32(database.getValue("woffset"));
            Hoffset = Convert.ToInt32(database.getValue("hoffset"));
            Mat srcImage = source;
            Mat template = GlobalVar.templateImg;
            Mat dstImage = new Mat();
            Rectangle searchRegion = new Rectangle(Xstart, Ystart,Swidth, Sheight);
            srcImage = new Mat(source, searchRegion);

            CvInvoke.MatchTemplate(srcImage, template, dstImage, TemplateMatchingType.CcoeffNormed);
            //CvInvoke.MatchTemplate(source, template, dstImage, TemplateMatchingType.Sqdiff);
            CvInvoke.Normalize(dstImage, dstImage, 0, 1, NormType.MinMax);
            double minValue = 0, maxValue = 0;
            Point minLocation = new Point(0, 0), maxLocation = new Point(0, 0);
            Point matchLocation = new Point(0, 0);
            CvInvoke.MinMaxLoc(dstImage, ref minValue, ref maxValue, ref minLocation, ref maxLocation);
            matchLocation = maxLocation;
            Rectangle rstRegion = new Rectangle(matchLocation.X + Xstart,matchLocation.Y + Ystart, template.Width, template.Height);
            Rectangle ocrRegion = new Rectangle(rstRegion.X - Xoffset,rstRegion.Y - Yoffset, Woffset, Hoffset);
            return ocrRegion;
        }

        public void resizeImage(Mat src)
        {
            double scale;
            if (src.Height < 96)
                scale = 256 / src.Height;
            else
                scale = 256 / src.Width;

            CvInvoke.Resize(src, src, new Size((int)(src.Width * scale), (int)(src.Height * scale)));
            GlobalVar.log.AppandText("区域较小，缩放:" + scale.ToString());
            //CvInvoke.Imwrite("D:\\tmp.bmp", src);
        }
    }
}