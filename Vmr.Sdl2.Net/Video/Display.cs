// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Video;

[Serializable]
public static class Display
{
    public static int Count => Sdl.GetNumVideoDisplays();
    public static bool IsScreenSaverDisabled => Sdl.IsScreenSaverEnabled();

    public static string? GetName(int displayIndex)
    {
        return Sdl.GetDisplayName(displayIndex);
    }

    public static Rectangle GetBounds(int displayIndex, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetDisplayBounds(displayIndex, out Rectangle bounds);
        if (code >= 0)
        {
            return bounds;
        }

        errorHandler(Sdl.GetError(), code);
        return Rectangle.Empty;
    }

    public static Rectangle GetUsableBounds(int displayIndex, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetDisplayUsableBounds(displayIndex, out Rectangle usableBounds);
        if (code >= 0)
        {
            return usableBounds;
        }

        errorHandler(Sdl.GetError(), code);
        return Rectangle.Empty;
    }

    public static Dpi GetDpi(int displayIndex, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetDisplayDpi(displayIndex, out float dDpi, out float hDpi, out float vDpi);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }

        return new Dpi { Diagonal = dDpi, Horizontal = hDpi, Vertical = vDpi };
    }

    public static DisplayOrientation GetOrientation(int displayIndex)
    {
        return Sdl.GetDisplayOrientation(displayIndex);
    }

    public static int CountModes(int displayIndex)
    {
        return Sdl.GetNumDisplayModes(displayIndex);
    }

    public static DisplayMode? GetMode(
        int displayIndex,
        int modeIndex,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetDisplayMode(displayIndex, modeIndex, &mode);
            if (code >= 0)
            {
                return new DisplayMode(mode);
            }

            errorHandler(Sdl.GetError(), code);
            return null;
        }
    }

    public static DisplayMode? GetDesktopDisplayMode(
        int displayIndex,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetDesktopDisplayMode(displayIndex, &mode);
            if (code >= 0)
            {
                return new DisplayMode(mode);
            }

            errorHandler(Sdl.GetError(), code);
            return null;
        }
    }

    public static DisplayMode? GetCurrentDisplayMode(
        int displayIndex,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetCurrentDisplayMode(displayIndex, &mode);
            if (code >= 0)
            {
                return new DisplayMode(mode);
            }

            errorHandler(Sdl.GetError(), code);
            return null;
        }
    }

    public static DisplayMode? GetClosestDisplayMode(
        int displayIndex,
        DisplayMode desiredMode,
        ErrorHandler errorHandler
    )
    {
        unsafe
        {
            Sdl.DisplayMode closest = new();
            nint closestReturn = Sdl.GetClosestDisplayMode(displayIndex, desiredMode, &closest);

            if (closestReturn != nint.Zero)
            {
                return new DisplayMode(closest);
            }

            errorHandler(Sdl.GetError());
            return null;
        }
    }

    public static void EnableScreenSaver(bool isEnabled)
    {
        if (isEnabled)
        {
            Sdl.EnableScreenSaver();
        }
        else
        {
            Sdl.DisableScreenSaver();
        }
    }
}
