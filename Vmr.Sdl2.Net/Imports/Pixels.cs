// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetPixelFormatName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetPixelFormatName(uint format);

    [LibraryImport(LibraryName, EntryPoint = "SDL_PixelFormatEnumToMasks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool PixelFormatEnumToMasks(
        uint format,
        out int bpp,
        out uint rMask,
        out uint gMask,
        out uint bMask,
        out uint aMask
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_MasksToPixelFormatEnum")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MasksToPixelFormatEnum(
        int bpp,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_AllocFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint AllocFormat(uint pixelFormat);

    [LibraryImport(LibraryName, EntryPoint = "SDL_FreeFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeFormat(nint format);

    [LibraryImport(LibraryName, EntryPoint = "SDL_AllocPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint AllocPalette(int nColors);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetPixelFormatPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetPixelFormatPalette(
        nint format,
        [MarshalUsing(typeof(SafeHandleMarshaller<Graphics.Palette>))] Graphics.Palette? palette
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetPaletteColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetPaletteColors(
        nint palette,
        SdlColorMarshaller.SdlColor* colors,
        int firstColor,
        int nColors
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_FreePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreePalette(nint palette);

    [LibraryImport(LibraryName, EntryPoint = "SDL_MapRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapRgb(Graphics.Pixels.PixelFormat format, byte r, byte g, byte b);

    [LibraryImport(LibraryName, EntryPoint = "SDL_MapRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint MapRgba(
        Graphics.Pixels.PixelFormat format,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetRgb(
        uint pixel,
        Graphics.Pixels.PixelFormat format,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetRgba(
        uint pixel,
        Graphics.Pixels.PixelFormat format,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CalculateGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CalculateGammaRamp(
        float gamma,
        [Out]
        [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
            ushort[] ramp
    );

    [StructLayout(LayoutKind.Sequential)]
    internal struct Palette
    {
        public int NColors;
        public SdlColorMarshaller.SdlColor* Colors;
        public uint Version;
        public int RefCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PixelFormat
    {
        public uint Format;
        public Palette* Palette;
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
        public PixelFormat* Next;
    }
}
