// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.Input.MouseUtilities;

public readonly struct MouseState : IEquatable<MouseState>
{
    public MouseButtons Buttons { get; internal init; }
    public Point Position { get; internal init; }

    public bool Equals(MouseState other)
    {
        return Buttons == other.Buttons && Position.Equals(other.Position);
    }

    public override bool Equals(object? obj)
    {
        return obj is MouseState other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Buttons, Position);
    }

    public override string ToString()
    {
        return $"{{Buttons: [{Buttons}], Position: {Position}}}";
    }

    public static bool operator ==(MouseState left, MouseState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MouseState left, MouseState right)
    {
        return !left.Equals(right);
    }
}
