// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Version), MarshalMode.Default, typeof(SdlVersionMarshaller))]
internal static class SdlVersionMarshaller
{
    public static Version ConvertToManaged(SdlVersion unmanaged)
    {
        return new Version(unmanaged.Major, unmanaged.Minor, unmanaged.Patch);
    }

    public static SdlVersion ConvertToUnmanaged(Version managed)
    {
        return new SdlVersion
        {
            Major = (byte)managed.Major,
            Minor = (byte)managed.Minor,
            Patch = (byte)managed.Revision
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlVersion
    {
        public byte Major;
        public byte Minor;
        public byte Patch;
    }
}
