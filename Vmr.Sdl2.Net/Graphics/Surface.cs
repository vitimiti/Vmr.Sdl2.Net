// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Graphics.Blending;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

[Serializable]
public class Surface : SafeHandleZeroOrMinusOneIsInvalid
{
    private int _userDataSize;

    public static YuvConversionMode YuvConversionMode
    {
        get => Sdl.GetYuvConversionMode();
        set => Sdl.SetYuvConversionMode(value);
    }

    public SurfaceMasks Flags
    {
        get
        {
            unsafe
            {
                return ((SdlSurfaceMarshaller.SdlSurface*)handle)->Flags;
            }
        }
    }

    public PixelFormat? PixelFormat
    {
        get
        {
            unsafe
            {
                return SdlPixelFormatMarshaller.ConvertToManaged(
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->Format,
                    false
                );
            }
        }
    }

    public Size Size
    {
        get
        {
            unsafe
            {
                return new Size(
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->W,
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->H
                );
            }
        }
    }

    public int Pitch
    {
        get
        {
            unsafe
            {
                return ((SdlSurfaceMarshaller.SdlSurface*)handle)->Pitch;
            }
        }
    }

    public byte[]? Pixels
    {
        get
        {
            unsafe
            {
                if (((SdlSurfaceMarshaller.SdlSurface*)handle)->Pixels is null)
                {
                    return null;
                }

                int size =
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->W
                    * ((SdlSurfaceMarshaller.SdlSurface*)handle)->H
                    * sizeof(byte);

                byte* pixelsHandle = (byte*)((SdlSurfaceMarshaller.SdlSurface*)handle)->Pixels;
                var pixels = new byte[size];
                for (int i = 0; i < size; i++)
                {
                    pixels[i] = pixelsHandle[i];
                }

                return pixels;
            }
        }
        set
        {
            unsafe
            {
                fixed (byte* pixelsHandle = value)
                {
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->Pixels = pixelsHandle;
                }
            }
        }
    }

    public byte[]? UserData
    {
        get
        {
            unsafe
            {
                if (((SdlSurfaceMarshaller.SdlSurface*)handle)->UserData is null)
                {
                    return null;
                }

                if (_userDataSize == 0)
                {
                    return Array.Empty<byte>();
                }

                byte* userDataHandle = (byte*)((SdlSurfaceMarshaller.SdlSurface*)handle)->Pixels;
                var userData = new byte[_userDataSize];
                for (int i = 0; i < _userDataSize; i++)
                {
                    userData[i] = userDataHandle[i];
                }

                return userData;
            }
        }
        set
        {
            unsafe
            {
                fixed (byte* pixelsHandle = value)
                {
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->Pixels = pixelsHandle;
                    _userDataSize = value?.Length ?? 0;
                }
            }
        }
    }

    public bool Locked
    {
        get
        {
            unsafe
            {
                return IntBoolMarshaller.ConvertToManaged(
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->Locked
                );
            }
        }
    }

    public Rectangle ClipRectangle
    {
        get
        {
            unsafe
            {
                return SdlRectangleMarshaller.ConvertToManaged(
                    ((SdlSurfaceMarshaller.SdlSurface*)handle)->ClipRect
                );
            }
        }
    }

    public int ReferenceCount
    {
        get
        {
            unsafe
            {
                return ((SdlSurfaceMarshaller.SdlSurface*)handle)->ReferenceCount;
            }
        }
    }

    public bool HasRle => Sdl.HasSurfaceRle(handle);
    public bool HasColorKey => Sdl.HasColorKey(handle);

