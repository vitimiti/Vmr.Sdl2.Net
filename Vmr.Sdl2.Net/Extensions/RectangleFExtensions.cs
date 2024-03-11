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

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Extensions;

public static class RectangleFExtensions
{
    public static RectangleF EnclosePoints(this RectangleF clip, PointF[] points)
    {
        RectangleF result = new();
        bool isValid = Sdl.EnclosePointFs(points, points.Length, clip, ref result);
        return isValid ? result : RectangleF.Empty;
    }

    public static (PointF Point1, PointF Point2) IntersectWithLine(
        this RectangleF rectangle,
        (PointF Point1, PointF Point2) line
    )
    {
        float x1 = line.Point1.X;
        float y1 = line.Point1.Y;
        float x2 = line.Point2.X;
        float y2 = line.Point2.Y;
        bool isValid = Sdl.IntersectRectangleFAndLine(rectangle, ref x1, ref y1, ref x2, ref y2);
        return isValid ? (new PointF(x1, y1), new PointF(x2, y2)) : (PointF.Empty, PointF.Empty);
    }
}
