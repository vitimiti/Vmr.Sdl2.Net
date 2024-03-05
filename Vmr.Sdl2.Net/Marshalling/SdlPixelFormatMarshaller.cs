// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Graphics.Pixels;

namespace Vmr.Sdl2.Net.Marshalling;

internal static unsafe class SdlPixelFormatMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPixelFormat
    {
        public uint Format;
        public SdlPaletteMarshaller.SdlPalette* Palette;
        public byte BitsPerPixel;
        public byte BytesPerPixel;
        private fixed byte _padding[2];
        public uint RMask;
        public uint GMask;
        public uint BMask;
        public uint AMask;
        public byte RLoss;
        public byte GLoss;
        public byte BLoss;
        public byte ALoss;
        public byte RShift;
        public byte GShift;
        public byte BShift;
        public byte AShift;
        public int RefCount;
        public SdlPixelFormat* Next;
    }

    public static PixelFormat? ConvertToManaged(SdlPixelFormat* unmanaged, bool ownsHandle)
    {
        return unmanaged is null ? null : new PixelFormat((nint)unmanaged, ownsHandle);
    }
}
