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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.CursorUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Input;

[NativeMarshalling(typeof(SafeHandleMarshaller<Cursor>))]
public class Cursor : SafeHandleZeroOrMinusOneIsInvalid
{
    private Cursor(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Cursor(CursorPixelColor[] pixelColors, Size size, Point hotPosition)
        : base(true)
    {
        if (size.Width % 8 != 0)
        {
            throw new InvalidEnumArgumentException(
                "The width of the cursor must be a multiple of 8"
            );
        }

        byte[] data = new byte[pixelColors.Length];
        byte[] mask = new byte[pixelColors.Length];
        for (int i = 0; i < pixelColors.Length; i++)
        {
            switch (pixelColors[i])
            {
                case CursorPixelColor.White:
                    data[i] = 0;
                    mask[i] = 1;
                    break;
                case CursorPixelColor.Black:
                    data[i] = 1;
                    mask[i] = 1;
                    break;
                case CursorPixelColor.Transparent:
                    data[i] = 0;
                    mask[i] = 0;
                    break;
                case CursorPixelColor.Inverted:
                    data[i] = 1;
                    mask[i] = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(pixelColors),
                        pixelColors[i],
                        $"The {nameof(pixelColors)} array must contain only the enums listed in the {nameof(CursorPixelColor)} enum."
                    );
            }
        }

        unsafe
        {
            fixed (byte* dataPtr = data)
            fixed (byte* maskPtr = mask)
            {
                handle = Sdl.CreateCursor(
                    dataPtr,
                    maskPtr,
                    size.Width,
                    size.Height,
                    hotPosition.X,
                    hotPosition.Y
                );
            }
        }

        if (handle == nint.Zero)
        {
            throw new CursorException("Unable to create a new cursor");
        }
    }

    public Cursor(Surface surface, Point hotPosition)
        : base(true)
    {
        handle = Sdl.CreateColorCursor(surface, hotPosition.X, hotPosition.Y);
        if (handle == nint.Zero)
        {
            throw new CursorException("Unable to create a new surface cursor");
        }
    }

    public Cursor(SystemCursor id)
        : base(true)
    {
        handle = Sdl.CreateSystemCursor(id);
        if (handle == nint.Zero)
        {
            throw new CursorException($"Unable to create a new system cursor {id}");
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreeCursor(handle);
        handle = nint.Zero;
        return true;
    }

    public static Cursor? GetActive()
    {
        nint cursorHandle = Sdl.GetCursor();
        if (cursorHandle == nint.Zero)
        {
            throw new CursorException("Unable to get the active cursor");
        }

        return new Cursor(cursorHandle, false);
    }

    public static Cursor? GetDefault()
    {
        nint cursorHandle = Sdl.GetDefaultCursor();
        if (cursorHandle == nint.Zero)
        {
            throw new CursorException("Unable to get the default cursor");
        }

        return new Cursor(cursorHandle, false);
    }

    public static bool IsShown()
    {
        int result = Sdl.ShowCursor(Sdl.Query);
        if (result < 0)
        {
            throw new CursorException("Unable to access cursor visibility state", result);
        }

        return IntBoolMarshaller.ConvertToManaged(result);
    }

    public static void SetIsShown(bool isShown)
    {
        int code = Sdl.ShowCursor(isShown ? Sdl.Enable : Sdl.Disable);
        if (code < 0)
        {
            throw new CursorException(
                $"Unable to set the cursor visibility to {(isShown ? "visible" : "invisible")}"
            );
        }
    }

    public void SetActive()
    {
        Sdl.SetCursor(this);
    }
}
