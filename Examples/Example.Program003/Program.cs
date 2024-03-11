// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Example.Program002.Settings;
using Example.Program003;

using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

try
{
    Game game =
        new(
            new WindowSettings
            {
                Title = "Example Vmr.Sdl2.Net Program 003",
                Position = new Point(WindowPredefinedPosition.Centered),
                Size = new Size(640, 480),
                Flags = WindowOptions.Shown
            }
        );

    game.Run();
}
catch (Exception e)
{
    MessageBox.Show(
        MessageBoxOptions.Error,
        "Unmanaged Exception",
        e.ToString(),
        null,
        (_, _) => Console.Error.WriteLine(e)
    );

    throw;
}
