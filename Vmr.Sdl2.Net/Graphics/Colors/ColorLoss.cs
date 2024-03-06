// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Graphics.Colors;

[Serializable]
public struct ColorLoss : IEquatable<ColorLoss>
{
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }
    public byte Alpha { get; set; }

    public bool Equals(ColorLoss other)
    {
        return Red == other.Red
            && Green == other.Green
            && Blue == other.Blue
            && Alpha == other.Alpha;
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorLoss other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Red, Green, Blue, Alpha);
    }

    public override string ToString()
    {
        return $"{{Red: {Red}, Green: {Green}, Blue: {Blue}, Alpha: {Alpha}}}";
    }

    public static bool operator ==(ColorLoss left, ColorLoss right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ColorLoss left, ColorLoss right)
    {
        return !left.Equals(right);
    }
}
