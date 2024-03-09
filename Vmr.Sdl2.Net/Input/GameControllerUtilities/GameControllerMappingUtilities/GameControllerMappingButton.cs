// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
