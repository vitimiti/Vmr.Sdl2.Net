// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Input.MouseUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class MouseMotionEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    uint mouseInstanceId,
    uint state,
    Point positionRelativeToWindow,
    MouseRelativeMotion mouseRelativeMotion
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public uint MouseInstanceId { get; private set; } = mouseInstanceId;
    public uint State { get; private set; } = state;
    public Point PositionRelativeToWindow { get; private set; } = positionRelativeToWindow;
    public MouseRelativeMotion MouseRelativeMotion { get; private set; } = mouseRelativeMotion;
}
