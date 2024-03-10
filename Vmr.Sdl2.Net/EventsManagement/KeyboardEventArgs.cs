// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class KeyboardEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    KeyState state,
    bool isKeyRepeat,
    KeySym keySym
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public KeyState State { get; private set; } = state;
    public bool IsKeyRepeat { get; private set; } = isKeyRepeat;
    public KeySym KeySym { get; private set; } = keySym;
}
