// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Extensions;

public static class SurfaceExtensions
{
    public static byte[] Convert(
        this byte[] src,
        Size size,
        uint srcFormat,
        int srcPitch,
        uint dstFormat,
        byte[] dst,
        int dstPitch
    )
    {
        unsafe
        {
            fixed (byte* srcHandle = src)
            fixed (byte* dstHandle = dst)
            {
                int code = Sdl.ConvertPixels(
                    size.Width,
                    size.Height,
                    srcFormat,
                    srcHandle,
                    srcPitch,
                    dstFormat,
                    dstHandle,
                    dstPitch
                );

                if (code < 0)
                {
                    throw new SurfaceException("Unable to convert the given data", code);
                }

                byte[] dstResult = new byte[dst.Length];
                for (int i = 0; i < dst.Length; i++)
                {
                    dstResult[i] = dstHandle[i];
                }

                return dstResult;
            }
        }
    }

    public static byte[] PremultiplyAlpha(
        this byte[] src,
        Size size,
        uint srcFormat,
        int srcPitch,
        uint dstFormat,
        byte[] dst,
        int dstPitch
    )
    {
        unsafe
        {
            fixed (byte* srcHandle = src)
            fixed (byte* dstHandle = dst)
            {
                int code = Sdl.PremultiplyAlpha(
                    size.Width,
                    size.Height,
                    srcFormat,
                    srcHandle,
                    srcPitch,
                    dstFormat,
                    dstHandle,
                    dstPitch
                );

                if (code < 0)
                {
                    throw new SurfaceException("Unable to convert the given data", code);
                }

                byte[] dstResult = new byte[dst.Length];
                for (int i = 0; i < dst.Length; i++)
                {
                    dstResult[i] = dstHandle[i];
                }

                return dstResult;
            }
        }
    }
}
