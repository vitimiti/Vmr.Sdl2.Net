// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net;

[Flags]
public enum ApplicationSubsystems : uint
{
    None = 0,
    Timer = 0x00000001U,
    Audio = 0x00000010U,
    Video = 0x00000020U,
    Joystick = 0x00000200U,
    Haptic = 0x00001000U,
    GameController = 0x00002000U,
    Events = 0x00004000U,
    Sensor = 0x00008000U,
    Everything = Timer | Audio | Video | Events | Joystick | Haptic | GameController | Sensor
}
