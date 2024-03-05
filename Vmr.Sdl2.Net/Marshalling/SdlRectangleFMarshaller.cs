// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Marshalling;

public static class SdlRectangleFMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlRectF
    {
        public float X;
        public float Y;
        public float W;
        public float H;
    }

    public static RectangleF ConvertToManaged(SdlRectF unmanaged)
    {
        return new RectangleF(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    public static SdlRectF ConvertToUnmanaged(RectangleF managed)
    {
        return new SdlRectF
        {
            X = managed.X,
            Y = managed.Y,
            W = managed.Width,
            H = managed.Height
        };
    }
}