    internal Surface(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Surface(Size size, int depth, ColorMasks masks, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.CreateRgbSurface(
            0,
            size.Width,
            size.Height,
            depth,
            masks.Red,
            masks.Green,
            masks.Blue,
            masks.Alpha
        );

        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Surface(Size size, int depth, uint format, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.CreateRgbSurfaceWithFormat(0, size.Width, size.Height, depth, format);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Surface(
        byte[] pixels,
        Size size,
        int depth,
        int pitch,
        ColorMasks masks,
        ErrorHandler errorHandler
    )
        : base(true)
    {
        unsafe
        {
            fixed (byte* pixelsHandle = pixels)
            {
                handle = Sdl.CreateRgbSurfaceFrom(
                    pixelsHandle,
                    size.Width,
                    size.Height,
                    depth,
                    pitch,
                    masks.Red,
                    masks.Green,
                    masks.Blue,
                    masks.Alpha
                );
            }
        }

        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Surface(
        byte[] pixels,
        Size size,
        int depth,
        int pitch,
        uint format,
        ErrorHandler errorHandler
    )
        : base(true)
    {
        unsafe
        {
            fixed (byte* pixelsHandle = pixels)
            {
                handle = Sdl.CreateRgbSurfaceWithFormatFrom(
                    pixelsHandle,
                    size.Width,
                    size.Height,
                    depth,
                    pitch,
                    format
                );
            }
        }

        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreeSurface(handle);
        handle = nint.Zero;
        return true;
    }

    public static Surface? LoadBmp(RwOps src, ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.LoadBmpRw(src, false);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static YuvConversionMode GetYuvConversionModeForResolution(Size resolution)
    {
        return Sdl.GetYuvConversionModeForResolution(resolution.Width, resolution.Height);
    }

    public void SetPalette(Palette? palette, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfacePalette(handle, palette);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void Lock(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.LockSurface(handle);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void Unlock()
    {
        Sdl.UnlockSurface(handle);
    }

    public void SaveBmp(RwOps dst, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SaveBmpRw(handle, dst, false);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetRleEnabled(bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceRle(handle, enabled);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetColorKey(bool enabled, uint key, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetColorKey(handle, enabled, key);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public uint GetColorKey(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetColorKey(handle, out uint key);
        if (code >= 0)
        {
            return key;
        }

        errorHandler(Sdl.GetError(), code);
        return 0;
    }

    public void SetColorMod(Color color, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceColorMod(handle, color.R, color.G, color.B);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }

        code = Sdl.SetSurfaceAlphaMod(handle, color.A);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public Color GetColorMod(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetSurfaceColorMod(handle, out byte r, out byte g, out byte b);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
            return Color.Black;
        }

        code = Sdl.GetSurfaceAlphaMod(handle, out byte a);
        if (code >= 0)
        {
            return Color.FromArgb(a, r, g, b);
        }

        errorHandler(Sdl.GetError(), code);
        return Color.Black;
    }

    public void SetBlendMode(BlendMode blendMode, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceBlendMode(handle, blendMode);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public BlendMode GetBlendMode(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetSurfaceBlendMode(handle, out BlendMode blendMode);
        if (code >= 0)
        {
            return blendMode;
        }

        errorHandler(Sdl.GetError(), code);
        return BlendMode.Invalid;
    }

    public void SetClipRectangle(Rectangle clip, ErrorHandler errorHandler)
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlClip = SdlRectangleMarshaller.ConvertToUnmanaged(
                clip
            );

            bool isValid = Sdl.SetClipRect(handle, &sdlClip);
            if (!isValid)
            {
                errorHandler(Sdl.GetError());
            }
        }
    }

    public Rectangle GetClipRectangle()
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect result = new();
            Sdl.GetClipRect(handle, &result);
            return SdlRectangleMarshaller.ConvertToManaged(result);
        }
    }

    public Surface? Duplicate(ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.DuplicateSurface(handle);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public Surface? Convert(PixelFormat format, ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.ConvertSurface(handle, format, 0);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public Surface? Convert(uint pixelFormat, ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.ConvertSurfaceFormat(handle, pixelFormat, 0);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public void Fill(uint color, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            int code = Sdl.FillRect(handle, null, color);
            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }
        }
    }

    public void Fill(Rectangle rectangle, uint color, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                rectangle
            );

            int code = Sdl.FillRect(handle, &sdlRect, color);
            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }
        }
    }

    public void Fill(Rectangle[] rectangles, uint color, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            var sdlRects = new SdlRectangleMarshaller.SdlRect[rectangles.Length];
            for (int i = 0; i < rectangles.Length; i++)
            {
                sdlRects[i] = SdlRectangleMarshaller.ConvertToUnmanaged(rectangles[i]);
            }

            fixed (SdlRectangleMarshaller.SdlRect* sdlRectsHandle = sdlRects)
            {
                int code = Sdl.FillRects(handle, sdlRectsHandle, rectangles.Length, color);
                if (code < 0)
                {
                    errorHandler(Sdl.GetError(), code);
                }
            }
        }
    }

    public void Blit(
        Rectangle srcRect,
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        UpperBlit(srcRect, dst, ref dstRect, doScaledBlit, errorHandler);
    }

    public void Blit(
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        UpperBlit(dst, ref dstRect, doScaledBlit, errorHandler);
    }

    public void Blit(
        Rectangle srcRect,
        Surface dst,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        UpperBlit(srcRect, dst, doScaledBlit, errorHandler);
    }

    public void Blit(Surface dst, bool doScaledBlit, ErrorCodeHandler errorHandler)
    {
        UpperBlit(dst, doScaledBlit, errorHandler);
    }

    public void UpperBlit(
        Rectangle srcRect,
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doScaledBlit
                ? Sdl.UpperBlitScaled(handle, &sdlSrcRect, dst.handle, &sdlDstRect)
                : Sdl.UpperBlit(handle, &sdlSrcRect, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }

            dstRect = SdlRectangleMarshaller.ConvertToManaged(sdlDstRect);
        }
    }

    public void UpperBlit(
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doScaledBlit
                ? Sdl.UpperBlitScaled(handle, null, dst.handle, &sdlDstRect)
                : Sdl.UpperBlit(handle, null, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }

            dstRect = SdlRectangleMarshaller.ConvertToManaged(sdlDstRect);
        }
    }

    public void UpperBlit(
        Rectangle srcRect,
        Surface dst,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            int code = doScaledBlit
                ? Sdl.UpperBlitScaled(handle, &sdlSrcRect, dst.handle, null)
                : Sdl.UpperBlit(handle, &sdlSrcRect, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }
        }
    }

