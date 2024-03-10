// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

[Serializable]
public readonly struct BallRelativeMotion : IEquatable<BallRelativeMotion>
{
    public short X { get; internal init; }
    public short Y { get; internal init; }

    public bool Equals(BallRelativeMotion other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is BallRelativeMotion other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{{X: {X}, Y: {Y}}}";
    }

    public static bool operator ==(BallRelativeMotion left, BallRelativeMotion right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BallRelativeMotion left, BallRelativeMotion right)
    {
        return !left.Equals(right);
    }
}
