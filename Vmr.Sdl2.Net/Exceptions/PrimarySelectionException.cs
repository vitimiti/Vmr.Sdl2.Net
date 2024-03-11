// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Exceptions;

[Serializable]
public class PrimarySelectionException : Exception
{
    public PrimarySelectionException(string? message)
        : base(message, new ExternalException(Sdl.GetError()))
    {
    }

    public PrimarySelectionException(string? message, int code)
        : base(message, new ExternalException(Sdl.GetError(), code))
    {
    }
}
