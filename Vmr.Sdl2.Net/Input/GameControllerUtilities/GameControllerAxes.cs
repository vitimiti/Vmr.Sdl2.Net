// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[Flags]
public enum GameControllerAxes : uint
{
    None = 0U,
    LeftX = 1U << 0,
    LeftY = 1U << 1,
    RightX = 1U << 2,
    RightY = 1U << 3,
    TriggerLeft = 1U << 4,
    TriggerRight = 1U << 5
}
