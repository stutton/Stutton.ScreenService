using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.ScreenService.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWINFO
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(bool? filler) : this() // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));
        }
    }
}
