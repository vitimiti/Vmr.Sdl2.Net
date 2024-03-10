// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Guid), MarshalMode.Default, typeof(GuidMarshaller))]
internal static unsafe class GuidMarshaller
{
    public static System.Guid ConvertToManaged(Guid unmanaged)
    {
        const int bufferSize = 256;
        byte* buffer = (byte*)NativeMemory.Alloc(bufferSize);
        try
        {
            Sdl.GuidToString(unmanaged, buffer, bufferSize);
            return new System.Guid(Utf8StringMarshaller.ConvertToManaged(buffer) ?? string.Empty);
        }
        finally
        {
            NativeMemory.Free(buffer);
        }
    }

    public static Guid ConvertToUnmanaged(System.Guid managed)
    {
        return Sdl.GuidFromString(managed.ToString("N"));
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Guid
    {
        public fixed byte Data[16];
    }
}
