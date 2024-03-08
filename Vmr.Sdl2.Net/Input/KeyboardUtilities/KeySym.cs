// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

[Serializable]
public readonly struct KeySym : IEquatable<KeySym>
{
    public ScanCode ScanCode { get; internal init; }
    public KeyCode Sym { get; internal init; }
    public KeyModifiers Modifiers { get; internal init; }

    public bool Equals(KeySym other)
    {
        return ScanCode == other.ScanCode && Sym == other.Sym && Modifiers == other.Modifiers;
    }

    public override bool Equals(object? obj)
    {
        return obj is KeySym other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)ScanCode, (int)Sym, (int)Modifiers);
    }

    public override string ToString()
    {
        return $"{{Scan Code: {ScanCode}, Sym: {Sym}, Modifiers: [{Modifiers}]}}";
    }

    public static bool operator ==(KeySym left, KeySym right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(KeySym left, KeySym right)
    {
        return !left.Equals(right);
    }
}
