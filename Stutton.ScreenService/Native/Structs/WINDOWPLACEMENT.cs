using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.ScreenService.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPLACEMENT
    {
        public uint Length;
        public uint Flags;
        public uint ShowCmd;
        public Point PtMinPosition;
        public Point PtMaxPosition;
        public RECT RcNormalPosition;
    }
}
