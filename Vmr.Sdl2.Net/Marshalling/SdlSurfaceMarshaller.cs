// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Graphics;

namespace Vmr.Sdl2.Net.Marshalling;

internal static unsafe class SdlSurfaceMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlSurface
    {
        public SurfaceMasks Flags;
        public SdlPixelFormatMarshaller.SdlPixelFormat* Format;
        public int W;
        public int H;
        public int Pitch;
        public void* Pixels;
        public void* UserData;
        public int Locked;
        private void* _listBlitMap;
        public SdlRectangleMarshaller.SdlRect ClipRect;
        private nint _blitMap;
        public int ReferenceCount;
    }

    public static Surface? ConvertToManaged(SdlSurface* unmanaged, bool ownsHandle)
    {
        return new Surface((nint)unmanaged, ownsHandle);
    }
}
