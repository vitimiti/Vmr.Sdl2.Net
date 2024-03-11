﻿// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Example.Program004;
using Example.Program004.Settings;

using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

try
{
    Game game =
        new(
            new WindowSettings
            {
                Title = "Example Vmr.Sdl2.Net Program 004",
                Position = new Point(WindowPredefinedPosition.Centered),
                Size = new Size(640, 480)
            }
        );

    game.Run();
}
catch (Exception e)
{
    MessageBox.Show(MessageBoxOptions.Error, "Unmanaged Exception", e.ToString(), null);
    throw;
}
