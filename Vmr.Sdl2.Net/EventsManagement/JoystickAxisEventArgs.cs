// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickAxisEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    byte axisIndex,
    short value
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public byte AxisIndex { get; private set; } = axisIndex;
    public short Value { get; private set; } = value;
}
