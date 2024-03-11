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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.EventsManagement;

public enum EventType : uint
{
    FirstEvent = 0x0000,
    Quit = 0x0100,
    AppTerminating,
    AppLowMemory,
    AppWillEnterBackground,
    AppDidEnterBackground,
    AppWillEnterForeground,
    AppDidEnterForeground,
    LocaleChanged,
    DisplayEvent = 0x0150,
    WindowEvent = 0x0200,
    SysWmEvent,
    KeyDown = 0x0300,
    KeyUp,
    TextEditing,
    TextInput,
    KeymapChanged,
    TextEditingExt,
    MouseMotion = 0x0400,
    MouseButtonDown,
    MouseButtonUp,
    MouseWheel,
    JoystickAxisMotion = 0x0600,
    JoystickBallMotion,
    JoystickHatMotion,
    JoystickButtonDown,
    JoystickButtonUp,
    JoystickDeviceAdded,
    JoystickDeviceRemoved,
    JoystickBatteryUpdated,
    GameControllerAxisMotion = 0x0650,
    GameControllerButtonDown,
    GameControllerButtonUp,
    GameControllerDeviceAdded,
    GameControllerDeviceRemoved,
    GameControllerDeviceRemapped,
    GameControllerTouchpadDown,
    GameControllerTouchpadMotion,
    GameControllerTouchpadUp,
    GameControllerSensorUpdate,
    GameControllerUpdateCompleteReservedForSdl3,
    GameControllerSteamHandleUpdated,
    FingerDown = 0x0700,
    FingerUp,
    FingerMotion,
    DollarGesture = 0x0800,
    DollarRecord,
    MultiGesture,
    ClipboardUpdate = 0x0900,
    DropFile = 0x1000,
    DropText,
    DropBegin,
    DropComplete,
    AudioDeviceAdded = 0x1100,
    AudioDeviceRemoved,
    SensorUpdate = 0x1200,
    RenderTargetsReset = 0x2000,
    RenderDeviceReset,
    PollSentinel = 0x7F00,
    UserEvent = 0x8000,
    LastEvent = 0xFFFF
}
