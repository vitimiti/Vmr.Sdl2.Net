// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetKeyboardFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetKeyboardFocus();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetKeyboardState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetKeyboardState(out int numKeys);

    [LibraryImport(LibraryName, EntryPoint = "SDL_ResetKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetKeyboard();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial KeyModifiers GetModState();

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetModState(KeyModifiers modState);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetKeyFromScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial KeyCode GetKeyFromScanCode(ScanCode scanCode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetScancodeFromKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ScanCode GetScanCodeFromKey(KeyCode key);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetScancodeName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetScanCodeName(ScanCode scanCode);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetScancodeFromName",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ScanCode GetScanCodeFromName(string name);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetKeyName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetKeyName(KeyCode key);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetKeyFromName",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial KeyCode GetKeyFromName(string name);

    [LibraryImport(LibraryName, EntryPoint = "SDL_StartTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartTextInput();

    [LibraryImport(LibraryName, EntryPoint = "SDL_IsTextInputActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IsTextInputActive();

    [LibraryImport(LibraryName, EntryPoint = "SDL_StopTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopTextInput();

    [LibraryImport(LibraryName, EntryPoint = "SDL_ClearComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ClearComposition();

    [LibraryImport(LibraryName, EntryPoint = "SDL_IsTextInputShown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IsTextInputShown();

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetTextInputRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTextInputRect(Rectangle rectangle);

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasScreenKeyboardSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool HasScreenKeyboardSupport();

    [LibraryImport(LibraryName, EntryPoint = "SDL_IsScreenKeyboardShown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IsScreenKeyboardShown();
}
