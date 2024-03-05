// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class Performance
{
    public static ulong Counter => Sdl.GetPerformanceCounter();
    public static ulong Frequency => Sdl.GetPerformanceFrequency();
}
