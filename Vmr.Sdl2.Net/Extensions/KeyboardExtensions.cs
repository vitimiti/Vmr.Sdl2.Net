// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input;
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
