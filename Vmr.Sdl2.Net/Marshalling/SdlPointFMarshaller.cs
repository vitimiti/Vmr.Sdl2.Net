// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Marshalling;

public static class SdlPointFMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPointF
    {
        public float X;
        public float Y;
    }

    public static PointF ConvertToManaged(SdlPointF unmanaged)
    {
        return new PointF(unmanaged.X, unmanaged.Y);
    }

    public static SdlPointF ConvertToUnmanaged(PointF managed)
    {
        return new SdlPointF { X = managed.X, Y = managed.Y };
    }
}
