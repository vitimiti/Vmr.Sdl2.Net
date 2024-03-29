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

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerMappingUtilities;

[Serializable]
public struct GameControllerMappingHat : IEquatable<GameControllerMappingHat>
{
    public GameControllerButton Button { get; set; }
    public int HatIndex { get; set; }
    public int HatValue { get; set; }

    internal string ToNativeString()
    {
        return $"{Sdl.GameControllerGetStringForButton(Button)}:h{HatIndex}.{HatValue}";
    }

    internal static GameControllerMappingHat FromNativeString(string nativeString)
    {
        string[] parts = nativeString.Split(':');
        string[] secondParts = parts[1].Split('.');
        if (
            parts.Length != 2
            || secondParts.Length != 2
            || !nativeString.Contains(":h")
            || !nativeString.Contains('.')
        )
        {
            throw new ArgumentException(
                $"The native string '{nativeString}' isn't in the 'x:hy.z' format, where 'x' is the button, 'y' is the hat index and 'z' is the hat value."
            );
        }

        return new GameControllerMappingHat
        {
            Button = Sdl.GameControllerGetButtonFromString(parts[0]),
            HatIndex = int.Parse(secondParts[0][1..]),
            HatValue = int.Parse(secondParts[1])
        };
    }

    public bool Equals(GameControllerMappingHat other)
    {
        return Button == other.Button
               && HatIndex == other.HatIndex
               && HatValue.Equals(other.HatValue);
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMappingHat other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Button, HatIndex, HatValue);
    }

    public override string ToString()
    {
        return $"{{Button: {Button}, Hat Index: {HatIndex}, Hat Value: {HatValue}}}";
    }

    public static bool operator ==(GameControllerMappingHat left, GameControllerMappingHat right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameControllerMappingHat left, GameControllerMappingHat right)
    {
        return !left.Equals(right);
    }
}
