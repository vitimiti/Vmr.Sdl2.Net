// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

try
{
    using Application application = new(ApplicationSubsystems.Video);
    using Window window =
        new(
            "Example Vmr.Sdl2.Net Program 001",
            new Point(WindowPredefinedPosition.Centered),
            new Size(640, 480),
            WindowOptions.Shown
        );

    using Surface screenSurface = window.GetSurface();
    screenSurface.Fill(Rectangle.Empty, screenSurface.PixelFormat?.MapRgb(Color.Black) ?? 0U);

    window.UpdateSurface();
    Events.OnQuit += (_, _) => Application.ShouldQuit = true;
    application.Run();
}
catch (Exception e)
{
    MessageBox.Show(MessageBoxOptions.Error, "Unmanaged Exception", e.ToString(), null);

    throw;
}
