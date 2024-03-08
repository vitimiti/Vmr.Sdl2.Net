// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.Input.MouseUtilities;

public readonly struct MouseState
{
    public MouseButtons Buttons { get; internal init; }
    public Point Position { get; internal init; }
}
