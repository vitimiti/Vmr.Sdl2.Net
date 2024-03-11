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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

[Serializable]
public readonly struct JoystickUsbId : IEquatable<JoystickUsbId>
{
    public ushort Vendor { get; internal init; }
    public ushort Product { get; internal init; }

    public bool Equals(JoystickUsbId other)
    {
        return Vendor == other.Vendor && Product == other.Product;
    }

    public override bool Equals(object? obj)
    {
        return obj is JoystickUsbId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Vendor, Product);
    }

    public override string ToString()
    {
        return $"{{Vendor ID: 0x{Vendor:X4}, Product ID: 0x{Product:X4}}}";
    }

    public static bool operator ==(JoystickUsbId left, JoystickUsbId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(JoystickUsbId left, JoystickUsbId right)
    {
        return !left.Equals(right);
    }
}
