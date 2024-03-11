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
public struct GameControllerMappingButton : IEquatable<GameControllerMappingButton>
{
    public GameControllerButton Button { get; set; }
    public int ButtonIndex { get; set; }

    internal string ToNativeString()
    {
        return $"{Sdl.GameControllerGetStringForButton(Button)}:b{ButtonIndex}";
    }

    internal static GameControllerMappingButton FromNativeString(string nativeString)
    {
        string[] parts = nativeString.Split(':');
        if (parts.Length != 2 || !nativeString.Contains(":b"))
        {
            throw new ArgumentException(
                $"The native string '{nativeString}' isn't in the 'x:by' format, where 'x' is the button and 'y' is the button index."
            );
        }

        return new GameControllerMappingButton
        {
            Button = Sdl.GameControllerGetButtonFromString(parts[0]),
            ButtonIndex = int.Parse(parts[1][1..])
        };
    }

    public bool Equals(GameControllerMappingButton other)
    {
        return Button == other.Button && ButtonIndex == other.ButtonIndex;
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMappingButton other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Button, ButtonIndex);
    }

    public override string ToString()
    {
        return $"{{Button: {Button}, Button Index: {ButtonIndex}}}";
    }

    public static bool operator ==(
        GameControllerMappingButton left,
        GameControllerMappingButton right
    )
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        GameControllerMappingButton left,
        GameControllerMappingButton right
    )
    {
        return !left.Equals(right);
    }
}
