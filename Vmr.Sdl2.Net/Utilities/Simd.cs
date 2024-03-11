// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

[NativeMarshalling(typeof(SafeHandleMarshaller<Simd>))]
public class Simd : SafeHandleZeroOrMinusOneIsInvalid
{
    public Simd(uint length, bool ownsHandle = true)
        : base(ownsHandle)
    {
        handle = Sdl.SimdAlloc(length);
        if (handle == nint.Zero)
        {
            throw new SimdException($"Unable to allocate a SIMD block of {length} bytes");
        }
    }

    public static ulong Alignment => Sdl.SimdGetAlignment();

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

    public void Realloc(uint length)
    {
        nint resultHandle = Sdl.SimdRealloc(this, length);
        if (resultHandle == nint.Zero)
        {
            throw new SimdException(
                $"Unable to reallocate the SIMD block to length {length} bytes"
            );
        }

        handle = resultHandle;
    }
}
