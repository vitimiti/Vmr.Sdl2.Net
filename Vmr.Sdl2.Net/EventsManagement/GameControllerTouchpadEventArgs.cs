// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerTouchpadEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    int touchpadIndex,
    int fingerIndex,
    PointF normalizedPosition,
    float pressure
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public int TouchpadIndex { get; private set; } = touchpadIndex;
    public int FingerIndex { get; private set; } = fingerIndex;
    public PointF NormalizedPosition { get; private set; } = normalizedPosition;
    public float Pressure { get; private set; } = pressure;
}
