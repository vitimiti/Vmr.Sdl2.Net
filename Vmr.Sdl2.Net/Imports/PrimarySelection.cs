// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_SetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetPrimarySelectionText(string? text);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetPrimarySelectionText();

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlBoolMarshaller))]
    public static partial bool HasPrimarySelectionText();
}
