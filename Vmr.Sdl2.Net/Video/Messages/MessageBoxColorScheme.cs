// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.Video.Messages;

public struct MessageBoxColorScheme
{
    public required Color Background { get; set; }
    public required Color Text { get; set; }
    public required Color ButtonBorder { get; set; }
    public required Color ButtonBackground { get; set; }
    public required Color ButtonSelected { get; set; }
}
