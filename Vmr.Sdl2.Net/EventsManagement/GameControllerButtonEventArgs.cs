// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.GameControllerUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerButtonEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    GameControllerButton button,
    ButtonState state
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public GameControllerButton Button { get; private set; } = button;
    public ButtonState State { get; private set; } = state;
}
