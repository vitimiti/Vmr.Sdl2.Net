// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.Video.Windowing;

public delegate HitTestResult HitTestFunction(Window window, Point area, byte[]? data);

public enum HitTestResult
{
    Normal,
    Draggable,
    ResizeTopLeft,
    ResizeTop,
    ResizeTopRight,
    ResizeRight,
    ResizeBottomRight,
    ResizeBottom,
    ResizeBottomLeft,
    ResizeLeft
}
