// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.CommonUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickButtonEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    byte buttonIndex,
    ButtonState state
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public byte ButtonIndex { get; private set; } = buttonIndex;
    public ButtonState State { get; private set; } = state;
}
