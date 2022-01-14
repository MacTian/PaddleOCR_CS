using Emgu.CV;
using OcrLiteLib;
using System;
using System.IO;

namespace CV_app
{
    internal class Parameters
    {
        private Database database = new Database();

        public bool initialParameters()
        {
            initPPOCR();
            initQRCode();
            string fname = Directory.GetCurrentDirectory().ToString() + "\\Models\\Pattern1.bmp";
            GlobalVar.templateImg = CvInvoke.Imread(fname);
            return false;
        }

        private bool initPPOCR()
        {
            string detPath, recPath;
            bool engServer = Convert.ToBoolean(database.getValue("engserver"));
            if (!engServer)
            {
                detPath = database.getValue("detpath");
                recPath = database.getValue("recpath");
            }
            else
            {
                detPath = database.getValue("detpath_server");
                recPath = database.getValue("recpath_server");
            }

            string clsPath = database.getValue("clspath");
            string keysPath = database.getValue("keyspath");
            
            bool isDetExists = File.Exists(detPath);
            bool isClsExists = File.Exists(clsPath);
            bool isRecExists = File.Exists(recPath);
            bool isKeysExists = File.Exists(recPath);
            if (isDetExists && isClsExists && isRecExists && isKeysExists)
            {
                GlobalVar.ocrEngin = new OcrLite();
                GlobalVar.ocrEngin.InitModels(detPath, clsPath, recPath, keysPath, 4);
                Console.WriteLine("OCR初始化完成！");
                return true;
            }
            else
            {
                Console.WriteLine("OCR初始化失败，请确认模型文件夹和文件后，重新初始化！");
                return false;
            }
        }

        private bool initQRCode()
        {
            string detProto = database.getValue("detproto");
            string detModel = database.getValue("detmodel");
            string supRsProto = database.getValue("suprsproto");
            string supRsModel = database.getValue("suprsmodel");
            bool isDetProtoExists = File.Exists(detProto);
            bool isDetModelExists = File.Exists(detModel);
            bool isSRProtoExists = File.Exists(supRsProto);
            bool isSRModelExists = File.Exists(supRsModel);
            if (isDetProtoExists && isDetModelExists && isSRProtoExists && isSRModelExists)
            {
                GlobalVar.qRCode = new WeChatQRCode(detProto, detModel, supRsProto, supRsModel);
                return true;
            }
            else
            {
                Console.WriteLine("QR初始化失败，请确认模型文件夹和文件后，重新初始化！");
                return false;
            }
        }
    }
}