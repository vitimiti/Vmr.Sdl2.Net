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

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

public struct RumbleFrequency : IEquatable<RumbleFrequency>
{
    public required ushort Low { get; set; }
    public required ushort High { get; set; }

    public bool Equals(RumbleFrequency other)
    {
        return Low == other.Low && High == other.High;
    }

    public override bool Equals(object? obj)
    {
        return obj is RumbleFrequency other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Low, High);
    }

    public override string ToString()
    {
        return $"{{Low: {Low}Hz, High: {High}Hz";
    }

    public static bool operator ==(RumbleFrequency left, RumbleFrequency right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RumbleFrequency left, RumbleFrequency right)
    {
        return !left.Equals(right);
    }
}
