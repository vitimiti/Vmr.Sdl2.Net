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

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerTouchpadEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    int touchpadIndex,
    int fingerIndex,
    PointF normalizedPosition,
    float pressure
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public int TouchpadIndex { get; private set; } = touchpadIndex;
    public int FingerIndex { get; private set; } = fingerIndex;
    public PointF NormalizedPosition { get; private set; } = normalizedPosition;
    public float Pressure { get; private set; } = pressure;
}
