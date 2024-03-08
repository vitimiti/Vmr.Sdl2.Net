// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[Flags]
public enum GameControllerButtons : uint
{
    None = 0U,
    A = 1U << 0,
    B = 1U << 1,
    X = 1U << 2,
    Y = 1U << 3,
    Back = 1U << 4,
    Guide = 1U << 5,
    Start = 1U << 6,
    LeftStick = 1U << 7,
    RightStick = 1U << 8,
    LeftShoulder = 1U << 9,
    RightShoulder = 1U << 10,
    DpadUp = 1U << 11,
    DpadDown = 1U << 12,
    DpadLeft = 1U << 13,
    DpadRight = 1U << 14,
    Misc1 = 1U << 15,
    Paddle1 = 1U << 16,
    Paddle2 = 1U << 17,
    Paddle3 = 1U << 18,
    Paddle4 = 1U << 19,
    Touchpad = 1U << 20
}
