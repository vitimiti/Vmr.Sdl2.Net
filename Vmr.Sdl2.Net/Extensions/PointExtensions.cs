// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

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

    public static Rectangle EncloseInRectangle(this Point[] points, ErrorHandler errorHandler)
    {
        Rectangle result = new();
        bool isValid = Sdl.EnclosePoints(points, points.Length, Rectangle.Empty, ref result);

        if (isValid)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return Rectangle.Empty;
    }

    public static int GetDisplayIndex(this Point point, ErrorCodeHandler errorHandler)
    {
        int result = Sdl.GetPointDisplayIndex(point);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return result;
    }
}
