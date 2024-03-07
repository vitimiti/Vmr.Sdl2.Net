// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Extensions;

public static class PointFExtensions
{
    public static bool IsInRectangleF(this PointF point, RectangleF rectangle)
    {
        return point.X >= rectangle.X
            && point.X < point.X + rectangle.Width
            && point.Y >= rectangle.Y
            && point.Y < point.Y + rectangle.Height;
    }

    public static RectangleF EncloseInRectangleF(this PointF[] points, ErrorHandler errorHandler)
    {
        RectangleF result = new();
        bool isValid = Sdl.EnclosePointFs(points, points.Length, RectangleF.Empty, ref result);

        if (isValid)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return RectangleF.Empty;
    }
}
