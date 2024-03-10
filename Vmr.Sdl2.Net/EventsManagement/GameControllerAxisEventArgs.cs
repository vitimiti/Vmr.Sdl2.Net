// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.GameControllerUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerAxisEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    GameControllerAxis axis,
    short value
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public GameControllerAxis Axis { get; private set; } = axis;
    public short Value { get; private set; } = value;
}
