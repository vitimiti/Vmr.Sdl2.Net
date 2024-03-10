// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class DropEventArgs(EventType type, TimeSpan timeStamp, string? file, uint windowId)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public string? File { get; private set; } = file;
    public uint WindowId { get; private set; } = windowId;
}
