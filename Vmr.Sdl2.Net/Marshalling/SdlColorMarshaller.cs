// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Color), MarshalMode.Default, typeof(SdlColorMarshaller))]
internal static class SdlColorMarshaller
{
    public static Color ConvertToManaged(SdlColor unmanaged)
    {
        return Color.FromArgb(unmanaged.A, unmanaged.R, unmanaged.G, unmanaged.B);
    }

    public static SdlColor ConvertToUnmanaged(Color managed)
    {
        return new SdlColor
        {
            R = managed.R,
            G = managed.G,
            B = managed.B,
            A = managed.A
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }
}
