// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(OwnedUtf8StringMarshaller))]
internal static unsafe class OwnedUtf8StringMarshaller
{
    public static string? ConvertToManaged(byte* unmanaged)
    {
        return Utf8StringMarshaller.ConvertToManaged(unmanaged);
    }

    public static byte* ConvertToUnmanaged(string? managed)
    {
        return Utf8StringMarshaller.ConvertToUnmanaged(managed);
    }
}
