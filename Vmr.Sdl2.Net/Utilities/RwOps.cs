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

using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

[NativeMarshalling(typeof(SafeHandleMarshaller<RwOps>))]
public class RwOps : SafeHandleZeroOrMinusOneIsInvalid
{
    public RwOps(Stream stream)
        : base(true)
    {
        using BinaryReader reader = new(stream);
        handle = Sdl.RwFromMem(reader.ReadBytes((int)stream.Length), (int)stream.Length);
        if (handle == nint.Zero)
        {
            throw new RwOpsException("Unable to create a native SDL stream from the given stream");
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreeRw(handle);
        handle = nint.Zero;
        return true;
    }
}
