// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Graphics.Colors;

public struct ColorMasks
{
    public int BitsPerPixel { get; set; }
    public uint Red { get; set; }
    public uint Green { get; set; }
    public uint Blue { get; set; }
    public uint Alpha { get; set; }

    public uint ToPixelFormat()
    {
        return Sdl.MasksToPixelFormatEnum(BitsPerPixel, Red, Green, Blue, Alpha);
    }
}
