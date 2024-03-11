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

using Vmr.Sdl2.Net.Input.MouseUtilities;

namespace Vmr.Sdl2.Net.EventsManagement;

public class MouseWheelEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint windowId,
    uint mouseInstanceId,
    ScrollAmount scrollAmount,
    MouseWheelDirection direction,
    PreciseScrollAmount preciseScrollAmount,
    Point position
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint WindowId { get; private set; } = windowId;
    public uint MouseInstanceId { get; private set; } = mouseInstanceId;
    public ScrollAmount ScrollAmount { get; private set; } = scrollAmount;
    public MouseWheelDirection Direction { get; private set; } = direction;
    public PreciseScrollAmount PreciseScrollAmount { get; private set; } = preciseScrollAmount;
    public Point Position { get; private set; } = position;
}
