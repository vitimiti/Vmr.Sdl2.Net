// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Input;

[Serializable]
[NativeMarshalling(typeof(FingerMarshaller))]
public struct Finger : IEquatable<Finger>
{
    public long Id { get; internal init; }
    public PointF Position { get; internal init; }
    public float Pressure { get; internal init; }

    public bool Equals(Finger other)
    {
        return Id == other.Id && Position.Equals(other.Position) && Pressure.Equals(other.Pressure);
    }

    public override bool Equals(object? obj)
    {
        return obj is Finger other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Position, Pressure);
    }

    public override string ToString()
    {
        return $"{{ID: {Id}, Position: {Position}, Pressure: {Pressure:F2}}}";
    }

    public static bool operator ==(Finger left, Finger right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Finger left, Finger right)
    {
        return !left.Equals(right);
    }
}
