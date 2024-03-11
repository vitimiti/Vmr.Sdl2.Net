// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Graphics.Blending;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Surface>))]
public class Surface : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<Surface>
{
    private int _userDataSize;

    internal Surface(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Surface(Size size, int depth, ColorMasks masks)
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
            throw new SurfaceException("Unable to create a new surface from the given RGB data");
        }
    }

    public Surface(Size size, int depth, uint format)
        : base(true)
    {
        handle = Sdl.CreateRgbSurfaceWithFormat(0, size.Width, size.Height, depth, format);
        if (handle == nint.Zero)
        {
            throw new SurfaceException(
                "Unable to create a surface from the given RGB data with format"
            );
        }
    }

    public Surface(byte[] pixels, Size size, int depth, int pitch, ColorMasks masks)
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
            throw new SurfaceException(
                "Unable to create a surface from the given RGB surface data"
            );
        }
    }

    public Surface(byte[] pixels, Size size, int depth, int pitch, uint format)
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
            throw new SurfaceException(
                "Unable to create a surface from the given RGB data with format"
            );
        }
    }

    private unsafe Sdl.Surface* UnsafeHandle => (Sdl.Surface*)handle;

    public static YuvConversionMode YuvConversionMode
    {
        get => Sdl.GetYuvConversionMode();
        set => Sdl.SetYuvConversionMode(value);
    }

    public SurfaceModes Flags
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

                Span<byte> userData = new(UnsafeHandle->Pixels, _userDataSize);
                return userData.ToArray();
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
                return RectangleMarshaller.ConvertToManaged(UnsafeHandle->ClipRect);
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

    public bool Equals(Surface? other)
    {
        return other is not null
               && Flags == other.Flags
               && PixelFormat == other.PixelFormat
               && Size == other.Size
               && Pitch == other.Pitch
               && Pixels == other.Pixels
               && HasRle == other.HasRle
               && HasColorKey == other.HasColorKey;
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

    public static Surface LoadBmp(RwOps src)
    {
        nint surfaceHandle = Sdl.LoadBmpRw(src, false);
        if (surfaceHandle == nint.Zero)
        {
            throw new SurfaceException("Unable to load the given BMP");
        }

        return new Surface(surfaceHandle, true);
    }

    public static YuvConversionMode GetYuvConversionModeForResolution(Size resolution)
    {
        return Sdl.GetYuvConversionModeForResolution(resolution.Width, resolution.Height);
    }

    public void SetPalette(Palette? palette)
    {
        int code = palette is null
            ? Sdl.SetSurfacePalette(this, nint.Zero)
            : Sdl.SetSurfacePalette(this, palette);

        if (code < 0)
        {
            throw new SurfaceException("Unable to set the surface palette", code);
        }
    }

    public void Lock()
    {
        int code = Sdl.LockSurface(this);
        if (code < 0)
        {
            throw new SurfaceException("Unable to lock the surface", code);
        }
    }

    public void Unlock()
    {
        Sdl.UnlockSurface(this);
    }

    public void SaveBmp(RwOps dst)
    {
        int code = Sdl.SaveBmpRw(this, dst, false);
        if (code < 0)
        {
            throw new SurfaceException("Unable to save the surface to the given BMP file", code);
        }
    }

    public void SetRleEnabled(bool enabled)
    {
        int code = Sdl.SetSurfaceRle(this, enabled);
        if (code < 0)
        {
            throw new SurfaceException($"Unable to set the RLE enabled state to {enabled}", code);
        }
    }

    public void SetColorKey(bool enabled, uint key)
    {
        int code = Sdl.SetColorKey(this, enabled, key);
        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to set the color key as: {{Is Enabled: {enabled}, Key: {key}}}",
                code
            );
        }
    }

    public uint GetColorKey()
    {
        int code = Sdl.GetColorKey(this, out uint key);
        if (code < 0)
        {
            throw new SurfaceException("Unable to get the color key", code);
        }

        return key;
    }

    public void SetColorModifier(Color color)
    {
        int code = Sdl.SetSurfaceColorMod(this, color.R, color.G, color.B);
        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to set the color modifier with the color {color}",
                code
            );
        }

        code = Sdl.SetSurfaceAlphaMod(this, color.A);
        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to set the alpha modifier with the color {color}",
                code
            );
        }
    }

    public Color GetColorMod()
    {
        int code = Sdl.GetSurfaceColorMod(this, out byte r, out byte g, out byte b);
        if (code < 0)
        {
            throw new SurfaceException("Unable to get the color modifier", code);
        }

        code = Sdl.GetSurfaceAlphaMod(this, out byte a);
        if (code < 0)
        {
            throw new SurfaceException("Unable to get the alpha modifier", code);
        }

        return Color.FromArgb(a, r, g, b);
    }

    public void SetBlendMode(BlendMode blendMode)
    {
        int code = Sdl.SetSurfaceBlendMode(this, blendMode);
        if (code < 0)
        {
            throw new SurfaceException($"Unable to set the blend mode to {blendMode}", code);
        }
    }

    public BlendMode GetBlendMode()
    {
        int code = Sdl.GetSurfaceBlendMode(this, out BlendMode blendMode);
        if (code < 0)
        {
            throw new SurfaceException("Unable to get the blend mode", code);
        }

        return blendMode;
    }

    public void SetClipRectangle(Rectangle clip)
    {
        bool isValid = Sdl.SetClipRect(this, clip);
        if (!isValid)
        {
            throw new SurfaceException($"Unable to set the clip rectangle to {clip}");
        }
    }

    public Rectangle GetClipRectangle()
    {
        Sdl.GetClipRect(this, out Rectangle result);
        return result;
    }

    public Surface Duplicate()
    {
        nint surfaceHandle = Sdl.DuplicateSurface(this);
        if (surfaceHandle == nint.Zero)
        {
            throw new SurfaceException("Unable to duplicate the surface");
        }

        return new Surface(surfaceHandle, true);
    }

    public Surface Convert(PixelFormat format)
    {
        nint surfaceHandle = Sdl.ConvertSurface(this, format, 0);
        if (surfaceHandle == nint.Zero)
        {
            throw new SurfaceException($"Unable to convert the surface to the format {format}");
        }

        return new Surface(surfaceHandle, true);
    }

    public Surface Convert(uint pixelFormat)
    {
        nint surfaceHandle = Sdl.ConvertSurfaceFormat(this, pixelFormat, 0);
        if (surfaceHandle == nint.Zero)
        {
            throw new SurfaceException(
                $"Unable to convert the surface to the pixel format {pixelFormat:X8}"
            );
        }

        return new Surface(surfaceHandle, true);
    }

    public void Fill(Rectangle rectangle, uint color)
    {
        int code = Sdl.FillRect(this, rectangle, color);
        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to fill the rectangle {rectangle} with the color {color:X8}",
                code
            );
        }
    }

    public void Fill(Rectangle[] rectangles, uint color)
    {
        unsafe
        {
            RectangleMarshaller.Rectangle[] sdlRects = new RectangleMarshaller.Rectangle[
                rectangles.Length
            ];
            for (int i = 0; i < rectangles.Length; i++)
            {
                sdlRects[i] = RectangleMarshaller.ConvertToUnmanaged(rectangles[i]);
            }

            fixed (RectangleMarshaller.Rectangle* sdlRectsHandle = sdlRects)
            {
                int code = Sdl.FillRects(this, sdlRectsHandle, rectangles.Length, color);
                if (code < 0)
                {
                    throw new SurfaceException(
                        $"Unable to fill {rectangles.Length} rectangles with the color {color:X8}",
                        code
                    );
                }
            }
        }
    }

    public void Blit(Rectangle srcRect, Surface dst, ref Rectangle dstRect, bool doScaledBlit)
    {
        UpperBlit(srcRect, dst, ref dstRect, doScaledBlit);
    }

    public void UpperBlit(Rectangle srcRect, Surface dst, ref Rectangle dstRect, bool doScaledBlit)
    {
        int code = doScaledBlit
            ? Sdl.UpperBlitScaled(this, srcRect, dst, ref dstRect)
            : Sdl.UpperBlit(this, srcRect, dst, ref dstRect);

        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to perform {(doScaledBlit ? "a scaled" : "an")} upper blit on the surface",
                code
            );
        }
    }

    public void LowerBlit(
        ref Rectangle srcRect,
        Surface dst,
        ref Rectangle dstRect,
        bool doScaledBlit
    )
    {
        int code = doScaledBlit
            ? Sdl.LowerBlitScaled(this, ref srcRect, dst, ref dstRect)
            : Sdl.LowerBlit(this, ref srcRect, dst, ref dstRect);

        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to perform {(doScaledBlit ? "a scaled" : "an")} lower blit on the surface",
                code
            );
        }
    }

    public void SoftStretch(Rectangle srcRect, Surface dst, Rectangle dstRect, bool doLinearStretch)
    {
        int code = doLinearStretch
            ? Sdl.SoftStretchLinear(this, srcRect, dst, dstRect)
            : Sdl.SoftStretch(this, srcRect, dst, dstRect);

        if (code < 0)
        {
            throw new SurfaceException(
                $"Unable to perform {(doLinearStretch ? "a linear" : "a")} soft stretch blit on the surface",
                0
            );
        }
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Surface)obj);
    }

    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Flags);
        code.Add(PixelFormat);
        code.Add(Size);
        code.Add(Pitch);
        code.Add(Pixels);
        code.Add(UserData);
        code.Add(Locked);
        code.Add(ClipRectangle);
        code.Add(ReferenceCount);
        code.Add(HasRle);
        code.Add(HasColorKey);
        return code.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Flags: [{Flags}], Pixel Format: {PixelFormat}, Size: {Size}, Pitch: {Pitch}, Pixels: [{string.Join(", ", Pixels ?? Array.Empty<byte>())}], User Data: [{string.Join(", ", UserData ?? Array.Empty<byte>())}], Locked: {Locked}, Clip Rectangle: {ClipRectangle}, Reference Count: {ReferenceCount}, Has RLE: {HasRle}, Has Color Key: {HasColorKey}}}";
    }

    public static bool operator ==(Surface? left, Surface? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Surface? left, Surface? right)
    {
        return !Equals(left, right);
    }
}
