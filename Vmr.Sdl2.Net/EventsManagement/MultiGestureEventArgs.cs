// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Input.MultiGestureUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class MultiGestureEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long touchDeviceId,
    MultiGestureDelta delta,
    PointF position,
    ushort numberOfFingers
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long TouchDeviceId { get; private set; } = touchDeviceId;
    public MultiGestureDelta Delta { get; private set; } = delta;
    public PointF Position { get; private set; } = position;
    public ushort NumberOfFingers { get; private set; } = numberOfFingers;
}
