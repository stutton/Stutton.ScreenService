using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.ScreenService.Native
{
    internal static class Callbacks
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    }
}
