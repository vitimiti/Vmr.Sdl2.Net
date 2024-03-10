// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Microsoft.Win32.SafeHandles;

namespace Vmr.Sdl2.Net.Utilities;

public class SysWmMessageSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal SysWmMessageSafeHandle(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    protected override bool ReleaseHandle()
    {
        handle = nint.Zero;
        return true;
    }
}
