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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Drawing;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.MouseUtilities;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Input;

public static class Mouse
{
    public static MouseState State
    {
        get
        {
            MouseButtons buttons = Sdl.GetMouseState(out int x, out int y);
            return new MouseState { Buttons = buttons, Position = new Point(x, y) };
        }
    }

    public static MouseState GlobalState
    {
        get
        {
            MouseButtons buttons = Sdl.GetGlobalMouseState(out int x, out int y);
            return new MouseState { Buttons = buttons, Position = new Point(x, y) };
        }
    }

    public static MouseState RelativeState
    {
        get
        {
            MouseButtons buttons = Sdl.GetRelativeMouseState(out int x, out int y);
            return new MouseState { Buttons = buttons, Position = new Point(x, y) };
        }
    }

    public static bool IsRelativeModeEnabled => Sdl.GetRelativeMouseMode();

    public static Window GetFocus()
    {
        nint handle = Sdl.GetMouseFocus();
        if (handle == nint.Zero)
        {
            throw new MouseException("Unable to get the mouse focused window");
        }

        return new Window(handle, false);
    }

    public static void Warp(Point position)
    {
        int code = Sdl.WarpMouseGlobal(position.X, position.Y);
        if (code < 0)
        {
            throw new MouseException($"Unable to warp mouse to {position}", code);
        }
    }

    public static void SetRelativeMode(bool enabled)
    {
        int code = Sdl.SetRelativeMouseMode(enabled);
        if (code < 0)
        {
            throw new MouseException(
                $"Unable to set mouse relative mode to {(enabled ? "enabled" : "disabled")}"
            );
        }
    }

    public static void Capture(bool captured)
    {
        int code = Sdl.CaptureMouse(captured);
        if (code < 0)
        {
            throw new MouseException(
                $"Unable to set mouse capture mode to {(captured ? "captured" : "not captured")}"
            );
        }
    }
}
