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

using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Video.OpenGl;

[NativeMarshalling(typeof(SafeHandleMarshaller<Context>))]
public class Context : SafeHandleZeroOrMinusOneIsInvalid
{
    internal Context(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public static SwapInterval SwapInterval => Sdl.GlGetSwapInterval();

    public static Window GetCurrentWindow()
    {
        nint result = Sdl.GlGetCurrentWindow();
        if (result == nint.Zero)
        {
            throw new ContextException("Unable to get the window with the current context");
        }

        return new Window(result, false);
    }

    public static Context GetCurrent()
    {
        nint result = Sdl.GlGetCurrentContext();
        if (result == nint.Zero)
        {
            throw new ContextException("Unable to get the current context");
        }

        return new Context(result, false);
    }

    public static void SetSwapInterval(SwapInterval interval)
    {
        int code = Sdl.GlSetSwapInterval(interval);
        if (code < 0)
        {
            throw new ContextException($"Unable to set the swap interval to {interval}", code);
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.GlDeleteContext(handle);
        handle = nint.Zero;
        return true;
    }
}
