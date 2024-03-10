// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement;

public class DollarGestureEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long touchDeviceId,
    long gestureId,
    uint numberOfFingers,
    float error,
    PointF normalizedCenterOfGesture
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long TouchDeviceId { get; private set; } = touchDeviceId;
    public long GestureId { get; private set; } = gestureId;
    public uint NumberOfFingers { get; private set; } = numberOfFingers;
    public float Error { get; private set; } = error;
    public PointF NormalizedCenterOfGesture { get; private set; } = normalizedCenterOfGesture;
}
