// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.SensorUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerSensorEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    SensorType sensor,
    float[] data,
    TimeSpan hardwareTimeStamp
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public SensorType Sensor { get; private set; } = sensor;
    public float[] Data { get; private set; } = data;
    public TimeSpan HardwareTimeStamp { get; private set; } = hardwareTimeStamp;
}
