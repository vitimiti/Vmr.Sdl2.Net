// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.JoystickUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickBatteryEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    JoystickPowerLevel level
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public JoystickPowerLevel Level { get; private set; } = level;
}
