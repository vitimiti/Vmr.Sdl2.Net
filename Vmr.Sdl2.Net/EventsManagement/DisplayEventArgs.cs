// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class DisplayEventArgs(EventType type, TimeSpan timeStamp, uint displayId) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint DisplayId { get; private set; } = displayId;
}
