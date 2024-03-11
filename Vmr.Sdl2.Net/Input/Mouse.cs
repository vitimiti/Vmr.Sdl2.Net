// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
