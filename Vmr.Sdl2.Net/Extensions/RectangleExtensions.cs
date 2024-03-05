// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
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
        unsafe
        {
            var sdlPoints = new SdlPointMarshaller.SdlPoint[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                sdlPoints[i] = SdlPointMarshaller.ConvertToUnmanaged(points[i]);
            }

            fixed (SdlPointMarshaller.SdlPoint* sdlPointsHandle = sdlPoints)
            {
                SdlRectangleMarshaller.SdlRect result = new();
                SdlRectangleMarshaller.SdlRect sdlClip = SdlRectangleMarshaller.ConvertToUnmanaged(
                    clip
                );

                bool isValid = Sdl.EnclosePoints(sdlPointsHandle, points.Length, &sdlClip, &result);
                if (isValid)
                {
                    return SdlRectangleMarshaller.ConvertToManaged(result);
                }

                errorHandler(Sdl.GetError());
                return Rectangle.Empty;
            }
        }
    }

    public static (Point Point1, Point Point2) IntersectWithLine(
        this Rectangle rectangle,
        (Point Point1, Point Point2) line,
        ErrorHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleMarshaller.SdlRect sdlRect = SdlRectangleMarshaller.ConvertToUnmanaged(
                rectangle
            );

            int x1 = line.Point1.X;
            int y1 = line.Point1.Y;
            int x2 = line.Point2.X;
            int y2 = line.Point2.Y;
            bool isValid = Sdl.IntersectRectangleAndLine(&sdlRect, ref x1, ref y1, ref x2, ref y2);
            if (isValid)
            {
                return (new Point(x1, y1), new Point(x2, y2));
            }

            errorHandler(Sdl.GetError());
            return (Point.Empty, Point.Empty);
        }
    }
}
