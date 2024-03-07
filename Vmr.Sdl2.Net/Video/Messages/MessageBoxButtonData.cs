// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video.Messages;

public struct MessageBoxButtonData
{
    public required MessageBoxButtonOptions Flags { get; set; }
    public required int ButtonId { get; set; }
    public required string Text { get; set; }
}
