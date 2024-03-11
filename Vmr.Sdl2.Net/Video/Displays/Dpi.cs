// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video.Displays;

[Serializable]
public readonly struct Dpi : IEquatable<Dpi>
{
    public float Diagonal { get; internal init; }
    public float Horizontal { get; internal init; }
    public float Vertical { get; internal init; }

    public bool Equals(Dpi other)
    {
        return Diagonal.Equals(other.Diagonal)
               && Horizontal.Equals(other.Horizontal)
               && Vertical.Equals(other.Vertical);
    }

    public override bool Equals(object? obj)
    {
        return obj is Dpi other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Diagonal, Horizontal, Vertical);
    }

    public override string ToString()
    {
        return $"{{Diagonal: {Diagonal:F2}, Horizontal: {Horizontal:F2}, Vertical: {Vertical:F2}}}";
    }

    public static bool operator ==(Dpi left, Dpi right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Dpi left, Dpi right)
    {
        return !left.Equals(right);
    }
}
