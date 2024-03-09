// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.TouchUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetNumTouchDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumTouchDevices();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetTouchDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetTouchDevice(int index);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetTouchName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetTouchName(int index);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetTouchDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial TouchDeviceType GetTouchDeviceType(long touchId);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetNumTouchFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumTouchFingers(long touchId);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetTouchFinger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Finger GetTouchFinger(long touchId, int index);
}
