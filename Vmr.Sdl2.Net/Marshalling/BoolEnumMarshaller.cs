// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(bool), MarshalMode.Default, typeof(BoolEnumMarshaller))]
internal static class BoolEnumMarshaller
{
    public static bool ConvertToManaged(Bool unmanaged)
    {
        return unmanaged switch
        {
            Bool.True => true,
            _ => false
        };
    }

    public static Bool ConvertToUnmanaged(bool managed)
    {
        return managed ? Bool.True : Bool.False;
    }

    internal enum Bool
    {
        False,
        True
    }
}
