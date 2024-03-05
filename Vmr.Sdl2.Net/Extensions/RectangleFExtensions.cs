// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
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
        unsafe
        {
            var sdlPoints = new SdlPointFMarshaller.SdlPointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                sdlPoints[i] = SdlPointFMarshaller.ConvertToUnmanaged(points[i]);
            }

            fixed (SdlPointFMarshaller.SdlPointF* sdlPointsHandle = sdlPoints)
            {
                SdlRectangleFMarshaller.SdlRectF result = new();
                SdlRectangleFMarshaller.SdlRectF sdlClip =
                    SdlRectangleFMarshaller.ConvertToUnmanaged(clip);

                bool isValid = Sdl.EnclosePointFs(
                    sdlPointsHandle,
                    points.Length,
                    &sdlClip,
                    &result
                );

                if (isValid)
                {
                    return SdlRectangleFMarshaller.ConvertToManaged(result);
                }

                errorHandler(Sdl.GetError());
                return RectangleF.Empty;
            }
        }
    }

    public static (PointF Point1, PointF Point2) IntersectWithLine(
        this RectangleF rectangle,
        (PointF Point1, PointF Point2) line,
        ErrorHandler errorHandler
    )
    {
        unsafe
        {
            SdlRectangleFMarshaller.SdlRectF sdlRect = SdlRectangleFMarshaller.ConvertToUnmanaged(
                rectangle
            );

            float x1 = line.Point1.X;
            float y1 = line.Point1.Y;
            float x2 = line.Point2.X;
            float y2 = line.Point2.Y;
            bool isValid = Sdl.IntersectRectangleFAndLine(&sdlRect, ref x1, ref y1, ref x2, ref y2);
            if (isValid)
            {
                return (new PointF(x1, y1), new PointF(x2, y2));
            }

            errorHandler(Sdl.GetError());
            return (PointF.Empty, PointF.Empty);
        }
    }
}
