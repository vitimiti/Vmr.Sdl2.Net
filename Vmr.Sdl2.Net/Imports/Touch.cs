// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

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
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
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
