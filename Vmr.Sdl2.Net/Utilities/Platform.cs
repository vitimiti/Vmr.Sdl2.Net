// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class Platform
{
    public static string? Name => Sdl.GetPlatform();
}
