// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement.WindowIdEvents;

public class WindowDisplayChangedEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    WindowEventId eventId,
    int displayIndex
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public WindowEventId EventId { get; private set; } = eventId;
    public int DisplayIndex { get; private set; } = displayIndex;
}
