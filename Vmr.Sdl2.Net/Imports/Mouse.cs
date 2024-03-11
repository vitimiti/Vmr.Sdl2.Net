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
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.CursorUtilities;
using Vmr.Sdl2.Net.Input.MouseUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetMouseFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetMouseFocus();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MouseButtons GetMouseState(out int x, out int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MouseButtons GetGlobalMouseState(out int x, out int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MouseButtons GetRelativeMouseState(out int x, out int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_WarpMouseInWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WarpMouseInWindow(Window window, int x, int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_WarpMouseGlobal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int WarpMouseGlobal(int x, int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetRelativeMouseMode(
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool enabled
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CaptureMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CaptureMouse(
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool captured
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GetRelativeMouseMode();

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateCursor(
        byte* data,
        byte* mask,
        int w,
        int h,
        int hotX,
        int hotY
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateColorCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateColorCursor(Graphics.Surface surface, int hotX, int hotY);

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateSystemCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateSystemCursor(SystemCursor id);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCursor(Cursor cursor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetCursor();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDefaultCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetDefaultCursor();

    [LibraryImport(LibraryName, EntryPoint = "SDL_FreeCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeCursor(nint cursor);

    [LibraryImport(LibraryName, EntryPoint = "SDL_ShowCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ShowCursor(int toggle);
}
