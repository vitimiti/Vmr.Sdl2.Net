// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

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
