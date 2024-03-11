// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class MouseException : Exception
{
    public MouseException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public MouseException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}
