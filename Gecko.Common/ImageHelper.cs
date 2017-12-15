using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Anxin.Common
{
    public class ImageHelper
    {
        public enum ImageCutOption
        {
            KeepOriginal = 1,  // 保持原图
            Scale = 2,         // 缩放
            FillWhite = 3,     // 补白边
            FillBlack = 4,     // 补黑边
        }


        public static Regex imageExtReg = new Regex("jpg$|gif$|bmp$|png$", RegexOptions.IgnoreCase);
        /// <summary>
        /// 获取唯一名称（带后缀）
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetUniqName(string fileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string mark = new StringHelper().GenerateCheckCodeNum(16);
            string ext = fileName.Substring(fileName.LastIndexOf("."));
            if (ext.Length < 1) ext = ".jpg";
            return timestamp + mark + ext;
        }

        public static Image TryCreateImage(Stream stream)
        {
            try
            {
                return Image.FromStream(stream);
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <SUMMARY>
        /// 图片缩放
        /// </SUMMARY>
        /// <PARAM name="sourceFile">图片源路径</PARAM>
        /// <PARAM name="destFile">缩放后图片输出路径</PARAM>
        /// <PARAM name="destHeight">缩放后图片高度</PARAM>
        /// <PARAM name="destWidth">缩放后图片宽度</PARAM>
        /// <RETURNS></RETURNS>
        public static bool GetThumbnail(string sourceFile, string destFile, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = System.Drawing.Image.FromFile(sourceFile);
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (destHeight == 0) { destHeight = imgSource.Height; if (destHeight < 200) { destHeight = 200; } }
            if (destWidth == 0) { destWidth = imgSource.Width; if (destWidth < 200) { destWidth = 200; } }
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.WhiteSmoke);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//设置JPEG编码
                        break;
                    }
                }

                if (jpegICI != null)
                {
                    outBmp.Save(destFile, jpegICI, encoderParams);
                }
                else
                {
                    outBmp.Save(destFile, thisFormat);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                imgSource.Dispose();
                outBmp.Dispose();
            }
        }
        /// <SUMMARY>
        /// 图片缩放
        /// </SUMMARY>
        /// <PARAM name="sourceFile">图片源路径</PARAM>
        /// <PARAM name="destFile">缩放后图片输出路径</PARAM>
        /// <PARAM name="destHeight">缩放后图片高度</PARAM>
        /// <PARAM name="destWidth">缩放后图片宽度</PARAM>
        /// <RETURNS></RETURNS>
        public static Bitmap ThumbnailForBitmap(string sourceFile, string destFile, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = null;
            try
            {
                imgSource = System.Drawing.Image.FromFile(sourceFile);
                System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
                int sW = 0, sH = 0;
                // 按比例缩放
                int sWidth = imgSource.Width;
                int sHeight = imgSource.Height;
                if (destHeight == 0) { destHeight = imgSource.Height; if (destHeight < 200) { destHeight = 200; } }
                if (destWidth == 0) { destWidth = imgSource.Width; if (destWidth < 200) { destWidth = 200; } }
                if (sHeight > destHeight || sWidth > destWidth)
                {
                    if ((sWidth * destHeight) > (sHeight * destWidth))
                    {
                        sW = destWidth;
                        sH = (destWidth * sHeight) / sWidth;
                    }
                    else
                    {
                        sH = destHeight;
                        sW = (sWidth * destHeight) / sHeight;
                    }
                }
                else
                {
                    sW = sWidth;
                    sH = sHeight;
                }

                Bitmap outBmp = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.WhiteSmoke);

                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
                g.Dispose();

                // 以下代码为保存图片时，设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;

                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;


                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//设置JPEG编码
                        break;
                    }
                }

                //if (jpegICI != null)
                //{
                //    outBmp.Save(destFile, jpegICI, encoderParams);
                //}
                //else
                //{
                //    outBmp.Save(destFile, thisFormat);
                //}

                return outBmp;
            }
            catch (Exception e)
            {
                Anxin.Common.FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                return null;

            }
            finally
            {
                imgSource.Dispose();

            }
        }
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="sourceStream">Stream 图片流对象</param>
        /// <param name="destHeight">缩放后图片高度</param>
        /// <param name="destWidth">缩放后图片宽度</param>
        /// <returns></returns>
        public static Bitmap ThumbnailForStream(Stream sourceStream, int destHeight, int destWidth)
        {
            Image imgSource = null;
            try
            {
                imgSource = Image.FromStream(sourceStream);
                ImageFormat thisFormat = imgSource.RawFormat;
                int sW = 0, sH = 0;
                // 按比例缩放
                int sWidth = imgSource.Width;
                int sHeight = imgSource.Height;
                if (destHeight == 0) { destHeight = imgSource.Height; if (destHeight < 200) { destHeight = 200; } }
                if (destWidth == 0) { destWidth = imgSource.Width; if (destWidth < 200) { destWidth = 200; } }
                if (sHeight > destHeight || sWidth > destWidth)
                {
                    if ((sWidth * destHeight) > (sHeight * destWidth))
                    {
                        sW = destWidth;
                        sH = (destWidth * sHeight) / sWidth;
                    }
                    else
                    {
                        sH = destHeight;
                        sW = (sWidth * destHeight) / sHeight;
                    }
                }
                else
                {
                    sW = sWidth;
                    sH = sHeight;
                }

                Bitmap outBmp = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.WhiteSmoke);

                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
                g.Dispose();

                //// 以下代码为保存图片时，设置压缩质量
                //EncoderParameters encoderParams = new EncoderParameters();
                //long[] quality = new long[1];
                //quality[0] = 100;

                //EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                //encoderParams.Param[0] = encoderParam;

                ////获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                //ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                //ImageCodecInfo jpegICI = null;
                //for (int x = 0; x < arrayICI.Length; x++)
                //{
                //    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                //    {
                //        jpegICI = arrayICI[x];//设置JPEG编码
                //        break;
                //    }
                //}
                return outBmp;
            }
            catch (Exception e)
            {
                //FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                LOG.Trace(LOG.ST.Day, "ImageHelper", String.Format("{0},{1}", e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                if (imgSource != null)
                {
                    imgSource.Dispose();
                }
            }
        }
        /// <summary>
        /// 对图片进行裁剪 yzx
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <param name="guize"></param>
        /// <returns></returns>
        public static Bitmap ThumbnailForStream(Stream sourceStream, int destHeight, int destWidth, int guize)
        {
            Image imgSource = null;
            try
            {
                imgSource = Image.FromStream(sourceStream);
                return ThumbnailForImage(imgSource, destWidth, destHeight);

            }
            catch (Exception e)
            {
                //FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                LOG.Trace(LOG.ST.Day, "ImageHelper", String.Format("{0},{1}", e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                if (imgSource != null)
                {
                    imgSource.Dispose();
                }
            }
        }

        public static Bitmap ThumbnailForImage(Image imgSource, int destWidth, int destHeight)
        {
            try
            {
                ImageFormat thisFormat = imgSource.RawFormat;

                //原图宽高
                int imageFromWidth = imgSource.Width;
                int imageFromHeight = imgSource.Height;
                int width = destWidth;
                int height = destHeight;
                //生成缩略图宽高
                int bitmapWidth = width;
                int bitmapHeight = height;

                //缩略图在"画布"上位置
                int X = 0;
                int Y = 0;
                int A = 0;
                int B = 0;
                int Chight = imageFromHeight;
                int Cwidth = imageFromWidth;
                //根据原图和素略图的比例，计算缩略图的实际大小和在缩略图上的位置
                if ((Convert.ToDouble(imageFromHeight) / Convert.ToDouble(imageFromWidth)) > (Convert.ToDouble(height) / Convert.ToDouble(width)))
                {
                    Chight = Convert.ToInt32(imageFromWidth * (Convert.ToDouble(height) / Convert.ToDouble(width)));
                    A = (imageFromHeight - Chight) / 2;
                }
                else if ((Convert.ToDouble(imageFromHeight) / Convert.ToDouble(imageFromWidth)) < (Convert.ToDouble(height) / Convert.ToDouble(width)))
                {
                    Cwidth = Convert.ToInt32(imageFromHeight * (Convert.ToDouble(height) / Convert.ToDouble(width)));
                    B = (imageFromWidth - Cwidth) / 2;

                }

                Bitmap outBmp = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(outBmp);
                g.Clear(Color.WhiteSmoke);

                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgSource, new Rectangle(X, Y, bitmapWidth, bitmapHeight), new Rectangle(B, A, Cwidth, Chight), GraphicsUnit.Pixel);
                g.Dispose();
                return outBmp;
            }
            catch (Exception e)
            {
                //FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                LOG.Trace(LOG.ST.Day, "ImageHelper", String.Format("{0},{1}", e.Message, e.StackTrace));
                return null;
            }
            finally
            {
                if (imgSource != null)
                {
                    imgSource.Dispose();
                }
            }
        }

        /// <summary>
        /// 如果为橫图，则根据width缩放，否则根据height缩放。如果图片比缩放结果小，则不处理。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <returns></returns>
        public static Image CreateAndScaleByLargeBorder(Image image, int destWidth, int destHeight)
        {
            if (image.Width >= image.Height && image.Width > destWidth)
            {
                return CreateAndScaleImage(image, destWidth, -1);
            } 
            else if (image.Width < image.Height && image.Height > destWidth)
            {
                return CreateAndScaleImage(image, -1, destHeight);
            }

            return image;
        }

        /// <summary>
        /// 缩放并创建bitmap, 如果 destWidth, destHeight 其中一个为 -1, 则为保持长宽比缩放
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <returns></returns>
        public static Bitmap CreateAndScaleImage(Stream stream, int destWidth, int destHeight)
        {
            try
            {
                var imgSource = Image.FromStream(stream);
                return CreateAndScaleImage(imgSource, destWidth, destHeight);
            }
            catch (Exception e)
            {
                FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                return null;
            }
        }

        public static Bitmap CreateAndScaleImage(Image imgSource, int destWidth, int destHeight)
        {
            try
            {
                ImageFormat thisFormat = imgSource.RawFormat;
                int sW = 0, sH = 0;
                // 按比例缩放
                int sWidth = imgSource.Width;
                int sHeight = imgSource.Height;

                // 如果没有设置长度/宽度，则使用宽度/长度按原始比例缩放
                if (destWidth == -1 && destHeight == -1)
                {
                    return new Bitmap(imgSource, sWidth, sHeight);
                }
                else if (destWidth == -1)
                {
                    if (sHeight == 0)
                    {
                        destWidth = 0;
                    }
                    else
                    {
                        double ratio = (double)destHeight / sHeight;
                        destWidth = Convert.ToInt32(sWidth * ratio);
                    }

                }
                else if (destHeight == -1)
                {
                    if (sWidth == 0)
                    {
                        destHeight = 0;
                    }
                    else
                    {
                        double ratio = (double)destWidth / sWidth;
                        destHeight = Convert.ToInt32(sHeight * ratio);
                    }
                }
                else
                { }


                if (destHeight == 0)
                {
                    destHeight = imgSource.Height;
                }

                if (destWidth == 0)
                {
                    destWidth = imgSource.Width;
                }
                if (sHeight > destHeight || sWidth > destWidth)
                {
                    if ((sWidth * destHeight) > (sHeight * destWidth))
                    {
                        sW = destWidth;
                        sH = (destWidth * sHeight) / sWidth;
                    }
                    else
                    {
                        sH = destHeight;
                        sW = (sWidth * destHeight) / sHeight;
                    }
                }
                else
                {
                    sW = sWidth;
                    sH = sHeight;
                }

                Bitmap outBmp = new Bitmap(destWidth, destHeight);
                using (Graphics g = Graphics.FromImage(outBmp))
                {
                    g.Clear(Color.WhiteSmoke);

                    // 设置画布的描绘质量
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
                    g.Dispose();
                }

                return outBmp;
            }
            catch (Exception e)
            {
                FileHelper.WriteTextFile(System.Web.HttpContext.Current.Server.MapPath("~/") + "xx.txt", e.Message, false);
                return null;
            }
        }

        public static void BytesToImage(byte[] Bytes, string fileName)
        {
            MemoryStream stream = null;
            try
            {
                fileName = fileName.Replace("/", "\\");
                string path = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                if (!Directory.Exists(path))
                { Directory.CreateDirectory(path); }
                stream = new MemoryStream(Bytes);
                Bitmap tempImage = new Bitmap(Image.FromStream(stream));
                tempImage.Save(fileName, ImageFormat.Jpeg);
                tempImage.Dispose();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

        }

        public static byte[] ImageToBytes(Image image)
        {
            return ImageToBytes(image, ImageFormat.Jpeg);
        }

        public static byte[] ImageToBytes(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] buffer = ms.GetBuffer();
                ms.Close();
                return buffer;
            }
        }

        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            return BitmapToBytes(bitmap, bitmap.RawFormat);
        }

        public static byte[] BitmapToBytes(Bitmap bitmap, ImageFormat format)
        {
            return ImageToBytes(bitmap, format);
        }

        public static bool GetThumbnail(Stream str, string destFile, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = System.Drawing.Image.FromStream(str);
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.WhiteSmoke);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//设置JPEG编码
                        break;
                    }
                }

                if (jpegICI != null)
                {
                    outBmp.Save(destFile, jpegICI, encoderParams);
                }
                else
                {
                    outBmp.Save(destFile, thisFormat);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                imgSource.Dispose();
                outBmp.Dispose();
            }
        }

        // 根据扩展名判断图片是否合法
        public static bool CheckLegalImage(string fileEx)
        {
            if (fileEx.Length > 10)
            {
                return false;
            }
            return imageExtReg.IsMatch(fileEx);
        }

        public static ImageFormat GetImageFormatByExtName(string fileExtName)
        {
            if (fileExtName.EndsWith("bmp"))
            {
                return ImageFormat.Bmp;
            }
            else if (fileExtName.EndsWith("jpg"))
            {
                return ImageFormat.Jpeg;
            }
            else if (fileExtName.EndsWith("png"))
            {
                return ImageFormat.Png;
            }
            else if (fileExtName.EndsWith("gif"))
            {
                return ImageFormat.Gif;
            }

            return ImageFormat.Jpeg;
        }


        /// <summary>
        /// Get the image extension name
        /// </summary>
        /// <param name="format"></param>
        /// <returns>return value format is "." + "formatName", default is .jpg </returns>
        public static string GetExtNameByImageFormat(ImageFormat format)
        {
            if (format == ImageFormat.Jpeg)
            {
                return ".jpg";
            }
            else if (format == ImageFormat.Gif)
            {
                return ".gif";
            }
            else if (format == ImageFormat.Png)
            {
                return ".png";
            }
            else if (format == ImageFormat.Bmp)
            {
                return ".bmp";
            }
            else
            {
                return ".jpg";
            }
        }



        private static System.Drawing.Text.PrivateFontCollection fm = null;
        private static System.Drawing.FontFamily FML
        {
            get
            {
                if (fm == null)
                {
                    fm = new System.Drawing.Text.PrivateFontCollection();
                    string file = string.Empty;
                    if (System.Web.HttpContext.Current != null)
                    {
                        file = System.Web.HttpContext.Current.Server.MapPath(".") + "\\jl.ttf";
                    }
                    else
                    {
                        file = System.Windows.Forms.Application.StartupPath + "\\jl.ttf";
                    }
                    FileInfo fi = new FileInfo(file);
                    if (!fi.Exists) { file = @"C:\Windows\Fonts\simfang.ttf"; }
                    fm.AddFontFile(file);
                }
                return fm.Families[0];
            }
        }

        /// <summary>
        /// 将文字转换成图片
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap CreateImage(string text, int fontsize)
        {
            int len = text.Length;
            int height = fontsize;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(height * len + 70, height + 52);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
            {
                using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
                {
                    g.Clear(System.Drawing.Color.White);
                    g.DrawString(text, new System.Drawing.Font(FML, fontsize), b, new System.Drawing.PointF(20, 22));
                }
            }
            return image;
        }

        /// <summary>
        /// 将图片 使用base 64 进行编码
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64(System.Drawing.Bitmap image)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imageBytes = ms.ToArray(); // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}