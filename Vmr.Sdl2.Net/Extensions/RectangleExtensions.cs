// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
