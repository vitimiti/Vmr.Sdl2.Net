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

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Video.Messages;

public static class MessageBox
{
    public static int Show(MessageBoxData messageBoxData)
    {
        int code = Sdl.ShowMessageBox(messageBoxData, out int buttonId);
        if (code < 0)
        {
            throw new MessageBoxException(
                $"Unable to show a message box with data {messageBoxData}",
                code
            );
        }

        return buttonId;
    }

    public static void Show(MessageBoxOptions options, string title, string message, Window? window)
    {
        int code = window is null
            ? Sdl.ShowSimpleMessageBox(options, title, message, nint.Zero)
            : Sdl.ShowSimpleMessageBox(options, title, message, window);

        if (code < 0)
        {
            throw new MessageBoxException("Unable to show a simple message box", code);
        }
    }
}
