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

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class Clipboard
{
    public static string? Text => Sdl.GetClipboardText();
    public static bool HasText => Sdl.HasClipboardText();

    public static void SetText(string? text)
    {
        int code = Sdl.SetClipboardText(text);
        if (code < 0)
        {
            throw new ClipboardException($"Unable to set the clipboard text to '{text}'", code);
        }
    }
}
