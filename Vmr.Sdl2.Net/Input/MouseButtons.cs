// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input;

[Flags]
public enum MouseButtons : uint
{
    None = 0U,
    Left = 1U << 0,
    Middle = 1U << 1,
    Right = 1U << 2,
    X1 = 1U << 3,
    X2 = 1U << 4
}
