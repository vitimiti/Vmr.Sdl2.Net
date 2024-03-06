// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Graphics.Colors;

[Serializable]
public struct ColorMasks : IEquatable<ColorMasks>
{
    public int BitsPerPixel { get; set; }
    public uint Red { get; set; }
    public uint Green { get; set; }
    public uint Blue { get; set; }
    public uint Alpha { get; set; }

    public uint ToPixelFormat()
    {
        return Sdl.MasksToPixelFormatEnum(BitsPerPixel, Red, Green, Blue, Alpha);
    }

    public bool Equals(ColorMasks other)
    {
        return BitsPerPixel == other.BitsPerPixel
            && Red == other.Red
            && Green == other.Green
            && Blue == other.Blue
            && Alpha == other.Alpha;
    }

    public override bool Equals(object? obj)
    {
        return obj is ColorMasks other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BitsPerPixel, Red, Green, Blue, Alpha);
    }

    public override string ToString()
    {
        return $"{{Bits Per Pixel: {BitsPerPixel}, Red: {Red}, Green: {Green}, Blue: {Blue}, Alpha: {Alpha}}}";
    }

    public static bool operator ==(ColorMasks left, ColorMasks right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ColorMasks left, ColorMasks right)
    {
        return !left.Equals(right);
    }
}
