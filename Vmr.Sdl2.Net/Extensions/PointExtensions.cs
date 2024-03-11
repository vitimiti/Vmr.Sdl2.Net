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

using System.Drawing;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Extensions;

public static class PointExtensions
{
    public static bool IsInRectangle(this Point point, Rectangle rectangle)
    {
        return point.X >= rectangle.X
               && point.X < point.X + rectangle.Width
               && point.Y >= rectangle.Y
               && point.Y < point.Y + rectangle.Height;
    }

    public static Rectangle EncloseInRectangle(this Point[] points)
    {
        Rectangle result = new();
        bool isValid = Sdl.EnclosePoints(points, points.Length, Rectangle.Empty, ref result);
        return isValid ? result : Rectangle.Empty;
    }

    public static int GetDisplayIndex(this Point point)
    {
        int result = Sdl.GetPointDisplayIndex(point);
        if (result < 0)
        {
            throw new DisplayException(
                $"Unable to get the display index for the point {point}",
                result
            );
        }

        return result;
    }
}
