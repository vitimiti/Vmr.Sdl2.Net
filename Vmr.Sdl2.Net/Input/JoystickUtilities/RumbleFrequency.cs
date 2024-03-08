// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
