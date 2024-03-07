// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Graphics.Blending;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Surface>))]
public class Surface : SafeHandleZeroOrMinusOneIsInvalid
{
    private int _userDataSize;

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

    private unsafe Sdl.Surface* UnsafeHandle => (Sdl.Surface*)handle;

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
                return UnsafeHandle->Flags;
            }
        }
    }

    public PixelFormat? PixelFormat
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Format is null
                    ? null
                    : new PixelFormat((nint)UnsafeHandle->Format, false);
            }
        }
    }

    public Size Size
    {
        get
        {
            unsafe
            {
                return new Size(UnsafeHandle->W, UnsafeHandle->H);
            }
        }
    }

    public int Pitch
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Pitch;
            }
        }
    }

    public byte[]? Pixels
    {
        get
        {
            unsafe
            {
                if (UnsafeHandle->Pixels is null)
                {
                    return null;
                }

                int size = UnsafeHandle->W * UnsafeHandle->H * sizeof(byte);

                byte* pixelsHandle = (byte*)UnsafeHandle->Pixels;
                byte[] pixels = new byte[size];
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
                    UnsafeHandle->Pixels = pixelsHandle;
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
                if (UnsafeHandle->UserData is null)
                {
                    return null;
                }

                if (_userDataSize == 0)
                {
                    return Array.Empty<byte>();
                }

                byte* userDataHandle = (byte*)UnsafeHandle->Pixels;
                byte[] userData = new byte[_userDataSize];
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
                    UnsafeHandle->Pixels = pixelsHandle;
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
                return IntBoolMarshaller.ConvertToManaged(UnsafeHandle->Locked);
            }
        }
    }

    public Rectangle ClipRectangle
    {
        get
        {
            unsafe
            {
                return SdlRectangleMarshaller.ConvertToManaged(UnsafeHandle->ClipRect);
            }
        }
    }

    public int ReferenceCount
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->ReferenceCount;
            }
        }
    }

    public bool HasRle => Sdl.HasSurfaceRle(this);
    public bool HasColorKey => Sdl.HasColorKey(this);

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
        int code = Sdl.SetSurfacePalette(this, palette);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void Lock(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.LockSurface(this);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void Unlock()
    {
        Sdl.UnlockSurface(this);
    }

    public void SaveBmp(RwOps dst, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SaveBmpRw(this, dst, false);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetRleEnabled(bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceRle(this, enabled);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetColorKey(bool enabled, uint key, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetColorKey(this, enabled, key);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public uint GetColorKey(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetColorKey(this, out uint key);
        if (code >= 0)
        {
            return key;
        }

        errorHandler(Sdl.GetError(), code);
        return 0;
    }

    public void SetColorMod(Color color, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceColorMod(this, color.R, color.G, color.B);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }

        code = Sdl.SetSurfaceAlphaMod(this, color.A);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public Color GetColorMod(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetSurfaceColorMod(this, out byte r, out byte g, out byte b);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
            return Color.Black;
        }

        code = Sdl.GetSurfaceAlphaMod(this, out byte a);
        if (code >= 0)
        {
            return Color.FromArgb(a, r, g, b);
        }

        errorHandler(Sdl.GetError(), code);
        return Color.Black;
    }

    public void SetBlendMode(BlendMode blendMode, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetSurfaceBlendMode(this, blendMode);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public BlendMode GetBlendMode(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetSurfaceBlendMode(this, out BlendMode blendMode);
        if (code >= 0)
        {
            return blendMode;
        }

        errorHandler(Sdl.GetError(), code);
        return BlendMode.Invalid;
    }

    public void SetClipRectangle(Rectangle clip, ErrorHandler errorHandler)
    {
        bool isValid = Sdl.SetClipRect(this, clip);
        if (!isValid)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Rectangle GetClipRectangle()
    {
        Sdl.GetClipRect(this, out Rectangle result);
        return result;
    }

    public Surface? Duplicate(ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.DuplicateSurface(this);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public Surface? Convert(PixelFormat format, ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.ConvertSurface(this, format, 0);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public Surface? Convert(uint pixelFormat, ErrorHandler errorHandler)
    {
        nint surfaceHandle = Sdl.ConvertSurfaceFormat(this, pixelFormat, 0);
        if (surfaceHandle != nint.Zero)
        {
            return new Surface(surfaceHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public void Fill(Rectangle rectangle, uint color, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.FillRect(this, rectangle, color);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void Fill(Rectangle[] rectangles, uint color, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect[] sdlRects = new SdlRectangleMarshaller.SdlRect[
                rectangles.Length
            ];
            for (int i = 0; i < rectangles.Length; i++)
            {
                sdlRects[i] = SdlRectangleMarshaller.ConvertToUnmanaged(rectangles[i]);
            }

            fixed (SdlRectangleMarshaller.SdlRect* sdlRectsHandle = sdlRects)
            {
                int code = Sdl.FillRects(this, sdlRectsHandle, rectangles.Length, color);
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

    public void UpperBlit(
        Rectangle srcRect,
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit,
        ErrorCodeHandler errorHandler
    )
    {
        int code = doScaledBlit
            ? Sdl.UpperBlitScaled(this, srcRect, dst, ref dstRect)
            : Sdl.UpperBlit(this, srcRect, dst, ref dstRect);

        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
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
        int code = doScaledBlit
            ? Sdl.LowerBlitScaled(this, ref srcRect, dst, ref dstRect)
            : Sdl.LowerBlit(this, ref srcRect, dst, ref dstRect);

        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
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
        int code = doLinearStretch
            ? Sdl.SoftStretchLinear(this, srcRect, dst, dstRect)
            : Sdl.SoftStretch(this, srcRect, dst, dstRect);

        if (code < 0)
        {
            errorHandler(Sdl.GetError(), 0);
        }
    }

    public override string ToString()
    {
        return $"{{Flags: [{Flags}], Pixel Format: {PixelFormat}, Size: {Size}, Pitch: {Pitch}, Pixels: [{string.Join(", ", Pixels ?? Array.Empty<byte>())}], User Data: [{string.Join(", ", UserData ?? Array.Empty<byte>())}], Locked: {Locked}, Clip Rectangle: {ClipRectangle}, Reference Count: {ReferenceCount}, Has RLE: {HasRle}, Has Color Key: {HasColorKey}}}";
    }
}
