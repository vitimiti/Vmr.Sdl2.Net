// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Guid), MarshalMode.Default, typeof(SdlGuidMarshaller))]
internal static unsafe class SdlGuidMarshaller
{
    public static Guid ConvertToManaged(SdlGuid unmanaged)
    {
        const int bufferSize = 256;
        byte* buffer = (byte*)NativeMemory.Alloc(bufferSize);
        try
        {
            Sdl.GuidToString(unmanaged, buffer, bufferSize);
            return new Guid(Utf8StringMarshaller.ConvertToManaged(buffer) ?? string.Empty);
        }
        finally
        {
            NativeMemory.Free(buffer);
        }
    }

    public static SdlGuid ConvertToUnmanaged(Guid managed)
    {
        return Sdl.GuidFromString(managed.ToString());
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlGuid
    {
        public fixed byte Data[16];
    }
}
