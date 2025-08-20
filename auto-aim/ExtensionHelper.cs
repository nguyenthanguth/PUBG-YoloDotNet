using SkiaSharp;
using System.Drawing.Imaging;

namespace auto_aim
{
    public static class ExtensionHelper
    {
        #region custom
        public static MemoryStream ToMemoryStream(this Bitmap bitmap)
        {
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }

        public static void DrawCross(this SKBitmap sKBitmap, int x_center, int y_center, int size_cross, SKColor color)
        {
            using (SKCanvas canvas = new SKCanvas(sKBitmap))
            {
                using SKPaint paint = new SKPaint
                {
                    Color = color,
                    StrokeWidth = 2,
                    IsAntialias = true
                };
                canvas.DrawLine(x_center - size_cross, y_center, x_center + size_cross, y_center, paint); // draw ngang
                canvas.DrawLine(x_center, y_center - size_cross, x_center, y_center + size_cross, paint); // draw dọc
            }
        }
        #endregion

        public static SKPoint ToSKPoint(this PointF point)
        {
            return new SKPoint(point.X, point.Y);
        }

        public static SKPointI ToSKPoint(this Point point)
        {
            return new SKPointI(point.X, point.Y);
        }

        public static PointF ToDrawingPoint(this SKPoint point)
        {
            return new PointF(point.X, point.Y);
        }

        public static Point ToDrawingPoint(this SKPointI point)
        {
            return new Point(point.X, point.Y);
        }

        public static SKRect ToSKRect(this RectangleF rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static SKRectI ToSKRect(this Rectangle rect)
        {
            return new SKRectI(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static RectangleF ToDrawingRect(this SKRect rect)
        {
            return RectangleF.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static Rectangle ToDrawingRect(this SKRectI rect)
        {
            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static SKSize ToSKSize(this SizeF size)
        {
            return new SKSize(size.Width, size.Height);
        }

        public static SKSizeI ToSKSize(this Size size)
        {
            return new SKSizeI(size.Width, size.Height);
        }

        public static SizeF ToDrawingSize(this SKSize size)
        {
            return new SizeF(size.Width, size.Height);
        }

        public static Size ToDrawingSize(this SKSizeI size)
        {
            return new Size(size.Width, size.Height);
        }

        public static Bitmap ToBitmap(this SKPicture picture, SKSizeI dimensions)
        {
            using SKImage skiaImage = SKImage.FromPicture(picture, dimensions);
            return skiaImage.ToBitmap();
        }

        public static Bitmap ToBitmap(this SKImage skiaImage)
        {
            Bitmap bitmap = new Bitmap(skiaImage.Width, skiaImage.Height, PixelFormat.Format32bppPArgb);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            using (SKPixmap pixmap = new SKPixmap(new SKImageInfo(bitmapData.Width, bitmapData.Height), bitmapData.Scan0, bitmapData.Stride))
            {
                skiaImage.ReadPixels(pixmap, 0, 0);
            }

            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static Bitmap ToBitmap(this SKBitmap skiaBitmap)
        {
            using SKPixmap pixmap = skiaBitmap.PeekPixels();
            using SKImage skiaImage = SKImage.FromPixels(pixmap);
            Bitmap result = skiaImage.ToBitmap();
            GC.KeepAlive(skiaBitmap);
            return result;
        }

        public static Bitmap ToBitmap(this SKPixmap pixmap)
        {
            using SKImage skiaImage = SKImage.FromPixels(pixmap);
            return skiaImage.ToBitmap();
        }

        public static SKBitmap ToSKBitmap(this Bitmap bitmap)
        {
            SKImageInfo info = new SKImageInfo(bitmap.Width, bitmap.Height);
            SKBitmap sKBitmap = new SKBitmap(info);
            using SKPixmap pixmap = sKBitmap.PeekPixels();
            bitmap.ToSKPixmap(pixmap);
            return sKBitmap;
        }

        public static SKImage ToSKImage(this Bitmap bitmap)
        {
            SKImageInfo info = new SKImageInfo(bitmap.Width, bitmap.Height);
            SKImage sKImage = SKImage.Create(info);
            using SKPixmap pixmap = sKImage.PeekPixels();
            bitmap.ToSKPixmap(pixmap);
            return sKImage;
        }

        public static void ToSKPixmap(this Bitmap bitmap, SKPixmap pixmap)
        {
            if (pixmap.ColorType == SKImageInfo.PlatformColorType)
            {
                SKImageInfo info = pixmap.Info;
                using Bitmap image = new Bitmap(info.Width, info.Height, info.RowBytes, PixelFormat.Format32bppPArgb, pixmap.GetPixels());
                using Graphics graphics = Graphics.FromImage(image);
                graphics.Clear(Color.Transparent);
                graphics.DrawImageUnscaled(bitmap, 0, 0);
                return;
            }

            using SKImage sKImage = bitmap.ToSKImage();
            sKImage.ReadPixels(pixmap, 0, 0);
        }

        public static SKColor ToSKColor(this Color color)
        {
            return (uint)color.ToArgb();
        }

        public static Color ToDrawingColor(this SKColor color)
        {
            return Color.FromArgb((int)(uint)color);
        }
    }
}
