// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Video.Windowing;

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
