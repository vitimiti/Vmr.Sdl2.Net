// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    private const string LibraryName = "SDL2";

    private static readonly List<KeyValuePair<bool, string>> OsSpecificNames =
    [
        new KeyValuePair<bool, string>(OperatingSystem.IsWindows(), "SDL2.dll"),
        new KeyValuePair<bool, string>(OperatingSystem.IsMacOS(), "libSDL2.dylib"),
        new KeyValuePair<bool, string>(
            OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD(),
            "libSDL2-2.0.so.0"
        )
    ];

    private static string GetOsSpecificName(string libName)
    {
        return OsSpecificNames.FirstOrDefault(valuePair => valuePair.Key).Value ?? libName;
    }

    private static nint DllImporter(
        string libName,
        Assembly assembly,
        DllImportSearchPath? searchPath
    )
    {
        _ = NativeLibrary.TryLoad(
            GetOsSpecificName(libName),
            assembly,
            searchPath,
            out nint handle
        );

        return handle;
    }

    static Sdl()
    {
        NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, DllImporter);
    }

    [LibraryImport(LibraryName, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Init(ApplicationSubsystems flags);

    [LibraryImport(LibraryName, EntryPoint = "SDL_WasInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ApplicationSubsystems WasInit(ApplicationSubsystems flags);

    [LibraryImport(LibraryName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
}
