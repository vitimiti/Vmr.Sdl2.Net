// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net;

public delegate void ErrorHandler(string? message);
public delegate void ErrorCodeHandler(string? message, int errorCode);

public static class Error
{
    public static void DefaultErrorHandler(string? message)
    {
        throw new ExternalException(message);
    }

    public static void DefaultErrorCodeHandler(string? message, int errorCode)
    {
        throw new ExternalException(message, errorCode);
    }
}
