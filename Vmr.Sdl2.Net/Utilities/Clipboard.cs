// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
