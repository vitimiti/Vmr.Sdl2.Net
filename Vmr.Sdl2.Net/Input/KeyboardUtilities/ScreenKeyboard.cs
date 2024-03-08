// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

public static class ScreenKeyboard
{
    public static bool IsSupported => Sdl.HasScreenKeyboardSupport();
    public static bool IsShown => Sdl.IsScreenKeyboardShown();
}
