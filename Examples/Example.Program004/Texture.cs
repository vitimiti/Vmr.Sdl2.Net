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

using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Utilities;

namespace Example.Program004;

public class Texture : IDisposable
{
    private readonly Surface? _current;
    private readonly FileStream? _currentFileStream;
    private readonly RwOps? _currentRwOps;
    private Rectangle _currentDstRect = Rectangle.Empty;

    public Texture(string path)
    {
        _currentFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        _currentRwOps = new RwOps(_currentFileStream);
        _current = Surface.LoadBmp(_currentRwOps);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Update(Surface screenSurface)
    {
        _current?.Blit(Rectangle.Empty, screenSurface, ref _currentDstRect, false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _current?.Dispose();
        _currentFileStream?.Dispose();
        _currentRwOps?.Dispose();
    }
}
