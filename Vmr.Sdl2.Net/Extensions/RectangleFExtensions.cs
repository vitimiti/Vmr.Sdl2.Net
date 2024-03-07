// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Extensions;

public static class RectangleFExtensions
{
    public static RectangleF EnclosePoints(
        this RectangleF clip,
        PointF[] points,
        ErrorHandler errorHandler
    )
    {
        RectangleF result = new();
        bool isValid = Sdl.EnclosePointFs(points, points.Length, clip, ref result);
        if (isValid)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return RectangleF.Empty;
    }

    public static (PointF Point1, PointF Point2) IntersectWithLine(
        this RectangleF rectangle,
        (PointF Point1, PointF Point2) line,
        ErrorHandler errorHandler
    )
    {
        float x1 = line.Point1.X;
        float y1 = line.Point1.Y;
        float x2 = line.Point2.X;
        float y2 = line.Point2.Y;
        bool isValid = Sdl.IntersectRectangleFAndLine(rectangle, ref x1, ref y1, ref x2, ref y2);
        if (isValid)
        {
            return (new PointF(x1, y1), new PointF(x2, y2));
        }

        errorHandler(Sdl.GetError());
        return (PointF.Empty, PointF.Empty);
    }
}
