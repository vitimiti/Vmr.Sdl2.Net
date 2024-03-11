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
using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.Extensions;

public static class KeyboardExtensions
{
    public static KeyCode GetKey(this ScanCode scanCode)
    {
        return Sdl.GetKeyFromScanCode(scanCode);
    }

    public static ScanCode GetScanCode(this KeyCode key)
    {
        return Sdl.GetScanCodeFromKey(key);
    }

    public static string GetName(this ScanCode scanCode)
    {
        return Sdl.GetScanCodeName(scanCode);
    }

    public static ScanCode GetScanCode(this string name)
    {
        return Sdl.GetScanCodeFromName(name);
    }

    public static string GetName(this KeyCode key)
    {
        return Sdl.GetKeyName(key);
    }

    public static KeyCode GetKey(this string name)
    {
        return Sdl.GetKeyFromName(name);
    }
}
