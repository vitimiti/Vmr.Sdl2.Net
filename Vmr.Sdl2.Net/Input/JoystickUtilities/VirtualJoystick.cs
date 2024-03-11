// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

public class VirtualJoystick : IDisposable
{
    public VirtualJoystick(
        JoystickType type,
        int numberOfAxes,
        int numberOfButtons,
        int numberOfHats
    )
    {
        int index = Sdl.JoystickAttachVirtual(type, numberOfAxes, numberOfButtons, numberOfHats);
        if (index < 0)
        {
            throw new JoystickException(
                $"Unable to create a virtual joystick as {{Type: {type}, Number of Axes: {numberOfAxes}, Number of Buttons: {numberOfButtons}, Number of Hats: {numberOfHats}}}",
                index
            );
        }

        Index = index;
    }

    public VirtualJoystick(VirtualJoystickDesc desc)
    {
        int index = Sdl.JoystickAttachVirtualEx(desc);
        if (index < 0)
        {
            throw new JoystickException(
                $"Unable to create a virtual joystick as described: {desc}",
                index
            );
        }

        Index = index;
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
            throw new JoystickException("Unable to detach the virtual joystick");
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
