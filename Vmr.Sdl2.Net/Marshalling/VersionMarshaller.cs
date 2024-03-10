// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Version), MarshalMode.Default, typeof(VersionMarshaller))]
internal static class VersionMarshaller
{
    public static System.Version ConvertToManaged(Version unmanaged)
    {
        return new System.Version(unmanaged.Major, unmanaged.Minor, unmanaged.Patch);
    }

    public static Version ConvertToUnmanaged(System.Version managed)
    {
        return new Version
        {
            Major = (byte)managed.Major,
            Minor = (byte)managed.Minor,
            Patch = (byte)managed.Revision
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Version
    {
        public byte Major;
        public byte Minor;
        public byte Patch;
    }
}
