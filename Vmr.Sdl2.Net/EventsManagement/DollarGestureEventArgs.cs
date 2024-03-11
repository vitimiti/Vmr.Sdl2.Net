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

using System.Drawing;

namespace Vmr.Sdl2.Net.EventsManagement;

public class DollarGestureEventArgs(
    EventType type,
    TimeSpan timeStamp,
    long touchDeviceId,
    long gestureId,
    uint numberOfFingers,
    float error,
    PointF normalizedCenterOfGesture
)
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public long TouchDeviceId { get; private set; } = touchDeviceId;
    public long GestureId { get; private set; } = gestureId;
    public uint NumberOfFingers { get; private set; } = numberOfFingers;
    public float Error { get; private set; } = error;
    public PointF NormalizedCenterOfGesture { get; private set; } = normalizedCenterOfGesture;
}
