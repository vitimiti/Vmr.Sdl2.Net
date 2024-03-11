// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Video.Messages;

public static class MessageBox
{
    public static int Show(MessageBoxData messageBoxData)
    {
        int code = Sdl.ShowMessageBox(messageBoxData, out int buttonId);
        if (code < 0)
        {
            throw new MessageBoxException(
                $"Unable to show a message box with data {messageBoxData}",
                code
            );
        }

        return buttonId;
    }

    public static void Show(MessageBoxOptions options, string title, string message, Window? window)
    {
        int code = window is null
            ? Sdl.ShowSimpleMessageBox(options, title, message, nint.Zero)
            : Sdl.ShowSimpleMessageBox(options, title, message, window);

        if (code < 0)
        {
            throw new MessageBoxException("Unable to show a simple message box", code);
        }
    }
}
