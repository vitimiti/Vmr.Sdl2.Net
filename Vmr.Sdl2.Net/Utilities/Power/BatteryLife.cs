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

namespace Vmr.Sdl2.Net.Utilities.Power;

[Serializable]
public readonly struct BatteryLife : IEquatable<BatteryLife>
{
    public TimeSpan TimeLeft { get; internal init; }
    public int PercentageLeft { get; internal init; }

    public bool Equals(BatteryLife other)
    {
        return TimeLeft == other.TimeLeft && PercentageLeft == other.PercentageLeft;
    }

    public override bool Equals(object? obj)
    {
        return obj is BatteryLife other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TimeLeft, PercentageLeft);
    }

    public override string ToString()
    {
        return $"{{Seconds Left: {TimeLeft.Seconds}s, Percentage Left: {PercentageLeft}%}}";
    }

    public static bool operator ==(BatteryLife left, BatteryLife right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BatteryLife left, BatteryLife right)
    {
        return !left.Equals(right);
    }
}
