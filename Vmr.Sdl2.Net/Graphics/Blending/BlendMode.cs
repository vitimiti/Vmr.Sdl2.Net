// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Graphics.Blending;

public enum BlendMode
{
    None = 0,
    AlphaBlending = 1 << 0,
    Additive = 1 << 1,
    ColorModulate = 1 << 2,
    ColorMultiply = 1 << 3,
    Invalid = 0x7FFFFFFF
}
