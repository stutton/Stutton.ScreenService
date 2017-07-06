using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using Size = System.Drawing.Size;

namespace Stutton.ScreenService
{
    public static class Capture
    {
        public static BitmapSource FullScreen()
        {
            var screenRect = ScreenService.GetScreenRectangle();
            using (var screenBmp = new Bitmap(screenRect.Width, screenRect.Height, PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(screenRect.X, screenRect.Y, 0, 0, new Size(screenRect.Width, screenRect.Height));
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }
    }
}
