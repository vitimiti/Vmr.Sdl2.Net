// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class Clipboard
{
    public static string? Text => Sdl.GetClipboardText();
    public static bool HasText => Sdl.HasClipboardText();

    public static void SetText(string? text, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetClipboardText(text);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }
}
