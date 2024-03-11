// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net;

public class Application : IDisposable
{
    private readonly ApplicationSubsystems _subsystems;

    public Application(ApplicationSubsystems subsystems)
    {
        _subsystems = subsystems;
        ShouldQuit = false;
    }

    public static ApplicationSubsystems InitializedSubsystems =>
        Sdl.WasInit(ApplicationSubsystems.None);

    public static bool ShouldQuit { get; set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        Sdl.Quit();
    }

    protected virtual void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            // Nothing to do here.
        }
    }

    ~Application()
    {
        Dispose(false);
    }

    protected virtual void Init()
    {
        int code = Sdl.Init(_subsystems);
        if (code < 0)
        {
            throw new AppException(
                $"Unable to initialize SDL2 with subsystems [{_subsystems}]",
                code
            );
        }
    }

    protected virtual void Load() { }

    protected virtual void Update()
    {
        Events.Poll();
    }

    public void Run()
    {
        Init();
        Load();
        while (!ShouldQuit)
        {
            Update();
        }
    }
}
