// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_SIMDGetAlignment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SimdGetAlignment();

    [LibraryImport(LibraryName, EntryPoint = "SDL_SIMDAlloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SimdAlloc(nuint len);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SIMDRealloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SimdRealloc(Simd mem, nuint len);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SIMDFree")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SimdFree(nint ptr);
}
