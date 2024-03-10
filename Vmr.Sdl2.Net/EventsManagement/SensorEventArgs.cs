// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class SensorEventArgs(
    EventType type,
    TimeSpan timeStamp,
    int instanceId,
    float[] data,
    TimeSpan hardwareTimeStamp
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public int InstanceId { get; private set; } = instanceId;
    public float[] Data { get; private set; } = data;
    public TimeSpan HardwareTimeStamp { get; private set; } = hardwareTimeStamp;
}
