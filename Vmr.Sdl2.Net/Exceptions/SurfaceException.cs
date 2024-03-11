// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class SurfaceException : Exception
{
    public SurfaceException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public SurfaceException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}
