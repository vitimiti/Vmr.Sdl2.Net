// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

[Flags]
public enum KeyModifiers : ushort
{
    None = 0x0000,
    LeftShift = 0x0001,
    RightShift = 0x0002,
    LeftControl = 0x0040,
    RightControl = 0x0080,
    LeftAlt = 0x0100,
    RightAlt = 0x0200,
    LeftGui = 0x0400,
    RightGui = 0x0800,
    Number = 0x1000,
    Caps = 0x2000,
    Mode = 0x4000,
    Scroll = 0x8000,
    Control = LeftControl | RightControl,
    Shift = LeftShift | RightShift,
    Alt = LeftAlt | RightAlt,
    Gui = LeftGui | RightGui
}