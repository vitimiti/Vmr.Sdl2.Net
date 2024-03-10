// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class TextEditingEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    string? text,
    int start,
    int length
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public string? Text { get; private set; } = text;
    public int Start { get; private set; } = start;
    public int Length { get; private set; } = length;
}
