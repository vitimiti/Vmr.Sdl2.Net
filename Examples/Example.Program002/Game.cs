// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Example.Program002.Settings;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program002;

public class Game(WindowSettings windowSettings) : Application(ApplicationSubsystems.Video)
{
    private Surface? _helloWorld;
    private FileStream? _helloWorldFileStream;
    private RwOps? _helloWorldRwOps;
    private Surface? _screenSurface;
    private Window? _window;

    protected override void Init()
    {
        base.Init();

        Events.OnQuit += (_, _) => ShouldQuit = true;

        _window = new Window(
            windowSettings.Title,
            windowSettings.Position,
            windowSettings.Size,
            WindowOptions.Hidden
        );

        _screenSurface = _window.GetSurface();
    }

    protected override void Load()
    {
        base.Load();

        _helloWorldFileStream = new FileStream(
            "res/hello_world.bmp",
            FileMode.Open,
            FileAccess.Read
        );

        _helloWorldRwOps = new RwOps(_helloWorldFileStream);
        _helloWorld = Surface.LoadBmp(_helloWorldRwOps);
        Rectangle dstRect = Rectangle.Empty;
        _helloWorld.Blit(Rectangle.Empty, _screenSurface!, ref dstRect, false);

        _window?.Show();
        _window?.UpdateSurface();
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _helloWorld?.Dispose();
        _helloWorldRwOps?.Dispose();
        _helloWorldFileStream?.Dispose();
        _screenSurface?.Dispose();
        _window?.Dispose();

        base.Dispose(disposing);
    }
}
