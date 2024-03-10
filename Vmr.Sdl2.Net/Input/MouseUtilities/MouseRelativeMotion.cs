// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.MouseUtilities;

[Serializable]
public readonly struct MouseRelativeMotion : IEquatable<MouseRelativeMotion>
{
    public int X { get; internal init; }
    public int Y { get; internal init; }

    public bool Equals(MouseRelativeMotion other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is MouseRelativeMotion other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{{X: {X}, Y: {Y}}}";
    }

    public static bool operator ==(MouseRelativeMotion left, MouseRelativeMotion right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MouseRelativeMotion left, MouseRelativeMotion right)
    {
        return !left.Equals(right);
    }
}
