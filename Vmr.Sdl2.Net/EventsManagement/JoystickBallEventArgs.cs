// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.JoystickUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class JoystickBallEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    byte ballIndex,
    BallRelativeMotion ballRelativeMotion
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public byte BallIndex { get; private set; } = ballIndex;
    public BallRelativeMotion BallRelativeMotion { get; private set; } = ballRelativeMotion;
}
