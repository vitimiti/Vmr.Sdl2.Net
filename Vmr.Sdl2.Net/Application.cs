// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net;

public sealed class Application : IDisposable
{
    private readonly bool _isInitialized;

    public static ApplicationSubsystems InitializedSubsystems =>
        Sdl.WasInit(ApplicationSubsystems.None);

    public Application(ApplicationSubsystems subsystems, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.Init(subsystems);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
        else
        {
            _isInitialized = true;
        }
    }

    public void Dispose()
    {
        if (_isInitialized)
        {
            Sdl.Quit();
        }
    }
}
