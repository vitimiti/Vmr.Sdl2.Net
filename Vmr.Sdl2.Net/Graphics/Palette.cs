// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

public class Palette : SafeHandleZeroOrMinusOneIsInvalid
{
    public Color[]? Colors
    {
        get
        {
            unsafe
            {
                if (((SdlPaletteMarshaller.SdlPalette*)handle)->Colors is null)
                {
                    return null;
                }

                if (((SdlPaletteMarshaller.SdlPalette*)handle)->NColors == 0)
                {
                    return Array.Empty<Color>();
                }

                var colors = new Color[((SdlPaletteMarshaller.SdlPalette*)handle)->NColors];
                for (int i = 0; i < ((SdlPaletteMarshaller.SdlPalette*)handle)->NColors; i++)
                {
                    colors[i] = SdlColorMarshaller.ConvertToManaged(
                        ((SdlPaletteMarshaller.SdlPalette*)handle)->Colors[i]
                    );
                }

                return colors;
            }
        }
    }

    public uint Version
    {
        get
        {
            unsafe
            {
                return ((SdlPaletteMarshaller.SdlPalette*)handle)->Version;
            }
        }
    }

    public int RefCount
    {
        get
        {
            unsafe
            {
                return ((SdlPaletteMarshaller.SdlPalette*)handle)->RefCount;
            }
        }
    }

    internal Palette(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Palette(int numberOfColors, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.AllocPalette(numberOfColors);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.FreePalette(handle);
        handle = nint.Zero;
        return true;
    }

    public void SetColors(
        Color[]? colors,
        int firstColor,
        int numberOfColors,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            SdlColorMarshaller.SdlColor[]? sdlColors = null;
            if (colors is not null)
            {
                sdlColors = new SdlColorMarshaller.SdlColor[colors.Length];
                for (int i = 0; i < colors.Length; i++)
                {
                    sdlColors[i] = SdlColorMarshaller.ConvertToUnmanaged(colors[i]);
                }
            }

            fixed (SdlColorMarshaller.SdlColor* colorsHandle = sdlColors)
            {
                int code = Sdl.SetPaletteColors(handle, colorsHandle, firstColor, numberOfColors);
                if (code < 0)
                {
                    errorHandler(Sdl.GetError(), code);
                }
            }
        }
    }
}
