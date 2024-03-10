// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.JoystickUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickHatEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    byte hatIndex,
    HatPositions value
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public byte HatIndex { get; private set; } = hatIndex;
    public HatPositions Value { get; private set; } = value;
}
