using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stutton.ScreenService
{
    public static class ScreenService
    {
        public static Rectangle GetScreenRectangle(bool force = false)
        {
            int left = 0, top = 0, width = 0, height = 0;
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Bounds.Left < left)
                {
                    left = screen.Bounds.Left;
                }
                if (screen.Bounds.Top < top)
                {
                    top = screen.Bounds.Top;
                }
                width += screen.Bounds.Width;
                height += screen.Bounds.Height;
            }
            return new Rectangle(left, top, width, height);
        }
    }
}
