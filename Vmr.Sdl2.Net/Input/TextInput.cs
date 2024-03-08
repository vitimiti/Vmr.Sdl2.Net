// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input;

public sealed class TextInput : IDisposable
{
    public TextInput()
    {
        Sdl.StartTextInput();
    }

    public static bool IsActive => Sdl.IsTextInputActive();
    public static bool IsShown => Sdl.IsTextInputShown();

    public void Dispose()
    {
        Sdl.StopTextInput();
    }

    public static void ClearComposition()
    {
        Sdl.ClearComposition();
    }

    public static void SetRectangle(Rectangle rectangle)
    {
        Sdl.SetTextInputRect(rectangle);
    }
}
