// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement;

public class TouchFingerEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long touchDeviceId,
    long fingerId,
    PointF normalizedPosition,
    PointF normalizedDeltaPosition,
    float pressure,
    uint windowId
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long TouchDeviceId { get; private set; } = touchDeviceId;
    public long FingerId { get; private set; } = fingerId;
    public PointF NormalizedPosition { get; private set; } = normalizedPosition;
    public PointF NormalizedDeltaPosition { get; private set; } = normalizedDeltaPosition;
    public float Pressure { get; private set; } = pressure;
    public uint WindowId { get; private set; } = windowId;
}
