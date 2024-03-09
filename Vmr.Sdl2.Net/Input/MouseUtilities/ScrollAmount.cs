// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.MouseUtilities;

[Serializable]
public readonly struct ScrollAmount : IEquatable<ScrollAmount>
{
    public int Horizontal { get; internal init; }
    public int Vertical { get; internal init; }

    public bool Equals(ScrollAmount other)
    {
        return Horizontal == other.Horizontal && Vertical == other.Vertical;
    }

    public override bool Equals(object? obj)
    {
        return obj is ScrollAmount other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Horizontal, Vertical);
    }

    public override string ToString()
    {
        return $"{{Horizontal: {Horizontal}, Vertical: {Vertical}}}";
    }

    public static bool operator ==(ScrollAmount left, ScrollAmount right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ScrollAmount left, ScrollAmount right)
    {
        return !left.Equals(right);
    }
}
