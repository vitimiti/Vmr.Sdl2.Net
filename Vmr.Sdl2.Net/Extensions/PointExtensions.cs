// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
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
                bool isValid = Sdl.EnclosePoints(sdlPointsHandle, points.Length, null, &result);
                if (isValid)
                {
                    return SdlRectangleMarshaller.ConvertToManaged(result);
                }

                errorHandler(Sdl.GetError());
                return Rectangle.Empty;
            }
        }
    }
}
