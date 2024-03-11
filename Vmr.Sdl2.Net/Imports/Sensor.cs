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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.SensorUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_LockSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockSensors();

    [LibraryImport(LibraryName, EntryPoint = "SDL_UnlockSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockSensors();

    [LibraryImport(LibraryName, EntryPoint = "SDL_NumSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int NumSensors();

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SensorType SensorGetDeviceType(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetDeviceNonPortableType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetDeviceNonPortableType(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetDeviceInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetDeviceInstanceId(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SensorOpen(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorFromInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SensorFromInstanceId(int instanceId);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_SensorGetName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SensorGetName(Sensor sensor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SensorType SensorGetType(Sensor sensor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetNonPortableType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetNonPortableType(Sensor sensor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetInstanceId(Sensor sensor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetData(Sensor sensor, float* data, int numValues);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorGetDataWithTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SensorGetDataWithTimeStamp(
        Sensor sensor,
        out ulong timeStamp,
        float* data,
        int numValues
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SensorClose(nint sensor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SensorUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SensorUpdate();
}
