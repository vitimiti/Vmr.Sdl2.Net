// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(bool), MarshalMode.Default, typeof(SdlBoolMarshaller))]
internal static class SdlBoolMarshaller
{
    public enum SdlBool
    {
        False,
        True
    }

    public static bool ConvertToManaged(SdlBool unmanaged)
    {
        return unmanaged switch
        {
            SdlBool.True => true,
            _ => false
        };
    }

    public static SdlBool ConvertToUnmanaged(bool managed)
    {
        return managed ? SdlBool.True : SdlBool.False;
    }
}
