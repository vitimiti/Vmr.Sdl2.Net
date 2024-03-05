// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public class Simd : SafeHandleZeroOrMinusOneIsInvalid
{
    public static ulong Alignment => Sdl.SimdGetAlignment();

    public Simd(uint length, ErrorHandler errorHandler, bool ownsHandle = true)
        : base(ownsHandle)
    {
        handle = Sdl.SimdAlloc(length);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.SimdFree(handle);
        handle = nint.Zero;
        return true;
    }

    public void Realloc(uint length, ErrorHandler errorHandler)
    {
        nint resultHandle = Sdl.SimdRealloc(handle, length);
        if (resultHandle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }

        handle = resultHandle;
    }
}
