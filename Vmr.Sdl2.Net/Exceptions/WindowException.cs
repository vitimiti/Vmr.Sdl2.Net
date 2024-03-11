// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class WindowException : Exception
{
    public WindowException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public WindowException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}