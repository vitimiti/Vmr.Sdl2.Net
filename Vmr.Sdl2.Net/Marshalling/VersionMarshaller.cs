// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

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
