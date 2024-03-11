// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Graphics.Colors;

[Serializable]
public struct ColorMasks : IEquatable<ColorMasks>
{
    public int BitsPerPixel { get; set; }
    public uint Red { get; set; }
    public uint Green { get; set; }
    public uint Blue { get; set; }
    public uint Alpha { get; set; }

    public uint ToPixelFormat()
    {
        return Sdl.MasksToPixelFormatEnum(BitsPerPixel, Red, Green, Blue, Alpha);
    }

    public bool Equals(ColorMasks other)
    {
        return BitsPerPixel == other.BitsPerPixel
               && Red == other.Red
               && Green == other.Green
               && Blue == other.Blue
               && Alpha == other.Alpha;
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorMasks other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BitsPerPixel, Red, Green, Blue, Alpha);
    }

    public override string ToString()
    {
        return
            $"{{Bits Per Pixel: {BitsPerPixel}, Red: {Red}, Green: {Green}, Blue: {Blue}, Alpha: {Alpha}}}";
    }

    public static bool operator ==(ColorMasks left, ColorMasks right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ColorMasks left, ColorMasks right)
    {
        return !left.Equals(right);
    }
}
