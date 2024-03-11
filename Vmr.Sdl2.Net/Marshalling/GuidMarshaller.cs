// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

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
