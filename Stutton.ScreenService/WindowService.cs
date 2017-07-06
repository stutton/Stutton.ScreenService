using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Stutton.ScreenService.Native;
using System.Drawing;

namespace Stutton.ScreenService
{
    public static class WindowService
    {
        private static bool rectContains(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Left <= rect2.Left &&
                   rect1.Top <= rect2.Top &&
                   rect1.Width >= rect2.Width &&
                   rect1.Height >= rect2.Height;
        }

        private static List<WindowInfo> findWindows(Callbacks.EnumWindowsProc filter)
        {
            var windows = new List<IntPtr>();
            User32.EnumWindows((wnd, param) =>
            {
                if (filter(wnd, param))
                {
                    windows.Add(wnd);
                }
                return true;
            }, IntPtr.Zero);

            var result = new List<WindowInfo>();
            int z = 0;
            windows.ForEach(w =>
            {
                User32.GetWindowRect(new HandleRef(null, w), out var rect);
                result.Add(new WindowInfo(w, GetWindowText(w), rect, z));
                z++;
            });

            return result;
        }

        private static string GetWindowText(IntPtr hWnd)
        {
            var size = User32.GetWindowTextLength(hWnd);
            if (size <= 0)
            {
                return string.Empty;
            }
            var builder = new StringBuilder(size + 1);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
        }

        public static IOrderedEnumerable<WindowInfo> GetVisibleWindows()
        {
            var allWindows = findWindows((wnd, param) =>
            {
                var windowTitle = GetWindowText(wnd);
                return User32.IsWindowVisible(wnd) && !string.IsNullOrWhiteSpace(windowTitle);
            });

            var result = new List<WindowInfo>();
            foreach(var window in allWindows)
            {
                if (window.Title == "Program Manager" || window.Title == "Settings")
                {
                    continue;
                }

                User32.GetWindowPlacement(window.GetHandle(), out var windowPlacement);
                if(windowPlacement.ShowCmd == Constants.SW_MINIMIZE)
                {
                    continue;
                }
                User32.GetWindowInfo(window.GetHandle(), out var windowInfo);
                int left, top, width, height;
                top = window.Rect.Top;
                height = window.Rect.Height - (int)windowInfo.cxWindowBorders;
                if (windowPlacement.ShowCmd == Constants.SW_MAXIMIZE)
                {
                    top += (int)windowInfo.cyWindowBorders;
                    height -= (int)windowInfo.cyWindowBorders;
                }
                left = window.Rect.Left + (int)windowInfo.cyWindowBorders;
                width = window.Rect.Width - (int)windowInfo.cxWindowBorders - (int)windowInfo.cyWindowBorders;
                window.Rect = new Rectangle(left, top, width, height);

                if(!result.Any(r => rectContains(r.Rect, window.Rect)))
                {
                    result.Add(window);
                }
            }
            return result.OrderBy(w => w.ZIndex);
        }
    }
}
