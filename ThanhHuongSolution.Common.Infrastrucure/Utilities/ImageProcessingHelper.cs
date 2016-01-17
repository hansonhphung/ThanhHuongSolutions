using System;
using System.IO;
using System.Drawing;

namespace ThanhHuongSolution.Common.Infrastrucure.Utilities
{
    public class ImageProcessingHelper
    {
        public static bool TryResize(Stream stream, int? width, int? height, out Stream output)
        {
            if (width == null && height == null)
            {
                throw new Exception("Must define width or height");
            }

            var src = Image.FromStream(stream) as Bitmap;
            if (src != null)
            {
                if (width == null)
                {
                    var ratio = (Convert.ToDouble(height.Value) / src.Height);
                    width = Convert.ToInt32(src.Width * ratio);
                }
                else
                {
                    if (height == null)
                    {
                        var ratio = (Convert.ToDouble(width.Value) / src.Width);
                        height = Convert.ToInt32(src.Height * ratio);
                    }
                }

                Bitmap _bitmap = new Bitmap(width.Value, height.Value);
                using (Graphics g = Graphics.FromImage(_bitmap))
                {
                    g.DrawImage(src, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                        new Rectangle(0, 0, src.Width, src.Height),
                        GraphicsUnit.Pixel);

                    var memoryStream = new MemoryStream();
                    _bitmap.Save(memoryStream, src.RawFormat);

                    memoryStream.Seek(0, 0);
                    output = memoryStream;

                    return true;
                }
            }
            else
            {
                output = new MemoryStream();
                return false;
            }
        }
    }
}
