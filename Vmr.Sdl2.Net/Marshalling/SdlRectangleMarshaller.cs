// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class SdlRectangleMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlRect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    public static Rectangle ConvertToManaged(SdlRect unmanaged)
    {
        return new Rectangle(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    public static SdlRect ConvertToUnmanaged(Rectangle managed)
    {
        return new SdlRect
        {
            X = managed.X,
            Y = managed.Y,
            W = managed.Width,
            H = managed.Height
        };
    }
}
