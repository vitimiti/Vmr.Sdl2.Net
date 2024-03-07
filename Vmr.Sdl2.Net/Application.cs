// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net;

public sealed class Application : IDisposable
{
    private readonly bool _isInitialized;

    public Application(
        ApplicationSubsystems subsystems,
        ErrorCodeHandler errorHandler,
        VersionMismatchHandler? versionMismatchHandler = null
    )
    {
        versionMismatchHandler?.Invoke(
            NativeLibraryInformation.ExpectedVersion,
            NativeLibraryInformation.Version
        );

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

    public static ApplicationSubsystems InitializedSubsystems =>
        Sdl.WasInit(ApplicationSubsystems.None);

    public void Dispose()
    {
        if (_isInitialized)
        {
            Sdl.Quit();
        }
    }
}
