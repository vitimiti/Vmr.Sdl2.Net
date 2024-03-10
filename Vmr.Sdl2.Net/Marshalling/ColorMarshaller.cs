// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Drawing.Color), MarshalMode.Default, typeof(ColorMarshaller))]
internal static class ColorMarshaller
{
    public static System.Drawing.Color ConvertToManaged(Color unmanaged)
    {
        return System.Drawing.Color.FromArgb(unmanaged.A, unmanaged.R, unmanaged.G, unmanaged.B);
    }

    public static Color ConvertToUnmanaged(System.Drawing.Color managed)
    {
        return new Color
        {
            R = managed.R,
            G = managed.G,
            B = managed.B,
            A = managed.A
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }
}
