// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Graphics;

namespace Vmr.Sdl2.Net.Marshalling;

internal static unsafe class SdlPaletteMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPalette
    {
        public int NColors;
        public SdlColorMarshaller.SdlColor* Colors;
        public uint Version;
        public int RefCount;
    }

    public static Palette? ConvertToManaged(SdlPalette* unmanaged, bool ownsHandle)
    {
        return unmanaged is null ? null : new Palette((nint)unmanaged, ownsHandle);
    }
}
