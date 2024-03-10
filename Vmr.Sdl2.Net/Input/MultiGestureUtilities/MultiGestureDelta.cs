// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.MultiGestureUtilities;

[Serializable]
public readonly struct MultiGestureDelta : IEquatable<MultiGestureDelta>
{
    public float Theta { get; internal init; }
    public float Distance { get; internal init; }

    public bool Equals(MultiGestureDelta other)
    {
        return Theta.Equals(other.Theta) && Distance.Equals(other.Distance);
    }

    public override bool Equals(object? obj)
    {
        return obj is MultiGestureDelta other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Theta, Distance);
    }

    public override string ToString()
    {
        return $"{{Delta Theta: {Theta}, Delta Distance: {Distance}}}";
    }

    public static bool operator ==(MultiGestureDelta left, MultiGestureDelta right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MultiGestureDelta left, MultiGestureDelta right)
    {
        return !left.Equals(right);
    }
}
