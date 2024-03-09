// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.MouseUtilities;

[Serializable]
public readonly struct PreciseScrollAmount : IEquatable<PreciseScrollAmount>
{
    public float Horizontal { get; internal init; }
    public float Vertical { get; internal init; }

    public bool Equals(PreciseScrollAmount other)
    {
        return Math.Abs(Horizontal - other.Horizontal) < float.Epsilon
               && Math.Abs(Vertical - other.Vertical) < float.Epsilon;
    }

    public override bool Equals(object? obj)
    {
        return obj is PreciseScrollAmount other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Horizontal, Vertical);
    }

    public override string ToString()
    {
        return $"{{Horizontal: {Horizontal:F2}, Vertical: {Vertical:F2}}}";
    }

    public static bool operator ==(PreciseScrollAmount left, PreciseScrollAmount right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PreciseScrollAmount left, PreciseScrollAmount right)
    {
        return !left.Equals(right);
    }
}
