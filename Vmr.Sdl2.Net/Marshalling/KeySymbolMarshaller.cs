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

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class KeySymbolMarshaller
{
    public static Input.KeyboardUtilities.KeySymbol ConvertToManaged(KeySymbol unmanaged)
    {
        return new Input.KeyboardUtilities.KeySymbol
        {
            ScanCode = unmanaged.ScanCode,
            KeyCode = unmanaged.Sym,
            Modifiers = unmanaged.Modifiers
        };
    }

    public static KeySymbol ConvertToUnmanaged(Input.KeyboardUtilities.KeySymbol managed)
    {
        return new KeySymbol
        {
            ScanCode = managed.ScanCode,
            Sym = managed.KeyCode,
            Modifiers = managed.Modifiers,
            Unused = 0
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeySymbol
    {
        public ScanCode ScanCode;
        public KeyCode Sym;
        public KeyModifiers Modifiers;
        public uint Unused;
    }
}
