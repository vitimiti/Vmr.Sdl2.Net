// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Graphics.Pixels;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Extensions;

public static class PixelExtensions
{
    public static byte PixelFlag(this uint format)
    {
        return (byte)((format >> 28) & 0x0F);
    }

    public static byte PixelType(this uint format)
    {
        return (byte)((format >> 24) & 0x0F);
    }

    public static byte PixelOrder(this uint format)
    {
        return (byte)((format >> 20) & 0x0F);
    }

    public static byte PixelLayout(this uint format)
    {
        return (byte)((format >> 16) & 0x0F);
    }

    public static byte BitsPerPixel(this uint format)
    {
        return (byte)((format >> 8) & 0xFF);
    }

    public static string? GetPixelFormatName(this uint format)
    {
        return Sdl.GetPixelFormatName(format);
    }

    public static ColorMasks PixelFormatToMasks(this uint format, ErrorHandler errorHandler)
    {
        bool isValid = Sdl.PixelFormatEnumToMasks(
            format,
            out int bpp,
            out uint rMask,
            out uint gMask,
            out uint bMask,
            out uint aMask
        );

        if (!isValid)
        {
            errorHandler(Sdl.GetError());
        }

        return new ColorMasks
        {
            BitsPerPixel = bpp,
            Red = rMask,
            Green = gMask,
            Blue = bMask,
            Alpha = aMask
        };
    }

    public static Color GetRgb(this uint pixel, PixelFormat format)
    {
        Sdl.GetRgb(pixel, format, out byte r, out byte g, out byte b);
        return Color.FromArgb(r, g, b);
    }

    public static Color GetRgba(this uint pixel, PixelFormat format)
    {
        Sdl.GetRgba(pixel, format, out byte r, out byte g, out byte b, out byte a);
        return Color.FromArgb(a, r, g, b);
    }

    public static ushort[] CalculateGammaRamp(this float gamma)
    {
        ushort[] ramp = new ushort[256];
        Sdl.CalculateGammaRamp(float.Clamp(gamma, 0F, 1F), ramp);
        return ramp;
    }
}
