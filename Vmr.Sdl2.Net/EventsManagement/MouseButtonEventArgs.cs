// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Input.CommonUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class MouseButtonEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    uint mouseInstanceId,
    byte buttonIndex,
    ButtonState state,
    byte clicks,
    Point positionRelativeToWindow
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public uint MouseInstanceId { get; private set; } = mouseInstanceId;
    public byte ButtonIndex { get; private set; } = buttonIndex;
    public ButtonState State { get; private set; } = state;
    public byte Clicks { get; private set; } = clicks;
    public Point PositionRelativeToWindow { get; private set; } = positionRelativeToWindow;
}
