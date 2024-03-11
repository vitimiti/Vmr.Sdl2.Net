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
