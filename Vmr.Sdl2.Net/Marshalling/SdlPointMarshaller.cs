// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class SdlPointMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPoint
    {
        public int X;
        public int Y;
    }

    public static Point ConvertToManaged(SdlPoint unmanaged)
    {
        return new Point(unmanaged.X, unmanaged.Y);
    }

    public static SdlPoint ConvertToUnmanaged(Point managed)
    {
        return new SdlPoint { X = managed.X, Y = managed.Y };
    }
}
