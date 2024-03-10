// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(bool), MarshalMode.Default, typeof(ByteBoolMarshaller))]
internal static class ByteBoolMarshaller
{
    public static bool ConvertToManaged(byte unmanaged)
    {
        return unmanaged == 1;
    }

    public static byte ConvertToUnmanaged(bool managed)
    {
        return (byte)(managed ? 1 : 0);
    }
}
