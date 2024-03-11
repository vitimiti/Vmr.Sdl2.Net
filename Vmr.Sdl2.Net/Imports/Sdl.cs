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
        new KeyValuePair<bool, string>(OperatingSystem.IsLinux(), "libSDL2-2.0.so")
    ];

    static Sdl()
    {
        NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, DllImporter);
    }

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
