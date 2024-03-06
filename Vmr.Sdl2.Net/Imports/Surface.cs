// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Graphics.Blending;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateRGBSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateRgbSurface(
        uint flags,
        int width,
        int height,
        int depth,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateRgbSurfaceWithFormat(
        uint flags,
        int width,
        int height,
        int depth,
        uint format
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateRgbSurfaceFrom(
        void* pixels,
        int width,
        int height,
        int depth,
        int pitch,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormatFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateRgbSurfaceWithFormatFrom(
        void* pixels,
        int width,
        int height,
        int depth,
        int pitch,
        uint format
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_FreeSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeSurface(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfacePalette(
        nint surface,
        [MarshalUsing(typeof(SafeHandleMarshaller<Palette>))] Palette? palette
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LockSurface(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSurface(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_LoadBMP_RW")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint LoadBmpRw(
        [MarshalUsing(typeof(SafeHandleMarshaller<RwOps>))] RwOps src,
        [MarshalUsing(typeof(IntBoolMarshaller))] bool freeSrc
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SaveBMP_RW")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SaveBmpRw(
        nint surface,
        [MarshalUsing(typeof(SafeHandleMarshaller<RwOps>))] RwOps dst,
        [MarshalUsing(typeof(IntBoolMarshaller))] bool freeDst
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceRle(
        nint surface,
        [MarshalUsing(typeof(IntBoolMarshaller))] bool flag
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool HasSurfaceRle(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetColorKey(
        nint surface,
        [MarshalUsing(typeof(IntBoolMarshaller))] bool enabled,
        uint key
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool HasColorKey(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetColorKey(nint surface, out uint key);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceColorMod(nint surface, byte r, byte g, byte b);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceColorMod(nint surface, out byte r, out byte g, out byte b);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceAlphaMod(nint surface, byte a);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceAlphaMod(nint surface, out byte a);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceBlendMode(nint surface, BlendMode blendMode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceBlendMode(nint surface, out BlendMode blendMode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool SetClipRect(nint surface, SdlRectangleMarshaller.SdlRect* rect);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetClipRect(nint surface, SdlRectangleMarshaller.SdlRect* rect);

    [LibraryImport(LibraryName, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint DuplicateSurface(nint surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint ConvertSurface(
        nint src,
        [MarshalUsing(typeof(SafeHandleMarshaller<PixelFormat>))] PixelFormat format,
        uint flags
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_ConvertSurfaceFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint ConvertSurfaceFormat(nint src, uint pixelFormat, uint flags);

    [LibraryImport(LibraryName, EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ConvertPixels(
        int width,
        int height,
        uint srcFormat,
        void* src,
        int srcPitch,
        uint dstFormat,
        void* dst,
        int dstPitch
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_PremultiplyAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PremultiplyAlpha(
        int width,
        int height,
        uint srcFormat,
        void* src,
        int srcPitch,
        uint dstFormat,
        void* dst,
        int dstPitch
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_FillRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FillRect(nint src, SdlRectangleMarshaller.SdlRect* rect, uint color);

    [LibraryImport(LibraryName, EntryPoint = "SDL_FillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FillRects(
        nint src,
        SdlRectangleMarshaller.SdlRect* rects,
        int count,
        uint color
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpperBlit(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_LowerBlit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LowerBlit(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SoftStretch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SoftStretch(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRet
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SoftStretchLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SoftStretchLinear(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRet
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpperBlitScaled(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_LowerBlitScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LowerBlitScaled(
        nint src,
        SdlRectangleMarshaller.SdlRect* srcRect,
        nint dst,
        SdlRectangleMarshaller.SdlRect* dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetYUVConversionMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetYuvConversionMode(YuvConversionMode mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetYUVConversionMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial YuvConversionMode GetYuvConversionMode();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetYUVConversionModeForResolution")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial YuvConversionMode GetYuvConversionModeForResolution(
        int width,
        int height
    );
}
