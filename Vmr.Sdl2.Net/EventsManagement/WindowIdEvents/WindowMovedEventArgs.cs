// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement.WindowIdEvents;

public class WindowMovedEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    WindowEventId eventId,
    Point position
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public WindowEventId EventId { get; private set; } = eventId;
    public Point Position { get; private set; } = position;
}
