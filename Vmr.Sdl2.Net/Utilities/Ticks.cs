// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class Ticks
{
    public static uint Current => Sdl.GetTicks();
    public static ulong Current64 => Sdl.GetTicks64();

    public static bool Passed(uint current, uint target)
    {
        return (int)(target - current) <= 0;
    }
}
