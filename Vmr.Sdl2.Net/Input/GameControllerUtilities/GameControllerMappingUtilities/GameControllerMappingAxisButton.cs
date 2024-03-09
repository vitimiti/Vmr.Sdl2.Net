// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
