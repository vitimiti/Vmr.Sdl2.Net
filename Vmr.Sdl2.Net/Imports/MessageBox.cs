// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Video;
using Vmr.Sdl2.Net.Video.Messages;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_ShowMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ShowMessageBox(MessageBoxData messageBoxData, out int buttonId);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_ShowSimpleMessageBox",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ShowSimpleMessageBox(
        MessageBoxOptions flags,
        string title,
        string message,
        Window window
    );

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_ShowSimpleMessageBox",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ShowSimpleMessageBox(
        MessageBoxOptions flags,
        string title,
        string message,
        nint window
    );
}
