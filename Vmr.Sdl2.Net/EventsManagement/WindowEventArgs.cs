// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class WindowEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    WindowEventId eventId
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public WindowEventId EventId { get; private set; } = eventId;
}
