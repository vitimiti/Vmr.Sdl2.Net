// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class SensorException : Exception
{
    public SensorException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public SensorException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}
