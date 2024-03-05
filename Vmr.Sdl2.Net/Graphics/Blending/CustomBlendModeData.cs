// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Graphics.Blending;

public struct CustomBlendModeData
{
    public BlendFactor SrcFactor { get; set; }
    public BlendFactor DstFactor { get; set; }
    public BlendOperation Operation { get; set; }
}
