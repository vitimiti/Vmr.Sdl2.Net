// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_RecordGesture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RecordGesture(long touchId);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SaveAllDollarTemplates")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SaveAllDollarTemplates(RwOps dst);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SaveDollarTemplate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(IntBoolMarshaller))]
    public static partial bool SaveDollarTemplate(long touchId, RwOps dst);

    [LibraryImport(LibraryName, EntryPoint = "SDL_LoadDollarTemplates")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LoadDollarTemplates(long touchId, RwOps src);
}
