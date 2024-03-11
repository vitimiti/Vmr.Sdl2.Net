// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

using Example.Program004.Settings;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program004;

public class Game(WindowSettings windowSettings) : Application(ApplicationSubsystems.Video)
{
    private readonly Dictionary<TextureType, Texture> _textures = new();
    private Texture? _currentTexture;
    private Surface? _screenSurface;
    private Window? _window;

    protected override void Init()
    {
        base.Init();

        Events.OnQuit += (_, _) => ShouldQuit = true;
        Events.OnKeyDown += (_, eventArgs) =>
        {
            _currentTexture = eventArgs.KeySymbol.KeyCode switch
            {
                KeyCode.Escape => _textures[TextureType.Default],
                KeyCode.Left => _textures[TextureType.Left],
                KeyCode.Up => _textures[TextureType.Up],
                KeyCode.Right => _textures[TextureType.Right],
                KeyCode.Down => _textures[TextureType.Down],
                _ => _currentTexture
            };
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

        _textures.Add(TextureType.Default, new Texture("res/press_me.bmp"));
        _textures.Add(TextureType.Left, new Texture("res/left.bmp"));
        _textures.Add(TextureType.Up, new Texture("res/up.bmp"));
        _textures.Add(TextureType.Right, new Texture("res/right.bmp"));
        _textures.Add(TextureType.Down, new Texture("res/down.bmp"));

        _currentTexture = _textures[TextureType.Default];

        _window?.Show();
    }

    protected override void Update()
    {
        base.Update();

        _currentTexture?.Update(_screenSurface!);
        _window?.UpdateSurface();
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        foreach ((TextureType _, Texture texture) in _textures)
        {
            texture.Dispose();
        }

        _screenSurface?.Dispose();
        _window?.Dispose();

        base.Dispose(disposing);
    }
}
