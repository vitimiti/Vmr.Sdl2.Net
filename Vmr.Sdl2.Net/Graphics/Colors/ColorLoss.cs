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

namespace Vmr.Sdl2.Net.Graphics.Colors;

[Serializable]
public struct ColorLoss : IEquatable<ColorLoss>
{
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }
    public byte Alpha { get; set; }

    public bool Equals(ColorLoss other)
    {
        return Red == other.Red
               && Green == other.Green
               && Blue == other.Blue
               && Alpha == other.Alpha;
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorLoss other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Red, Green, Blue, Alpha);
    }

    public override string ToString()
    {
        return $"{{Red: {Red}, Green: {Green}, Blue: {Blue}, Alpha: {Alpha}}}";
    }

    public static bool operator ==(ColorLoss left, ColorLoss right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ColorLoss left, ColorLoss right)
    {
        return !left.Equals(right);
    }
}
