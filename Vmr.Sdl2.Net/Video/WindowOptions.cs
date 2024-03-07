// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video;

[Flags]
public enum WindowOptions : uint
{
    None = 0x00000000U,
    FullScreen = 0x00000001U,
    OpenGl = 0x00000002U,
    Shown = 0x00000004U,
    Hidden = 0x00000008U,
    Borderless = 0x00000010U,
    Resizable = 0x00000020U,
    Minimized = 0x00000040U,
    Maximized = 0x00000080U,
    MouseGrabbed = 0x00000100U,
    InputFocus = 0x00000200U,
    MouseFocus = 0x00000400U,
    FullScreenDesktop = FullScreen | 0x00001000U,
    Foreign = 0x00000800U,
    AllowHighDpi = 0x00002000U,
    MouseCapture = 0x00004000U,
    AlwaysOnTop = 0x00008000U,
    SkipTaskbar = 0x00010000U,
    Utility = 0x00020000U,
    Tooltip = 0x00040000U,
    PopupMenu = 0x00080000U,
    KeyboardGrabbed = 0x00100000U,
    Vulkan = 0x10000000U,
    Metal = 0x20000000U,
    InputGrabbed = MouseGrabbed
}
