// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickDeviceEventArgs(EventType type, TimeSpan timeStamp, int which) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public int JoystickDeviceIndex { get; private set; } = which;
}
