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
