// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerDeviceEventArgs(EventType type, TimeSpan timeStamp, int joystickDeviceId)
    : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public int JoystickDeviceId { get; private set; } = joystickDeviceId;
}
