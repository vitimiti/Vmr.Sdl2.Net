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

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerMappingUtilities;

[Serializable]
public struct GameControllerMappingButtonAxis : IEquatable<GameControllerMappingButtonAxis>
{
    public GameControllerButton Button { get; set; }
    public int AxisIndex { get; set; }
    public GameControllerMappingAxisDeviation Deviation { get; set; }

    internal string ToNativeString()
    {
        string axisDeviation = Deviation switch
        {
            GameControllerMappingAxisDeviation.None => string.Empty,
            GameControllerMappingAxisDeviation.Negative => "-",
            GameControllerMappingAxisDeviation.Positive => "+",
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(Deviation),
                    Deviation,
                    $"The {nameof(Deviation)} must be one of the values defined in {nameof(GameControllerMappingAxisDeviation)}"
                )
        };

        return $"{axisDeviation}{Sdl.GameControllerGetStringForButton(Button)}:a{AxisIndex}";
    }

    internal static GameControllerMappingButtonAxis FromNativeString(string nativeString)
    {
        string[] parts = nativeString.Split(':');
        if (parts.Length != 2 || !nativeString.Contains(":a"))
        {
            throw new ArgumentException(
                $"The native string '{nativeString}' isn't in the 'x:ay', '-x:ay' or '+x:ay' format, where 'x' is the button, 'y' is the axis index and '-' or '+' is the deviation, if any."
            );
        }

        GameControllerMappingAxisDeviation deviation = nativeString[0] switch
        {
            '+' => GameControllerMappingAxisDeviation.Positive,
            '-' => GameControllerMappingAxisDeviation.Negative,
            _ => GameControllerMappingAxisDeviation.None
        };

        GameControllerButton axis = nativeString[0] switch
        {
            '+' or '-' => Sdl.GameControllerGetButtonFromString(parts[0][1..]),
            _ => Sdl.GameControllerGetButtonFromString(parts[0])
        };

        return new GameControllerMappingButtonAxis
        {
            Button = axis, AxisIndex = int.Parse(parts[1][1..]), Deviation = deviation
        };
    }

    public bool Equals(GameControllerMappingButtonAxis other)
    {
        return Button == other.Button
               && AxisIndex == other.AxisIndex
               && Deviation == other.Deviation;
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMappingButtonAxis other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Button, AxisIndex, (int)Deviation);
    }

    public override string ToString()
    {
        return $"{{Button: {Button}, Axis Index: {AxisIndex}, Direction: {Deviation}}}";
    }

    public static bool operator ==(
        GameControllerMappingButtonAxis left,
        GameControllerMappingButtonAxis right
    )
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        GameControllerMappingButtonAxis left,
        GameControllerMappingButtonAxis right
    )
    {
        return !left.Equals(right);
    }
}
