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
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;

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

    public Palette(int numberOfColors)
        : base(true)
    {
        handle = Sdl.AllocPalette(numberOfColors);
        if (handle == nint.Zero)
        {
            throw new PaletteException(
                $"Unable to create a palette with {numberOfColors} color(s)"
            );
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

    public void SetColors(Color[]? colors, int firstColor, int numberOfColors)
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
                    throw new PaletteException(
                        "Unable to set the given new colors to the palette",
                        code
                    );
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
        return
            $"{{Colors: [{string.Join(", ", Colors ?? Array.Empty<Color>())}], Version: {Version}, Reference Count: {ReferenceCount}}}";
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
