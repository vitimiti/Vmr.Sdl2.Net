// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(bool), MarshalMode.Default, typeof(IntBoolMarshaller))]
public static class IntBoolMarshaller
{
    public static bool ConvertToManaged(int unmanaged)
    {
        return unmanaged != 0;
    }

    public static int ConvertToUnmanaged(bool managed)
    {
        return managed ? 1 : 0;
    }
}