    public void UpperBlit(Surface dst, bool doScaledBlit, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            int code = doScaledBlit
                ? Sdl.UpperBlitScaled(handle, null, dst.handle, null)
                : Sdl.UpperBlit(handle, null, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }
        }
    }

    public void LowerBlit(
        ref Rectangle srcRect,
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doScaledBlit
                ? Sdl.LowerBlitScaled(handle, &sdlSrcRect, dst.handle, &sdlDstRect)
                : Sdl.LowerBlit(handle, &sdlSrcRect, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }

            srcRect = SdlRectangleMarshaller.ConvertToManaged(sdlSrcRect);
            dstRect = SdlRectangleMarshaller.ConvertToManaged(sdlDstRect);
        }
    }

    public void LowerBlit(
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doScaledBlit
                ? Sdl.LowerBlitScaled(handle, null, dst.handle, &sdlDstRect)
                : Sdl.LowerBlit(handle, null, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }

            dstRect = SdlRectangleMarshaller.ConvertToManaged(sdlDstRect);
        }
    }

    public void LowerBlit(
        ref Rectangle srcRect,
        Surface dst,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            int code = doScaledBlit
                ? Sdl.LowerBlitScaled(handle, &sdlSrcRect, dst.handle, null)
                : Sdl.LowerBlit(handle, &sdlSrcRect, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }

            srcRect = SdlRectangleMarshaller.ConvertToManaged(sdlSrcRect);
        }
    }

    public void LowerBlit(Surface dst, bool doScaledBlit, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            int code = doScaledBlit
                ? Sdl.LowerBlitScaled(handle, null, dst.handle, null)
                : Sdl.LowerBlit(handle, null, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), code);
            }
        }
    }

    public void SoftStretch(
        Rectangle srcRect,
        Surface dst,
        Rectangle dstRect,
        bool doLinearStretch,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doLinearStretch
                ? Sdl.SoftStretchLinear(handle, &sdlSrcRect, dst.handle, &sdlDstRect)
                : Sdl.SoftStretch(handle, &sdlSrcRect, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), 0);
            }
        }
    }

    public void SoftStretch(
        Surface dst,
        Rectangle dstRect,
        bool doLinearStretch,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlDstRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                dstRect
            );

            int code = doLinearStretch
                ? Sdl.SoftStretchLinear(handle, null, dst.handle, &sdlDstRect)
                : Sdl.SoftStretch(handle, null, dst.handle, &sdlDstRect);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), 0);
            }
        }
    }

    public void SoftStretch(
        Rectangle srcRect,
        Surface dst,
        bool doLinearStretch,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlSrcRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                srcRect
            );

            int code = doLinearStretch
                ? Sdl.SoftStretchLinear(handle, &sdlSrcRect, dst.handle, null)
                : Sdl.SoftStretch(handle, &sdlSrcRect, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), 0);
            }
        }
    }

    public void SoftStretch(Surface dst, bool doLinearStretch, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            int code = doLinearStretch
                ? Sdl.SoftStretchLinear(handle, null, dst.handle, null)
                : Sdl.SoftStretch(handle, null, dst.handle, null);

            if (code < 0)
            {
                errorHandler(Sdl.GetError(), 0);
            }
        }
    }

    public override string ToString()
    {
        return $"{{Flags: [{Flags}], Pixel Format: {PixelFormat}, Size: {Size}, Pitch: {Pitch}, Pixels: [{string.Join(", ", Pixels ?? Array.Empty<byte>())}], User Data: [{string.Join(", ", UserData ?? Array.Empty<byte>())}], Locked: {Locked}, Clip Rectangle: {ClipRectangle}, Reference Count: {ReferenceCount}, Has RLE: {HasRle}, Has Color Key: {HasColorKey}}}";
    }
}
