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
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Graphics.Colors;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Graphics.Pixels;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<PixelFormat>))]
public class PixelFormat : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<PixelFormat>
{
    internal PixelFormat(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public PixelFormat(uint pixelFormat)
        : base(true)
    {
        handle = Sdl.AllocFormat(pixelFormat);
        if (handle == nint.Zero)
        {
            throw new PixelFormatException(
                $"Unable to create a pixel format with the value {pixelFormat:X8}"
            );
        }
    }

    private unsafe Sdl.PixelFormat* UnsafeHandle => (Sdl.PixelFormat*)handle;

    public uint Format
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Format;
            }
        }
    }

    public Palette? Palette
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Palette is null
                    ? null
                    : new Palette((nint)UnsafeHandle->Palette, false);
            }
        }
    }

    public byte BytesPerPixel
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->BytesPerPixel;
            }
        }
    }

    public ColorMasks ColorMasks
    {
        get
        {
            unsafe
            {
                return new ColorMasks
                {
                    BitsPerPixel = UnsafeHandle->BitsPerPixel,
                    Red = UnsafeHandle->RMask,
                    Green = UnsafeHandle->GMask,
                    Blue = UnsafeHandle->BMask,
                    Alpha = UnsafeHandle->AMask
                };
            }
        }
    }

    public ColorLoss ColorLoss
    {
        get
        {
            unsafe
            {
                return new ColorLoss
                {
                    Red = UnsafeHandle->RLoss,
                    Green = UnsafeHandle->GLoss,
                    Blue = UnsafeHandle->BLoss,
                    Alpha = UnsafeHandle->ALoss
                };
            }
        }
    }

    public ColorShift ColorShift
    {
        get
        {
            unsafe
            {
                return new ColorShift
                {
                    Red = UnsafeHandle->RShift,
                    Green = UnsafeHandle->GShift,
                    Blue = UnsafeHandle->BShift,
                    Alpha = UnsafeHandle->AShift
                };
            }
        }
    }

    public int ReferenceCount
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->RefCount;
            }
        }
    }

    public PixelFormat? Next
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Next is null
                    ? null
                    : new PixelFormat((nint)UnsafeHandle->Next, false);
            }
        }
    }

    public bool Equals(PixelFormat? other)
    {
        return other is not null
               && Format == other.Format
               && Palette == other.Palette
               && BytesPerPixel == other.BytesPerPixel
               && ColorMasks == other.ColorMasks
               && ColorLoss == other.ColorLoss
               && ColorShift == other.ColorShift;
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreeFormat(handle);
        handle = nint.Zero;
        return true;
    }

    public static PixelFormat CreateAsUnknown()
    {
        return new PixelFormat(0);
    }

    public static PixelFormat CreateAsIndex1Lsb()
    {
        return new PixelFormat(Define(PixelType.Index1, BitmapOrder.LeastSignificantBit, 1, 0));
    }

    public static PixelFormat CreateAsIndex1Msb()
    {
        return new PixelFormat(Define(PixelType.Index1, BitmapOrder.MostSignificantBit, 1, 0));
    }

    public static PixelFormat CreateAsIndex2Lsb()
    {
        return new PixelFormat(Define(PixelType.Index2, BitmapOrder.LeastSignificantBit, 2, 0));
    }

    public static PixelFormat CreateAsIndex2Msb()
    {
        return new PixelFormat(Define(PixelType.Index2, BitmapOrder.MostSignificantBit, 2, 0));
    }

    public static PixelFormat CreateAsIndex4Lsb()
    {
        return new PixelFormat(Define(PixelType.Index4, BitmapOrder.LeastSignificantBit, 4, 0));
    }

    public static PixelFormat CreateAsIndex4Msb()
    {
        return new PixelFormat(Define(PixelType.Index4, BitmapOrder.MostSignificantBit, 4, 0));
    }

    public static PixelFormat CreateAsIndex8()
    {
        return new PixelFormat(Define(PixelType.Index8, 8, 1));
    }

    public static PixelFormat CreateAsRgb332()
    {
        return new PixelFormat(
            Define(PixelType.Packed8, PackedOrder.Xrgb, PackedLayout.L332, 8, 1)
        );
    }

    public static PixelFormat CreateAsXrgb4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xrgb, PackedLayout.L4444, 12, 2)
        );
    }

    public static PixelFormat CreateAsRgb444()
    {
        return CreateAsXrgb4444();
    }

    public static PixelFormat CreateAsXbgr4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xbgr, PackedLayout.L4444, 12, 2)
        );
    }

    public static PixelFormat CreateAsBgr444()
    {
        return CreateAsXbgr4444();
    }

    public static PixelFormat CreateAsXrgb1555()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xrgb, PackedLayout.L1555, 15, 2)
        );
    }

    public static PixelFormat CreateAsRgb555()
    {
        return CreateAsXrgb1555();
    }

    public static PixelFormat CreateAsXbgr1555()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xbgr, PackedLayout.L1555, 15, 2)
        );
    }

    public static PixelFormat CreateAsBgr555()
    {
        return CreateAsXbgr1555();
    }

    public static PixelFormat CreateAsArgb4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Argb, PackedLayout.L4444, 16, 2)
        );
    }

    public static PixelFormat CreateAsRgba4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Rgba, PackedLayout.L4444, 16, 2)
        );
    }

    public static PixelFormat CreateAsAbgr4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Abgr, PackedLayout.L4444, 16, 2)
        );
    }

    public static PixelFormat CreateAsBgra4444()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Bgra, PackedLayout.L4444, 16, 2)
        );
    }

    public static PixelFormat CreateAsArgb1555()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Argb, PackedLayout.L1555, 16, 2)
        );
    }

    public static PixelFormat CreateAsRgba5551()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Rgba, PackedLayout.L5551, 16, 2)
        );
    }

    public static PixelFormat CreateAsAbgr1555()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Abgr, PackedLayout.L1555, 16, 2)
        );
    }

    public static PixelFormat CreateAsBgra5551()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Bgra, PackedLayout.L5551, 16, 2)
        );
    }

    public static PixelFormat CreateAsRgb565()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xrgb, PackedLayout.L565, 16, 2)
        );
    }

    public static PixelFormat CreateAsBgr565()
    {
        return new PixelFormat(
            Define(PixelType.Packed16, PackedOrder.Xbgr, PackedLayout.L565, 16, 2)
        );
    }

    public static PixelFormat CreateAsRgb24()
    {
        return new PixelFormat(Define(PixelType.ArrayU8, ArrayOrder.Rgb, 24, 3));
    }

    public static PixelFormat CreateAsBgr24()
    {
        return new PixelFormat(Define(PixelType.ArrayU8, ArrayOrder.Bgr, 24, 3));
    }

    public static PixelFormat CreateAsXrgb8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Xrgb, PackedLayout.L8888, 24, 4)
        );
    }

    public static PixelFormat CreateAsRgb888()
    {
        return CreateAsXrgb8888();
    }

    public static PixelFormat CreateAsRgbx8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Rgbx, PackedLayout.L8888, 24, 4)
        );
    }

    public static PixelFormat CreateAsXbgr8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Xbgr, PackedLayout.L8888, 24, 4)
        );
    }

    public static PixelFormat CreateAsBgr888()
    {
        return CreateAsXbgr8888();
    }

    public static PixelFormat CreateAsBgrx8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Bgrx, PackedLayout.L8888, 24, 4)
        );
    }

    public static PixelFormat CreateAsArgb8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Argb, PackedLayout.L8888, 32, 4)
        );
    }

    public static PixelFormat CreateAsRgba8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Rgba, PackedLayout.L8888, 32, 4)
        );
    }

    public static PixelFormat CreateAsAbgr8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Abgr, PackedLayout.L8888, 32, 4)
        );
    }

    public static PixelFormat CreateAsBgra8888()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Bgra, PackedLayout.L8888, 32, 4)
        );
    }

    public static PixelFormat CreateAsArgb2101010()
    {
        return new PixelFormat(
            Define(PixelType.Packed32, PackedOrder.Argb, PackedLayout.L2101010, 32, 4)
        );
    }

    public static PixelFormat CreateAsRgba32()
    {
        return BitConverter.IsLittleEndian ? CreateAsAbgr8888() : CreateAsRgba8888();
    }

    public static PixelFormat CreateAsArgb32()
    {
        return BitConverter.IsLittleEndian ? CreateAsBgra8888() : CreateAsArgb8888();
    }

    public static PixelFormat CreateAsBgra32()
    {
        return BitConverter.IsLittleEndian ? CreateAsArgb8888() : CreateAsBgra8888();
    }

    public static PixelFormat CreateAsAbgr32()
    {
        return BitConverter.IsLittleEndian ? CreateAsRgba8888() : CreateAsAbgr8888();
    }

    public static PixelFormat CreateAsRgbx32()
    {
        return BitConverter.IsLittleEndian ? CreateAsXbgr8888() : CreateAsRgbx8888();
    }

    public static PixelFormat CreateAsXrgb32()
    {
        return BitConverter.IsLittleEndian ? CreateAsBgrx8888() : CreateAsXrgb8888();
    }

    public static PixelFormat CreateAsBgrx32()
    {
        return BitConverter.IsLittleEndian ? CreateAsXrgb8888() : CreateAsBgrx8888();
    }

    public static PixelFormat CreateAsXbgr32()
    {
        return BitConverter.IsLittleEndian ? CreateAsRgbx8888() : CreateAsXbgr8888();
    }

    public static PixelFormat CreateAsYv12()
    {
        return new PixelFormat(Define('Y', 'V', '1', '2'));
    }

    public static PixelFormat CreateAsIyuv()
    {
        return new PixelFormat(Define('I', 'Y', 'U', 'V'));
    }

    public static PixelFormat CreateAsYuy2()
    {
        return new PixelFormat(Define('Y', 'U', 'Y', '2'));
    }

    public static PixelFormat CreateAsUyvy()
    {
        return new PixelFormat(Define('U', 'Y', 'V', 'Y'));
    }

    public static PixelFormat CreateAsYvyu()
    {
        return new PixelFormat(Define('Y', 'V', 'Y', 'U'));
    }

    public static PixelFormat CreateAsNv12()
    {
        return new PixelFormat(Define('N', 'V', '1', '2'));
    }

    public static PixelFormat CreateAsNv21()
    {
        return new PixelFormat(Define('N', 'V', '2', '1'));
    }

    public static PixelFormat CreateAsExternalOes()
    {
        return new PixelFormat(Define('O', 'E', 'S', ' '));
    }

    private static uint Define(uint type, uint order, uint layout, byte bits, byte bytes)
    {
        return (1U << 28)
               | (type << 24)
               | (order << 20)
               | (layout << 16)
               | ((uint)bits << 8)
               | ((uint)bytes << 0);
    }

    public static uint Define(char a, char b, char c, char d)
    {
        return (uint)(((byte)a << 0) | ((byte)b << 8) | ((byte)c << 16) | ((byte)d << 24));
    }

    public static uint Define(PixelType type, byte bits, byte bytes)
    {
        return Define((uint)type, 0U, 0U, bits, bytes);
    }

    public static uint Define(PixelType type, PackedLayout layout, byte bits, byte bytes)
    {
        return Define((uint)type, 0U, (uint)layout, bits, bytes);
    }

    public static uint Define(PixelType type, ArrayOrder order, byte bits, byte bytes)
    {
        return Define((uint)type, (uint)order, 0U, bits, bytes);
    }

    public static uint Define(PixelType type, BitmapOrder order, byte bits, byte bytes)
    {
        return Define((uint)type, (uint)order, 0U, bits, bytes);
    }

    public static uint Define(PixelType type, PackedOrder order, byte bits, byte bytes)
    {
        return Define((uint)type, (uint)order, 0U, bits, bytes);
    }

    public static uint Define(
        PixelType type,
        ArrayOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        return Define((uint)type, (uint)order, (uint)layout, bits, bytes);
    }

    public static uint Define(
        PixelType type,
        BitmapOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        return Define((uint)type, (uint)order, (uint)layout, bits, bytes);
    }

    public static uint Define(
        PixelType type,
        PackedOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        return Define((uint)type, (uint)order, (uint)layout, bits, bytes);
    }

    public void SetPalette(Palette? palette)
    {
        int code = palette is null
            ? Sdl.SetPixelFormatPalette(handle, nint.Zero)
            : Sdl.SetPixelFormatPalette(handle, palette);

        if (code < 0)
        {
            throw new PixelFormatException(
                $"Unable to set the pixel format palette to: {palette}",
                code
            );
        }
    }

    public uint MapRgb(Color color)
    {
        return Sdl.MapRgb(this, color.R, color.G, color.B);
    }

    public uint MapRba(Color color)
    {
        return Sdl.MapRgba(this, color.R, color.G, color.B, color.A);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((PixelFormat)obj);
    }

    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Format);
        code.Add(Palette);
        code.Add(BytesPerPixel);
        code.Add(ColorMasks);
        code.Add(ColorLoss);
        code.Add(ColorShift);
        code.Add(ReferenceCount);
        code.Add(Next);
        return code.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Format: {Format}, Palette: {Palette}, Bytes Per Pixel: {BytesPerPixel}, Color Masks: {ColorMasks}, Color Loss: {ColorLoss}, Color Shift: {ColorShift}, Reference Count: {ReferenceCount}, Next: {Next}}}";
    }

    public static bool operator ==(PixelFormat? left, PixelFormat? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PixelFormat? left, PixelFormat? right)
    {
        return !Equals(left, right);
    }
}
