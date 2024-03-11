// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;
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

    public static Window? GetCurrentWindow(ErrorHandler errorHandler)
    {
        nint result = Sdl.GlGetCurrentWindow();
        if (result != nint.Zero)
        {
            return new Window(result, false);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static Context? GetCurrent(ErrorHandler errorHandler)
    {
        nint result = Sdl.GlGetCurrentContext();
        if (result != nint.Zero)
        {
            return new Context(result, false);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static void SetSwapInterval(SwapInterval interval, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GlSetSwapInterval(interval);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
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
