// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public class PrimarySelection
{
    public static string? Text => Sdl.GetPrimarySelectionText();
    public static bool HasText => Sdl.HasPrimarySelectionText();

    public static void SetText(string? text, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetPrimarySelectionText(text);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }
}
