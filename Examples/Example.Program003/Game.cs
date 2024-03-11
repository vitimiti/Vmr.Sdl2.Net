// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

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
