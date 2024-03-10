// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Input.MouseUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class MouseWheelEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    uint mouseInstanceId,
    ScrollAmount scrollAmount,
    MouseWheelDirection direction,
    PreciseScrollAmount preciseScrollAmount,
    Point position
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public uint MouseInstanceId { get; private set; } = mouseInstanceId;
    public ScrollAmount ScrollAmount { get; private set; } = scrollAmount;
    public MouseWheelDirection Direction { get; private set; } = direction;
    public PreciseScrollAmount PreciseScrollAmount { get; private set; } = preciseScrollAmount;
    public Point Position { get; private set; } = position;
}
