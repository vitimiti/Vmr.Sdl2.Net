// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
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
