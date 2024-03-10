// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

[Serializable]
public readonly struct KeySymbol : IEquatable<KeySymbol>
{
    public ScanCode ScanCode { get; internal init; }
    public KeyCode KeyCode { get; internal init; }
    public KeyModifiers Modifiers { get; internal init; }

    public bool Equals(KeySymbol other)
    {
        return ScanCode == other.ScanCode
               && KeyCode == other.KeyCode
               && Modifiers == other.Modifiers;
    }

    public override bool Equals(object? obj)
    {
        return obj is KeySymbol other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)ScanCode, (int)KeyCode, (int)Modifiers);
    }

    public override string ToString()
    {
        return $"{{Scan Code: {ScanCode}, Key Code: {KeyCode}, Modifiers: [{Modifiers}]}}";
    }

    public static bool operator ==(KeySymbol left, KeySymbol right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(KeySymbol left, KeySymbol right)
    {
        return !left.Equals(right);
    }
}
