using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace Sinodom.ElevatorCloud.Web
{
    public class ImageHelper
    {
        public static byte[] PictureWatermark(byte[] imageBytes, List<string> watermarkContents)
        {
            byte[] byteArray;
            using (var stream = new MemoryStream(imageBytes))
            {
                var watermarkedStream = new MemoryStream();
                using (var img = Image.FromStream(stream))
                {
                    using (var graphic = Graphics.FromImage(img))
                    {
                        var font = new Font(FontFamily.GenericSansSerif, 100, FontStyle.Bold, GraphicsUnit.Pixel);
                        var color = Color.FromArgb(128, 255, 255, 255);
                        var brush = new SolidBrush(color);
                        StringFormat stringFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Far
                        };

                        int i = 0;
                        foreach (var watermarkContent in watermarkContents)
                        {
                            i += 120;
                            var point = new Point(img.Width - 20, img.Height - i);
                            graphic.DrawString(watermarkContent, font, brush, point, stringFormat);
                        }

                        img.Save(watermarkedStream, ImageFormat.Png);
                        byteArray = watermarkedStream.ToArray();
                    }
                }
            }

            return byteArray;
        }

        public static byte[] Thumbnail(byte[] imageBytes, int destWidth, int destHeight)
        {
            byte[] byteArray;
            using (var stream = new MemoryStream(imageBytes))
            {
                var watermarkedStream = new MemoryStream();
                using (var img = Image.FromStream(stream))
                {
                    var sourceImage = img;
                    int width, height;
                    var sourceWidth = sourceImage.Width;
                    var sourceHeight = sourceImage.Height;
                    if (sourceHeight > destHeight || sourceWidth > destWidth)
                    {
                        if (sourceWidth * destHeight > sourceHeight * destWidth)
                        {
                            width = destWidth;
                            height = destWidth * sourceHeight / sourceWidth;
                        }
                        else
                        {
                            height = destHeight;
                            width = sourceWidth * destHeight / sourceHeight;
                        }
                    }
                    else
                    {
                        width = sourceWidth;
                        height = sourceHeight;
                    }
                    var destBitmap = new Bitmap(destWidth, destHeight);
                    var g = Graphics.FromImage(destBitmap);
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(sourceImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height),
                        0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel);

                    destBitmap.Save(watermarkedStream, ImageFormat.Png);
                    byteArray = watermarkedStream.ToArray();
                }
            }
            return byteArray;
        }
    }
}
