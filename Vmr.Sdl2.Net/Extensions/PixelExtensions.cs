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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Drawing;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Extensions;

public static class PixelExtensions
{
    public static byte PixelFlag(this uint format)
    {
        return (byte)((format >> 28) & 0x0F);
    }

    public static byte PixelType(this uint format)
    {
        return (byte)((format >> 24) & 0x0F);
    }

    public static byte PixelOrder(this uint format)
    {
        return (byte)((format >> 20) & 0x0F);
    }

    public static byte PixelLayout(this uint format)
    {
        return (byte)((format >> 16) & 0x0F);
    }

    public static byte BitsPerPixel(this uint format)
    {
        return (byte)((format >> 8) & 0xFF);
    }

    public static string? GetPixelFormatName(this uint format)
    {
        return Sdl.GetPixelFormatName(format);
    }

    public static ColorMasks PixelFormatToMasks(this uint format)
    {
        bool isValid = Sdl.PixelFormatEnumToMasks(
            format,
            out int bpp,
            out uint rMask,
            out uint gMask,
            out uint bMask,
            out uint aMask
        );

        if (!isValid)
        {
            throw new PixelFormatException(
                $"Unable to transform the format {format:X8} to color masks"
            );
        }

        return new ColorMasks
        {
            BitsPerPixel = bpp,
            Red = rMask,
            Green = gMask,
            Blue = bMask,
            Alpha = aMask
        };
    }

    public static Color GetRgb(this uint pixel, PixelFormat format)
    {
        Sdl.GetRgb(pixel, format, out byte r, out byte g, out byte b);
        return Color.FromArgb(r, g, b);
    }

    public static Color GetRgba(this uint pixel, PixelFormat format)
    {
        Sdl.GetRgba(pixel, format, out byte r, out byte g, out byte b, out byte a);
        return Color.FromArgb(a, r, g, b);
    }

    public static ushort[] CalculateGammaRamp(this float gamma)
    {
        ushort[] ramp = new ushort[256];
        Sdl.CalculateGammaRamp(float.Clamp(gamma, 0F, 1F), ramp);
        return ramp;
    }
}
