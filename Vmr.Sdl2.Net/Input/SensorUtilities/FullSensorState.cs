// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Input.SensorUtilities;

[Serializable]
public readonly struct FullSensorState : IEquatable<FullSensorState>
{
    public TimeSpan TimeStamp { get; internal init; }
    public float[]? Data { get; internal init; }

    public bool Equals(FullSensorState other)
    {
        return TimeStamp.Equals(other.TimeStamp) && Equals(Data, other.Data);
    }

    public override bool Equals(object? obj)
    {
        return obj is FullSensorState other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TimeStamp, Data);
    }

    public override string ToString()
    {
        return
            $"{{Time Stamp: {TimeStamp.Microseconds}μs, Data: [{(Data is not null ? string.Join(", ", Data) : string.Empty)}]}}";
    }

    public static bool operator ==(FullSensorState left, FullSensorState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FullSensorState left, FullSensorState right)
    {
        return !left.Equals(right);
    }
}
