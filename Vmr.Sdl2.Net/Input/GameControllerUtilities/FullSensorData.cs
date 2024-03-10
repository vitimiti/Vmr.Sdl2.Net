// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[Serializable]
public readonly struct FullSensorData : IEquatable<FullSensorData>
{
    public TimeSpan TimeStamp { get; internal init; }
    public float[]? Data { get; internal init; }

    public bool Equals(FullSensorData other)
    {
        return TimeStamp.Equals(other.TimeStamp) && Equals(Data, other.Data);
    }

    public override bool Equals(object? obj)
    {
        return obj is FullSensorData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TimeStamp, Data);
    }

    public override string ToString()
    {
        return $"{{Time Stamp: {TimeStamp.Microseconds}μs, Data: [{(Data is not null ? string.Join(", ", Data) : string.Empty)}]}}";
    }

    public static bool operator ==(FullSensorData left, FullSensorData right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FullSensorData left, FullSensorData right)
    {
        return !left.Equals(right);
    }
}
