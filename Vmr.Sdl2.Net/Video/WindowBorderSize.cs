// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video;

public readonly struct WindowBorderSize : IEquatable<WindowBorderSize>
{
    public int Top { get; internal init; }
    public int Left { get; internal init; }
    public int Bottom { get; internal init; }
    public int Right { get; internal init; }

    public bool Equals(WindowBorderSize other)
    {
        return Top == other.Top
            && Left == other.Left
            && Bottom == other.Bottom
            && Right == other.Right;
    }

    public override bool Equals(object? obj)
    {
        return obj is WindowBorderSize other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Top, Left, Bottom, Right);
    }

    public static bool operator ==(WindowBorderSize left, WindowBorderSize right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WindowBorderSize left, WindowBorderSize right)
    {
        return !left.Equals(right);
    }
}
