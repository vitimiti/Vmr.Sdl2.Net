// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public class PrimarySelection
{
    public static string? Text => Sdl.GetPrimarySelectionText();
    public static bool HasText => Sdl.HasPrimarySelectionText();

    public static void SetText(string? text)
    {
        int code = Sdl.SetPrimarySelectionText(text);
        if (code < 0)
        {
            throw new PrimarySelectionException(
                $"Unable to set the primary selection text to '{text}'",
                code
            );
        }
    }
}
