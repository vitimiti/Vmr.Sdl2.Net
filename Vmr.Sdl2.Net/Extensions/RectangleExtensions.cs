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

public static class RectangleExtensions
{
    public static Rectangle EnclosePoints(this Rectangle clip, Point[] points)
    {
        Rectangle result = new();
        bool isValid = Sdl.EnclosePoints(points, points.Length, clip, ref result);
        return isValid ? result : Rectangle.Empty;
    }

    public static (Point Point1, Point Point2) IntersectWithLine(
        this Rectangle rectangle,
        (Point Point1, Point Point2) line
    )
    {
        int x1 = line.Point1.X;
        int y1 = line.Point1.Y;
        int x2 = line.Point2.X;
        int y2 = line.Point2.Y;
        bool isValid = Sdl.IntersectRectangleAndLine(rectangle, ref x1, ref y1, ref x2, ref y2);
        return isValid ? (new Point(x1, y1), new Point(x2, y2)) : (Point.Empty, Point.Empty);
    }

    public static int GetDisplayIndex(this Rectangle rectangle)
    {
        int result = Sdl.GetRectDisplayIndex(rectangle);
        if (result < 0)
        {
            throw new DisplayException(
                $"Unable to get the display index for the rectangle {rectangle}",
                result
            );
        }

        return result;
    }
}
