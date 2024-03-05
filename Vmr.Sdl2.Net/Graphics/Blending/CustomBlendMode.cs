// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Graphics.Blending;

public static class CustomBlendMode
{
    public static BlendMode Compose(
        CustomBlendModeData colorCustomBlendModeData,
        CustomBlendModeData alphaCustomBlendModeData
    )
    {
        return Sdl.ComposeCustomBlendMode(
            colorCustomBlendModeData.SrcFactor,
            colorCustomBlendModeData.DstFactor,
            colorCustomBlendModeData.Operation,
            alphaCustomBlendModeData.SrcFactor,
            alphaCustomBlendModeData.DstFactor,
            alphaCustomBlendModeData.Operation
        );
    }
}
