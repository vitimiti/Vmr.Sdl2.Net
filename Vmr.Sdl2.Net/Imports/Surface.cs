// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Graphics.Blending;
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
    public static partial int SetSurfacePalette(Graphics.Surface surface, Graphics.Palette palette);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfacePalette(Graphics.Surface surface, nint palette);

    [LibraryImport(LibraryName, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LockSurface(Graphics.Surface surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSurface(Graphics.Surface surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_LoadBMP_RW")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint LoadBmpRw(
        RwOps src,
        [MarshalUsing(typeof(IntBoolMarshaller))]
        bool freeSrc
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SaveBMP_RW")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SaveBmpRw(
        Graphics.Surface surface,
        RwOps dst,
        [MarshalUsing(typeof(IntBoolMarshaller))]
        bool freeDst
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceRle(
        Graphics.Surface surface,
        [MarshalUsing(typeof(IntBoolMarshaller))]
        bool flag
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool HasSurfaceRle(Graphics.Surface surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetColorKey(
        Graphics.Surface surface,
        [MarshalUsing(typeof(IntBoolMarshaller))]
        bool enabled,
        uint key
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool HasColorKey(Graphics.Surface surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetColorKey(Graphics.Surface surface, out uint key);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceColorMod(Graphics.Surface surface, byte r, byte g, byte b);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceColorMod(
        Graphics.Surface surface,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceAlphaMod(Graphics.Surface surface, byte a);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceAlphaMod(Graphics.Surface surface, out byte a);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetSurfaceBlendMode(Graphics.Surface surface, BlendMode blendMode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSurfaceBlendMode(
        Graphics.Surface surface,
        out BlendMode blendMode
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool SetClipRect(
        Graphics.Surface surface,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle rect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetClipRect(
        Graphics.Surface surface,
        [MarshalUsing(typeof(RectangleMarshaller))]
        out Rectangle rect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint DuplicateSurface(Graphics.Surface surface);

    [LibraryImport(LibraryName, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint ConvertSurface(
        Graphics.Surface src,
        Graphics.Pixels.PixelFormat format,
        uint flags
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_ConvertSurfaceFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint ConvertSurfaceFormat(
        Graphics.Surface src,
        uint pixelFormat,
        uint flags
    );

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
    public static partial int FillRect(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle rect,
        uint color
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_FillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FillRects(
        Graphics.Surface src,
        RectangleMarshaller.Rectangle* rects,
        int count,
        uint color
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpperBlit(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_LowerBlit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LowerBlit(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SoftStretch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SoftStretch(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle dstRet
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SoftStretchLinear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SoftStretchLinear(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle dstRet
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpperBlitScaled(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle dstRect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_LowerBlitScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LowerBlitScaled(
        Graphics.Surface src,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle srcRect,
        Graphics.Surface dst,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle dstRect
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

    [StructLayout(LayoutKind.Sequential)]
    internal struct Surface
    {
        public SurfaceModes Flags;
        public PixelFormat* Format;
        public int W;
        public int H;
        public int Pitch;
        public void* Pixels;
        public void* UserData;
        public int Locked;
        private readonly void* _listBlitMap;
        public RectangleMarshaller.Rectangle ClipRect;
        private readonly nint _blitMap;
        public int ReferenceCount;
    }
}
