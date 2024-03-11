// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

using System.Drawing;

using Vmr.Sdl2.Net.Imports;

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

    public static RectangleF EncloseInRectangleF(this PointF[] points)
    {
        RectangleF result = new();
        bool isValid = Sdl.EnclosePointFs(points, points.Length, RectangleF.Empty, ref result);
        return isValid ? result : RectangleF.Empty;
    }
}
