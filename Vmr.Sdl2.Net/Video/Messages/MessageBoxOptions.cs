// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video.Messages;

[Flags]
public enum MessageBoxOptions : uint
{
    None = 0U,
    Error = 0x00000010U,
    Warning = 0x00000020U,
    Information = 0x00000040U,
    ButtonsLeftToRight = 0x00000080U,
    ButtonsRightToLeft = 0x00000100U
}
