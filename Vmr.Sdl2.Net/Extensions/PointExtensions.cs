// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
