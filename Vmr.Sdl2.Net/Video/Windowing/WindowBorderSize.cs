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

namespace Vmr.Sdl2.Net.Video.Windowing;

public readonly struct WindowBorderSize : IEquatable<WindowBorderSize>
{
    public int Top { get; internal init; }
    public int Left { get; internal init; }
    public int Bottom { get; internal init; }
    public int Right { get; internal init; }

    public bool Equals(WindowBorderSize other)
    {
        return Top == other.Top
               && Left == other.Left
               && Bottom == other.Bottom
               && Right == other.Right;
    }

    public override bool Equals(object? obj)
    {
        return obj is WindowBorderSize other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Top, Left, Bottom, Right);
    }

    public static bool operator ==(WindowBorderSize left, WindowBorderSize right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WindowBorderSize left, WindowBorderSize right)
    {
        return !left.Equals(right);
    }
}
