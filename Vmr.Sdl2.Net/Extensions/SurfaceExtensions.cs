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
