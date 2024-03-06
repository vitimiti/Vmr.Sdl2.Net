// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Graphics;

[Flags]
public enum SurfaceMasks : uint
{
    SwSurface = 0U,
    PreAlloc = 1U << 0,
    RleAccel = 1U << 1,
    DontFree = 1U << 2,
    SimdAligned = 1U << 3
}
