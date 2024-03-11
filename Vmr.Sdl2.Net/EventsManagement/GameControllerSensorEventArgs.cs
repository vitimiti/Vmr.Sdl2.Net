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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using Vmr.Sdl2.Net.Input.SensorUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class GameControllerSensorEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long joystickInstanceId,
    SensorType sensor,
    float[] data,
    TimeSpan hardwareTimeStamp
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long JoystickInstanceId { get; private set; } = joystickInstanceId;
    public SensorType Sensor { get; private set; } = sensor;
    public float[] Data { get; private set; } = data;
    public TimeSpan HardwareTimeStamp { get; private set; } = hardwareTimeStamp;
}
