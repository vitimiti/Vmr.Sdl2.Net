// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video.Messages;

[Flags]
public enum MessageBoxButtonOptions : uint
{
    None = 0U,
    ReturnKeyDefault = 1U << 0,
    EscapeKeyDefault = 1U << 1
}
