// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class TouchDeviceException : Exception
{
    public TouchDeviceException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public TouchDeviceException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}
