// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

public class VirtualJoystick : IDisposable
{
    private readonly ErrorCodeHandler _errorHandler;

    public VirtualJoystick(
        JoystickType type,
        int numberOfAxes,
        int numberOfButtons,
        int numberOfHats,
        ErrorCodeHandler errorHandler
    )
    {
        int index = Sdl.JoystickAttachVirtual(type, numberOfAxes, numberOfButtons, numberOfHats);
        if (index < 0)
        {
            errorHandler(Sdl.GetError(), index);
        }

        Index = index;
        _errorHandler = errorHandler;
    }

    public VirtualJoystick(VirtualJoystickDesc desc, ErrorCodeHandler errorHandler)
    {
        int index = Sdl.JoystickAttachVirtualEx(desc);
        if (index < 0)
        {
            errorHandler(Sdl.GetError(), index);
        }

        Index = index;
        _errorHandler = errorHandler;
    }

    public int Index { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        int code = Sdl.JoystickDetachVirtual(Index);
        if (code < 0)
        {
            _errorHandler(Sdl.GetError(), code);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            // Nothing to do here.
        }
    }

    ~VirtualJoystick()
    {
        Dispose(false);
    }
}
