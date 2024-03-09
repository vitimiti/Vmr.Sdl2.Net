// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public enum WindowEventId : byte
{
    None,
    Shown,
    Hidden,
    Exposed,
    Moved,
    Resized,
    SizeChanged,
    Minimized,
    Maximized,
    Restored,
    Enter,
    Leave,
    FocusGained,
    FocusLost,
    Close,
    TakeFocus,
    HitTest,
    IccProfChanged,
    DisplayChanged
}
