// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Video.Displays;

namespace Vmr.Sdl2.Net.EventsManagement.DisplayIdEvents;

public class DisplayOrientationEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint displayId,
    DisplayOrientation orientation
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint DisplayId { get; private set; } = displayId;
    public DisplayOrientation Orientation { get; private set; } = orientation;
}
