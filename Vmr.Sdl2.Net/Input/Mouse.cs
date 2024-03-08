// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video;

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

    public static Window? GetFocus(ErrorHandler errorHandler)
    {
        nint handle = Sdl.GetMouseFocus();
        if (handle != nint.Zero)
        {
            return new Window(handle, false);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static void Warp(Point position, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.WarpMouseGlobal(position.X, position.Y);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static void SetRelativeMode(bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetRelativeMouseMode(enabled);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static void Capture(bool captured, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.CaptureMouse(captured);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }
}
