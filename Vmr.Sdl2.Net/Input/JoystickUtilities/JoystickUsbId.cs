// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
        return
            $"{{Vendor ID: {Vendor} (0x{Vendor:X4}), Product ID: {Product} (0x{Product:X4})}}";
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
