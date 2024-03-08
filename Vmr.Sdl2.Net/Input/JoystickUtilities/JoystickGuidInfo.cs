// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
