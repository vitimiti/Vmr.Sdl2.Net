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
public readonly struct JoystickGuidInfo : IEquatable<JoystickGuidInfo>
{
    public ushort Vendor { get; internal init; }
    public ushort Product { get; internal init; }
    public ushort Version { get; internal init; }
    public ushort Crc16 { get; internal init; }

    public bool Equals(JoystickGuidInfo other)
    {
        return Vendor == other.Vendor
               && Product == other.Product
               && Version == other.Version
               && Crc16 == other.Crc16;
    }

    public override bool Equals(object? obj)
    {
        return obj is JoystickGuidInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Vendor, Product, Version, Crc16);
    }

    public override string ToString()
    {
        return
            $"{{Vendor: 0x{Vendor:X4}, Product: 0x{Product:X4}, Version: 0x{Version:X4}, CRC16: 0x{Crc16:X4}}}";
    }

    public static bool operator ==(JoystickGuidInfo left, JoystickGuidInfo right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(JoystickGuidInfo left, JoystickGuidInfo right)
    {
        return !left.Equals(right);
    }
}
