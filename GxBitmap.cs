using GxIAPINET;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace CV_app
{
    public class GxBitmap : IDisposable
    {
        private IGXDevice m_objIGXDevice = null;
        private bool m_bIsColor = false;
        private byte[] m_byMonoBuffer = null;
        private byte[] m_byColorBuffer = null;
        private byte[] m_byRawBuffer = null;
        private int m_nPayloadSize = 0;
        private int m_nWidth = 0;
        private int m_nHeight = 0;
        private Bitmap m_bitmapForSave = null;
        private const uint PIXEL_FORMATE_BIT = 0x00FF0000;
        private const uint GX_PIXEL_8BIT = 0x00080000;

        private CWin32Bitmaps.BITMAPINFO m_objBitmapInfo = new CWin32Bitmaps.BITMAPINFO();
        private IntPtr m_pBitmapInfo = IntPtr.Zero;
        private bool _disposed = false;

        public GxBitmap(IGXDevice objIGXDevice)
        {
            m_objIGXDevice = objIGXDevice;
            string strValue = null;
            if (null != objIGXDevice)
            {
                m_nPayloadSize = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("PayloadSize").GetValue();
                m_nWidth = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Width").GetValue();
                m_nHeight = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Height").GetValue();

                if (objIGXDevice.GetRemoteFeatureControl().IsImplemented("PixelColorFilter"))
                {
                    strValue = objIGXDevice.GetRemoteFeatureControl().GetEnumFeature("PixelColorFilter").GetValue();
                    if ("None" != strValue)
                    {
                        m_bIsColor = true;
                    }
                }
            }

            m_byRawBuffer = new byte[m_nPayloadSize];
            m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];
            m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];

            __CreateBitmap(out m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor);

            if (m_bIsColor)
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = -m_nHeight;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 24;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;
            }
            else
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = -m_nHeight;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 8;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;

                m_objBitmapInfo.bmiColors = new CWin32Bitmaps.RGBQUAD[256];
                for (int i = 0; i < 256; i++)
                {
                    m_objBitmapInfo.bmiColors[i].rgbBlue = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbGreen = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbRed = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbReserved = (byte)i;
                }
            }
            m_pBitmapInfo = Marshal.AllocHGlobal(2048);
            Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
        }

        ~GxBitmap()
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
                if (m_pBitmapInfo != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(m_pBitmapInfo);
                    m_pBitmapInfo = IntPtr.Zero;
                }
                if (m_bitmapForSave != null)
                {
                    m_bitmapForSave.Dispose();
                    m_bitmapForSave = null;
                }
                _disposed = true;
            }
        }

        public void SaveBmp(IBaseData objIBaseData, string strFilePath)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;

            __UpdateBufferSize(objIBaseData);

            if (null != objIBaseData)
            {
                emValidBits = __GetBestValudBit(objIBaseData.GetPixelFormat());
                if (m_bIsColor)
                {
                    IntPtr pBufferColor = objIBaseData.ConvertToRGB24(emValidBits, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);
                    Marshal.Copy(pBufferColor, m_byColorBuffer, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);
                    __UpdateBitmapForSave(m_byColorBuffer);
                }
                else
                {
                    IntPtr pBufferMono = IntPtr.Zero;
                    if (__IsPixelFormat8(objIBaseData.GetPixelFormat()))
                    {
                        pBufferMono = objIBaseData.GetBuffer();
                    }
                    else
                    {
                        pBufferMono = objIBaseData.ConvertToRaw8(emValidBits);
                    }
                    Marshal.Copy(pBufferMono, m_byMonoBuffer, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);
                    __UpdateBitmapForSave(m_byMonoBuffer);
                }
                m_bitmapForSave.Save(strFilePath, ImageFormat.Bmp);
            }
        }

        public void SaveRaw(IBaseData objIBaseData, string strFilePath)
        {
            using (Stream objFileStream = new FileStream(strFilePath, FileMode.Create))
            using (BinaryWriter objSW = new BinaryWriter(objFileStream))
            {
                __UpdateBufferSize(objIBaseData);

                if (null != objIBaseData)
                {
                    IntPtr pBufferRaw = objIBaseData.GetBuffer();
                    Marshal.Copy(pBufferRaw, m_byRawBuffer, 0, m_nPayloadSize);
                }

                objSW.Write(m_byRawBuffer);
            }
        }

        public Bitmap GetBmp(IBaseData objIBaseData)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;

            __UpdateBufferSize(objIBaseData);

            if (null != objIBaseData)
            {
                emValidBits = __GetBestValudBit(objIBaseData.GetPixelFormat());
                if (m_bIsColor)
                {
                    IntPtr pBufferColor = objIBaseData.ConvertToRGB24(emValidBits, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);
                    Marshal.Copy(pBufferColor, m_byColorBuffer, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);
                    __UpdateBitmapForSave(m_byColorBuffer);
                }
                else
                {
                    IntPtr pBufferMono = IntPtr.Zero;
                    if (__IsPixelFormat8(objIBaseData.GetPixelFormat()))
                    {
                        pBufferMono = objIBaseData.GetBuffer();
                    }
                    else
                    {
                        pBufferMono = objIBaseData.ConvertToRaw8(emValidBits);
                    }
                    Marshal.Copy(pBufferMono, m_byMonoBuffer, 0, __GetStride(m_nWidth, m_bIsColor) * m_nHeight);
                    __UpdateBitmapForSave(m_byMonoBuffer);
                }
            }
            return m_bitmapForSave;
        }

        private void __UpdateBufferSize(IBaseData objIBaseData)
        {
            if (null != objIBaseData)
            {
                m_nPayloadSize = (int)objIBaseData.GetPayloadSize();
                m_nWidth = (int)objIBaseData.GetWidth();
                m_nHeight = (int)objIBaseData.GetHeight();

                if (!__IsCompatible(m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor))
                {
                    m_byRawBuffer = new byte[m_nPayloadSize];
                    m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];
                    m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeight];

                    m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                    m_objBitmapInfo.bmiHeader.biHeight = m_nHeight;
                    Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
                }
            }
        }

        private void __UpdateBitmapForSave(byte[] byBuffer)
        {
            if (__IsCompatible(m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor))
            {
                __UpdateBitmap(m_bitmapForSave, byBuffer, m_nWidth, m_nHeight, m_bIsColor);
            }
            else
            {
                __CreateBitmap(out m_bitmapForSave, m_nWidth, m_nHeight, m_bIsColor);
                __UpdateBitmap(m_bitmapForSave, byBuffer, m_nWidth, m_nHeight, m_bIsColor);
            }
        }

        private bool __IsPixelFormat8(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            uint uiPixelFormatEntry = (uint)emPixelFormatEntry;
            return (uiPixelFormatEntry & PIXEL_FORMATE_BIT) == GX_PIXEL_8BIT;
        }

        private GX_VALID_BIT_LIST __GetBestValudBit(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
            switch (emPixelFormatEntry)
            {
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG8:
                    emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
                    break;
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG10:
                    emValidBits = GX_VALID_BIT_LIST.GX_BIT_2_9;
                    break;
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG12:
                    emValidBits = GX_VALID_BIT_LIST.GX_BIT_4_11;
                    break;
                default:
                    break;
            }
            return emValidBits;
        }

        private PixelFormat __GetFormat(bool bIsColor)
        {
            return bIsColor ? PixelFormat.Format24bppRgb : PixelFormat.Format8bppIndexed;
        }

        private int __GetStride(int nWidth, bool bIsColor)
        {
            return bIsColor ? nWidth * 3 : nWidth;
        }

        private bool __IsCompatible(Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            if (bitmap == null
                || bitmap.Height != nHeight
                || bitmap.Width != nWidth
                || bitmap.PixelFormat != __GetFormat(bIsColor))
            {
                return false;
            }
            return true;
        }

        private void __CreateBitmap(out Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            bitmap = new Bitmap(nWidth, nHeight, __GetFormat(bIsColor));
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = colorPalette;
            }
        }

        private void __UpdateBitmap(Bitmap bitmap, byte[] byBuffer, int nWidth, int nHeight, bool bIsColor)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptrBmp = bmpData.Scan0;
            int nImageStride = __GetStride(m_nWidth, bIsColor);
            if (nImageStride == bmpData.Stride)
            {
                Marshal.Copy(byBuffer, 0, ptrBmp, bmpData.Stride * bitmap.Height);
            }
            else
            {
                for (int i = 0; i < bitmap.Height; ++i)
                {
                    Marshal.Copy(byBuffer, i * nImageStride, new IntPtr(ptrBmp.ToInt64() + i * bmpData.Stride), m_nWidth);
                }
            }
            bitmap.UnlockBits(bmpData);
        }
    }
}
