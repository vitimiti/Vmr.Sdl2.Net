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

    private unsafe Sdl.Palette* UnsafeHandle => (Sdl.Palette*)handle;

    public Color[]? Colors
    {
        get
        {
            unsafe
            {
                if (UnsafeHandle->Colors is null)
                {
                    return null;
                }

                if (UnsafeHandle->NColors == 0)
                {
                    return Array.Empty<Color>();
                }

                Color[] colors = new Color[UnsafeHandle->NColors];
                for (int i = 0; i < UnsafeHandle->NColors; i++)
                {
                    colors[i] = SdlColorMarshaller.ConvertToManaged(UnsafeHandle->Colors[i]);
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
                return UnsafeHandle->Version;
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
