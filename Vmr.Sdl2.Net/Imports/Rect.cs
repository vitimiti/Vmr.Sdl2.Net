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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_EnclosePoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool EnclosePoints(
        [MarshalUsing(
            typeof(ArrayMarshaller<Point, PointMarshaller.Point>),
            CountElementName = nameof(count)
        )]
        Point[] points,
        int count,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle clip,
        [MarshalUsing(typeof(RectangleMarshaller))]
        ref Rectangle result
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IntersectRectangleAndLine(
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle rect,
        ref int x1,
        ref int y1,
        ref int x2,
        ref int y2
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_EncloseFPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool EnclosePointFs(
        [MarshalUsing(CountElementName = nameof(count))]
        PointF[] points,
        int count,
        [MarshalUsing(typeof(RectangleFMarshaller))]
        RectangleF clip,
        [MarshalUsing(typeof(RectangleFMarshaller))]
        ref RectangleF result
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_IntersectFRectAndLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IntersectRectangleFAndLine(
        [MarshalUsing(typeof(RectangleFMarshaller))]
        RectangleF rect,
        ref float x1,
        ref float y1,
        ref float x2,
        ref float y2
    );
}
