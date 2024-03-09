// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[NativeMarshalling(typeof(SafeHandleMarshaller<SteamSafeHandle>))]
public class SteamSafeHandle : SafeHandleMinusOneIsInvalid
{
    internal SteamSafeHandle(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        handle = nint.Zero;
        return true;
    }
}
