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

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerMappingUtilities;

[Serializable]
public struct GameControllerMappingAxisButton : IEquatable<GameControllerMappingAxisButton>
{
    public GameControllerAxis Axis { get; set; }
    public int ButtonIndex { get; set; }
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

        return $"{axisDeviation}{Sdl.GameControllerGetStringForAxis(Axis)}:b{ButtonIndex}";
    }

    internal static GameControllerMappingAxisButton FromNativeString(string nativeString)
    {
        string[] parts = nativeString.Split(':');
        if (parts.Length != 2 || !nativeString.Contains(":b"))
        {
            throw new ArgumentException(
                $"The native string '{nativeString}' isn't in the 'x:by' format, where 'x' is the button and 'y' is the button index."
            );
        }

        GameControllerMappingAxisDeviation deviation = nativeString[0] switch
        {
            '+' => GameControllerMappingAxisDeviation.Positive,
            '-' => GameControllerMappingAxisDeviation.Negative,
            _ => GameControllerMappingAxisDeviation.None
        };

        GameControllerAxis axis = nativeString[0] switch
        {
            '+' or '-' => Sdl.GameControllerGetAxisFromString(parts[0][1..]),
            _ => Sdl.GameControllerGetAxisFromString(parts[0])
        };

        return new GameControllerMappingAxisButton
        {
            Axis = axis, ButtonIndex = int.Parse(parts[1][1..]), Deviation = deviation
        };
    }

    public bool Equals(GameControllerMappingAxisButton other)
    {
        return Axis == other.Axis
               && ButtonIndex == other.ButtonIndex
               && Deviation == other.Deviation;
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMappingAxisButton other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Axis, ButtonIndex, (int)Deviation);
    }

    public override string ToString()
    {
        return $"{{Button: {Axis}, Button Index: {ButtonIndex}, Deviation: {Deviation}}}";
    }

    public static bool operator ==(
        GameControllerMappingAxisButton left,
        GameControllerMappingAxisButton right
    )
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        GameControllerMappingAxisButton left,
        GameControllerMappingAxisButton right
    )
    {
        return !left.Equals(right);
    }
}
