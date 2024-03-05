// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public class RwOps : SafeHandleZeroOrMinusOneIsInvalid
{
    public RwOps(Stream stream, ErrorHandler errorHandler)
        : base(true)
    {
        using BinaryReader reader = new(stream);
        handle = Sdl.RwFromMem(reader.ReadBytes((int)stream.Length), (int)stream.Length);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
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
