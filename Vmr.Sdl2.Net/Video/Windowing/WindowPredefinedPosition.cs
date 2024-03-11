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

namespace Vmr.Sdl2.Net.Video.Windowing;

public readonly struct WindowPredefinedPosition
{
    public const uint UndefinedMask = 0x1FFF0000U;
    public const uint CenteredMask = 0x2FFF0000u;

    public static readonly int Undefined = UndefinedDisplay(0);
    public static readonly int Centered = CenteredDisplay(0);

    public static int UndefinedDisplay(int displayIndex)
    {
        return (int)(UndefinedMask | displayIndex);
    }

    public static int CenteredDisplay(int displayIndex)
    {
        return (int)(CenteredMask | displayIndex);
    }

    public static bool IsUndefined(int position)
    {
        return (position & 0xFFFF0000) == UndefinedMask;
    }

    public static bool IsCentered(int position)
    {
        return (position & 0xFFFF0000) == CenteredMask;
    }
}
