// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Utilities.Power;

[Serializable]
public readonly struct BatteryLife : IEquatable<BatteryLife>
{
    public TimeSpan TimeLeft { get; internal init; }
    public int PercentageLeft { get; internal init; }

    public bool Equals(BatteryLife other)
    {
        return TimeLeft == other.TimeLeft && PercentageLeft == other.PercentageLeft;
    }

    public override bool Equals(object? obj)
    {
        return obj is BatteryLife other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TimeLeft, PercentageLeft);
    }

    public override string ToString()
    {
        return $"{{Seconds Left: {TimeLeft.Seconds}s, Percentage Left: {PercentageLeft}%}}";
    }

    public static bool operator ==(BatteryLife left, BatteryLife right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BatteryLife left, BatteryLife right)
    {
        return !left.Equals(right);
    }
}
