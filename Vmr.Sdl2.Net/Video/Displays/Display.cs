// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Video.Displays;

[Serializable]
public static class Display
{
    public static int Count => Sdl.GetNumVideoDisplays();
    public static bool IsScreenSaverDisabled => Sdl.IsScreenSaverEnabled();

    public static string? GetName(int displayIndex)
    {
        return Sdl.GetDisplayName(displayIndex);
    }

    public static Rectangle GetBounds(int displayIndex)
    {
        int code = Sdl.GetDisplayBounds(displayIndex, out Rectangle bounds);
        if (code < 0)
        {
            throw new DisplayException(
                $"Unable to get the bounds for display index {displayIndex}",
                code
            );
        }

        return bounds;
    }

    public static Rectangle GetUsableBounds(int displayIndex)
    {
        int code = Sdl.GetDisplayUsableBounds(displayIndex, out Rectangle usableBounds);
        if (code < 0)
        {
            throw new DisplayException(
                $"Unable to get the usable bounds for display index {displayIndex}",
                code
            );
        }

        return usableBounds;
    }

    public static Dpi GetDpi(int displayIndex)
    {
        int code = Sdl.GetDisplayDpi(displayIndex, out float dDpi, out float hDpi, out float vDpi);
        if (code < 0)
        {
            throw new DisplayException($"Unable to get the DPI for display index {displayIndex}");
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

    public static DisplayMode GetMode(int displayIndex, int modeIndex)
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetDisplayMode(displayIndex, modeIndex, &mode);
            if (code < 0)
            {
                throw new DisplayException(
                    $"Unable to get the display mode index {modeIndex} for display index {displayIndex}",
                    code
                );
            }

            return new DisplayMode(mode);
        }
    }

    public static DisplayMode GetDesktopDisplayMode(int displayIndex)
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetDesktopDisplayMode(displayIndex, &mode);
            if (code < 0)
            {
                throw new DisplayException(
                    $"Unable to get the desktop display mode for display index {displayIndex}",
                    code
                );
            }

            return new DisplayMode(mode);
        }
    }

    public static DisplayMode GetCurrentDisplayMode(int displayIndex)
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetCurrentDisplayMode(displayIndex, &mode);
            if (code < 0)
            {
                throw new DisplayException(
                    $"Unable to get the current display mode for display index {displayIndex}",
                    code
                );
            }

            return new DisplayMode(mode);
        }
    }

    public static DisplayMode GetClosestDisplayMode(int displayIndex, DisplayMode desiredMode)
    {
        unsafe
        {
            Sdl.DisplayMode closest = new();
            nint closestReturn = Sdl.GetClosestDisplayMode(displayIndex, desiredMode, &closest);

            if (closestReturn == nint.Zero)
            {
                throw new DisplayException(
                    $"Unable to get the closest display mode to {desiredMode} from display index {displayIndex}"
                );
            }

            return new DisplayMode(closest);
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
