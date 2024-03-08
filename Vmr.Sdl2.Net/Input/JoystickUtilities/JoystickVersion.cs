// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
        return
            $"{{Product Version: {Product} (0x{Product:X4}), Firmware Version: {Firmware} (0x{Firmware:X4})}}";
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
