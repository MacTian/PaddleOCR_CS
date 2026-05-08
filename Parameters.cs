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
            bool ocrOk = initPPOCR();
            bool qrOk = initQRCode();
            initFilePath();
            string fname = AppDomain.CurrentDomain.BaseDirectory + "\\Models\\Pattern1.bmp";
            if (File.Exists(fname))
            {
                GlobalVar.templateImg = CvInvoke.Imread(fname);
            }
            else
            {
                GlobalVar.log?.AppandText("模板图像不存在: " + fname);
            }
            return ocrOk;
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
            bool isKeysExists = File.Exists(keysPath);
            if (isDetExists && isClsExists && isRecExists && isKeysExists)
            {
                GlobalVar.ocrEngin = new OcrLite();
                GlobalVar.ocrEngin.InitModels(detPath, clsPath, recPath, keysPath, 4);
                GlobalVar.log?.AppandText("OCR初始化完成");
                return true;
            }
            else
            {
                GlobalVar.log?.AppandText("OCR初始化失败，缺少模型文件。det=" + isDetExists + " cls=" + isClsExists + " rec=" + isRecExists + " keys=" + isKeysExists);
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
                GlobalVar.log?.AppandText("QR初始化完成");
                return true;
            }
            else
            {
                GlobalVar.log?.AppandText("QR初始化失败，缺少模型文件");
                return false;
            }
        }

        private void initFilePath()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string modelsDir = Path.Combine(appPath, "Models");
            string detPath = Path.Combine(modelsDir, "ch_PP-OCRv2_det_infer.onnx");
            string recPath = Path.Combine(modelsDir, "ch_PP-OCRv2_rec_infer.onnx");
            string clsPath = Path.Combine(modelsDir, "ch_ppocr_mobile_v2.0_cls_infer.onnx");
            string keysPath = Path.Combine(modelsDir, "ppocr_keys_v1.txt");
            string detProto = Path.Combine(modelsDir, "detect.prototxt");
            string detModel = Path.Combine(modelsDir, "detect.caffemodel");
            string supRsProto = Path.Combine(modelsDir, "sr.prototxt");
            string supRsModel = Path.Combine(modelsDir, "sr.caffemodel");
            database.setValue("detpath", detPath);
            database.setValue("recpath", recPath);
            database.setValue("clspath", clsPath);
            database.setValue("keyspath", keysPath);
            database.setValue("detproto", detProto);
            database.setValue("detmodel", detModel);
            database.setValue("suprsproto", supRsProto);
            database.setValue("suprsmodel", supRsModel);
        }
    }
}
