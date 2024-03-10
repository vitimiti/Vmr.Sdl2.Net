// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Palette>))]
public class Palette : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<Palette>
{
    internal Palette(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Palette(int numberOfColors, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.AllocPalette(numberOfColors);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    private unsafe Sdl.Palette* UnsafeHandle => (Sdl.Palette*)handle;

    public Color[]? Colors
    {
        get
        {
            unsafe
            {
                if (UnsafeHandle->Colors is null)
                {
                    return null;
                }

                if (UnsafeHandle->NColors == 0)
                {
                    return Array.Empty<Color>();
                }

                Color[] colors = new Color[UnsafeHandle->NColors];
                for (int i = 0; i < UnsafeHandle->NColors; i++)
                {
                    colors[i] = ColorMarshaller.ConvertToManaged(UnsafeHandle->Colors[i]);
                }

                return colors;
            }
        }
    }

    public uint Version
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Version;
            }
        }
    }

    public int ReferenceCount
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->RefCount;
            }
        }
    }

    public bool Equals(Palette? other)
    {
        return other is not null && Colors == other.Colors;
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreePalette(handle);
        handle = nint.Zero;
        return true;
    }

    public void SetColors(
        Color[]? colors,
        int firstColor,
        int numberOfColors,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            ColorMarshaller.Color[]? sdlColors = null;
            if (colors is not null)
            {
                sdlColors = new ColorMarshaller.Color[colors.Length];
                for (int i = 0; i < colors.Length; i++)
                {
                    sdlColors[i] = ColorMarshaller.ConvertToUnmanaged(colors[i]);
                }
            }

            fixed (ColorMarshaller.Color* colorsHandle = sdlColors)
            {
                int code = Sdl.SetPaletteColors(handle, colorsHandle, firstColor, numberOfColors);
                if (code < 0)
                {
                    errorHandler(Sdl.GetError(), code);
                }
            }
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

        return Equals((Palette)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Colors, Version, ReferenceCount);
    }

    public override string ToString()
    {
        return $"{{Colors: [{string.Join(", ", Colors ?? Array.Empty<Color>())}], Version: {Version}, Reference Count: {ReferenceCount}}}";
    }

    public static bool operator ==(Palette? left, Palette? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Palette? left, Palette? right)
    {
        return !Equals(left, right);
    }
}
