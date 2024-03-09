// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerMappingUtilities;

[Serializable]
public struct GameControllerMappingAxisHat : IEquatable<GameControllerMappingAxisHat>
{
    public GameControllerAxis Axis { get; set; }
    public int HatIndex { get; set; }
    public int HatValue { get; set; }

    internal string ToNativeString()
    {
        return $"{Sdl.GameControllerGetStringForAxis(Axis)}:h{HatIndex}.{HatValue}";
    }

    internal static GameControllerMappingAxisHat FromNativeString(string nativeString)
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

        return new GameControllerMappingAxisHat
        {
            Axis = Sdl.GameControllerGetAxisFromString(parts[0]),
            HatIndex = int.Parse(secondParts[0][1..]),
            HatValue = int.Parse(secondParts[1])
        };
    }

    public bool Equals(GameControllerMappingAxisHat other)
    {
        return Axis == other.Axis && HatIndex == other.HatIndex && HatValue.Equals(other.HatValue);
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMappingAxisHat other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Axis, HatIndex, HatValue);
    }

    public override string ToString()
    {
        return $"{{Button: {Axis}, Hat Index: {HatIndex}, Hat Value: {HatValue}}}";
    }

    public static bool operator ==(
        GameControllerMappingAxisHat left,
        GameControllerMappingAxisHat right
    )
    {
        return left.Equals(right);
    }

    public static bool operator !=(
        GameControllerMappingAxisHat left,
        GameControllerMappingAxisHat right
    )
    {
        return !left.Equals(right);
    }
}
