// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Extensions;

public static class RectangleExtensions
{
    public static Rectangle EnclosePoints(
        this Rectangle clip,
        Point[] points,
        ErrorHandler errorHandler
    )
    {
        Rectangle result = new();
        bool isValid = Sdl.EnclosePoints(points, points.Length, clip, ref result);
        if (isValid)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return Rectangle.Empty;
    }

    public static (Point Point1, Point Point2) IntersectWithLine(
        this Rectangle rectangle,
        (Point Point1, Point Point2) line,
        ErrorHandler errorHandler
    )
    {
        int x1 = line.Point1.X;
        int y1 = line.Point1.Y;
        int x2 = line.Point2.X;
        int y2 = line.Point2.Y;
        bool isValid = Sdl.IntersectRectangleAndLine(rectangle, ref x1, ref y1, ref x2, ref y2);
        if (isValid)
        {
            return (new Point(x1, y1), new Point(x2, y2));
        }

        errorHandler(Sdl.GetError());
        return (Point.Empty, Point.Empty);
    }

    public static int GetDisplayIndex(this Rectangle rectangle, ErrorCodeHandler errorHandler)
    {
        int result = Sdl.GetRectDisplayIndex(rectangle);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return result;
    }
}
