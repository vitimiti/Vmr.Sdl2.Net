// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net;

[Flags]
public enum ApplicationSubsystems : uint
{
    None = 0,
    Timer = 0x00000001U,
    Audio = 0x00000010U,
    Video = 0x00000020U,
    Joystick = 0x00000200U,
    Haptic = 0x00001000U,
    GameController = 0x00002000U,
    Events = 0x00004000U,
    Sensor = 0x00008000U,
    Everything = Timer | Audio | Video | Events | Joystick | Haptic | GameController | Sensor
}
