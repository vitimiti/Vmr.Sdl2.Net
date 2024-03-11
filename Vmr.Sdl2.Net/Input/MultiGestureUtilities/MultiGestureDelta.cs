// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

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
