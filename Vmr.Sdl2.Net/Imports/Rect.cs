// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool EnclosePoints(
        [MarshalUsing(
            typeof(ArrayMarshaller<Point, SdlPointMarshaller.SdlPoint>),
            CountElementName = nameof(count)
        )]
            Point[] points,
        int count,
        [MarshalUsing(typeof(SdlRectangleMarshaller))] Rectangle clip,
        [MarshalUsing(typeof(SdlRectangleMarshaller))] ref Rectangle result
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool IntersectRectangleAndLine(
        [MarshalUsing(typeof(SdlRectangleMarshaller))] Rectangle rect,
        ref int x1,
        ref int y1,
        ref int x2,
        ref int y2
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_EncloseFPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool EnclosePointFs(
        [MarshalUsing(CountElementName = nameof(count))] PointF[] points,
        int count,
        [MarshalUsing(typeof(SdlRectangleFMarshaller))] RectangleF clip,
        [MarshalUsing(typeof(SdlRectangleFMarshaller))] ref RectangleF result
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_IntersectFRectAndLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool IntersectRectangleFAndLine(
        [MarshalUsing(typeof(SdlRectangleFMarshaller))] RectangleF rect,
        ref float x1,
        ref float y1,
        ref float x2,
        ref float y2
    );
}
