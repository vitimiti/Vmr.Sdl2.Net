// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Example.Program003.Settings;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program003;

public class Game(WindowSettings windowSettings) : Application(ApplicationSubsystems.Video)
{
    private Surface? _screenSurface;
    private Window? _window;
    private Surface? _xOut;
    private Rectangle _xOutDstRect = Rectangle.Empty;
    private FileStream? _xOutFileStream;
    private RwOps? _xOutRwOps;

    protected override void Init()
    {
        base.Init();

        Events.OnQuit += (_, _) => ShouldQuit = true;
        Events.OnKeyDown += (_, eventArgs) =>
        {
            if (eventArgs.KeySymbol.KeyCode == KeyCode.Escape)
            {
                ShouldQuit = true;
            }
        };

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

        _xOutFileStream = new FileStream("res/x_out.bmp", FileMode.Open, FileAccess.Read);
        _xOutRwOps = new RwOps(_xOutFileStream);
        _xOut = Surface.LoadBmp(_xOutRwOps);

        _window?.Show();
    }

    protected override void Update()
    {
        base.Update();

        _xOut?.Blit(Rectangle.Empty, _screenSurface!, ref _xOutDstRect, false);
        _window?.UpdateSurface();
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _xOut?.Dispose();
        _xOutRwOps?.Dispose();
        _xOutFileStream?.Dispose();
        _screenSurface?.Dispose();
        _window?.Dispose();

        base.Dispose(disposing);
    }
}
