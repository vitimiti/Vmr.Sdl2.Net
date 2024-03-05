// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Graphics.Pixels;

public enum PixelType : uint
{
    Unknown,
    Index1,
    Index2 = 12,
    Index4 = 2,
    Index8,
    Packed8,
    Packed16,
    Packed32,
    ArrayU8,
    ArrayU16,
    ArrayU32,
    ArrayF16,
    ArrayF32
}
