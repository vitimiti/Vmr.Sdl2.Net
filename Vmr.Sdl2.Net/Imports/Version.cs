// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetVersion(
        [MarshalUsing(typeof(SdlVersionMarshaller))] out Version version
    );

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetRevision",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetRevision();
}
