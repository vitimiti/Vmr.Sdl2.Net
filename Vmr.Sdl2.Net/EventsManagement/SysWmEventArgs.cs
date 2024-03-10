// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class SysWmEventArgs(EventType type, TimeSpan timeStamp, SysWmMessageSafeHandle msg)
    : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public SysWmMessageSafeHandle Msg { get; private set; } = msg;
}
