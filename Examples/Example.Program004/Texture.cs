// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
