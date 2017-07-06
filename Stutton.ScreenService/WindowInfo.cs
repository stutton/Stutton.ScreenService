using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stutton.ScreenService
{
    public class WindowInfo
    {
        private IntPtr _handle;

        public WindowInfo(IntPtr handle, string title, Rectangle rect, int zIndex = -1)
        {
            _handle = handle;
            Rect = rect;
            Title = title;
            ZIndex = zIndex;
        }

        public Rectangle Rect { get; internal set; }

        public string Title { get; internal set; }

        public int ZIndex { get; internal set; }

        public IntPtr GetHandle()
        {
            return _handle;
        }
    }
}
