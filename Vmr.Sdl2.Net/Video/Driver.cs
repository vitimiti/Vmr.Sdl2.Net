// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Video;

public static class Driver
{
    public static int Count => Sdl.GetNumVideoDrivers();
    public static string? CurrentName => Sdl.GetCurrentVideoDriver();

    public static string? GetName(int driverIndex)
    {
        return Sdl.GetVideoDriver(driverIndex);
    }
}
