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

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

[Serializable]
public readonly struct JoystickVersion : IEquatable<JoystickVersion>
{
    public ushort Product { get; internal init; }
    public ushort Firmware { get; internal init; }

    public bool Equals(JoystickVersion other)
    {
        return Product == other.Product && Firmware == other.Firmware;
    }

    public override bool Equals(object? obj)
    {
        return obj is JoystickVersion other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Product, Firmware);
    }

    public override string ToString()
    {
        return $"{{Product Version: 0x{Product:X4}, Firmware Version: 0x{Firmware:X4}}}";
    }

    public static bool operator ==(JoystickVersion left, JoystickVersion right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(JoystickVersion left, JoystickVersion right)
    {
        return !left.Equals(right);
    }
}
