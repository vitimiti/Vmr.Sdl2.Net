// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Graphics;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Palette>))]
public class Palette : SafeHandleZeroOrMinusOneIsInvalid
{
    public Color[]? Colors
    {
        get
        {
            unsafe
            {
                if (((Sdl.Palette*)handle)->Colors is null)
                {
                    return null;
                }

                if (((Sdl.Palette*)handle)->NColors == 0)
                {
                    return Array.Empty<Color>();
                }

                var colors = new Color[((Sdl.Palette*)handle)->NColors];
                for (int i = 0; i < ((Sdl.Palette*)handle)->NColors; i++)
                {
                    colors[i] = SdlColorMarshaller.ConvertToManaged(
                        ((Sdl.Palette*)handle)->Colors[i]
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
                return ((Sdl.Palette*)handle)->Version;
            }
        }
    }

    public int ReferenceCount
    {
        get
        {
            unsafe
            {
                return ((Sdl.Palette*)handle)->RefCount;
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

    public override string ToString()
    {
        return $"{{Colors: [{string.Join(", ", Colors ?? Array.Empty<Color>())}], Version: {Version}, Reference Count: {ReferenceCount}}}";
    }
}
