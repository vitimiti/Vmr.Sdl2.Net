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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Video.Displays;
using Vmr.Sdl2.Net.Video.OpenGl;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumVideoDrivers();

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetVideoDriver",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetVideoDriver(int index);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetCurrentVideoDriver",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetCurrentVideoDriver();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetNumVideoDisplays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumVideoDisplays();

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetDisplayName",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetDisplayName(int displayIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDisplayBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDisplayBounds(
        int displayIndex,
        [MarshalUsing(typeof(RectangleMarshaller))]
        out Rectangle rect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDisplayUsableBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDisplayUsableBounds(
        int displayIndex,
        [MarshalUsing(typeof(RectangleMarshaller))]
        out Rectangle rect
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDisplayDPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDisplayDpi(
        int displayIndex,
        out float dDpi,
        out float hDpi,
        out float vDpi
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial DisplayOrientation GetDisplayOrientation(int displayIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetNumDisplayModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumDisplayModes(int displayIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDisplayMode(int displayIndex, int modeIndex, DisplayMode* mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetDesktopDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDesktopDisplayMode(int displayIndex, DisplayMode* mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetCurrentDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCurrentDisplayMode(int displayIndex, DisplayMode* mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetClosestDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetClosestDisplayMode(
        int displayIndex,
        Video.Displays.DisplayMode mode,
        DisplayMode* closest
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetPointDisplayIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPointDisplayIndex(
        [MarshalUsing(typeof(PointMarshaller))]
        Point point
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetRectDisplayIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRectDisplayIndex(
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle rectangle
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowDisplayIndex(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowDisplayMode(Window window, Video.Displays.DisplayMode? mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowDisplayMode(Window window, DisplayMode* mode);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowICCProfile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(size))]
    public static partial byte[]? GetWindowIccProfile(Window window, out nuint size);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetWindowPixelFormat(Window window);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_CreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateWindow(
        string title,
        int x,
        int y,
        int w,
        int h,
        WindowOptions flags
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_CreateWindowFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateWindowFrom(void* data);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetWindowId(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetWindowFromId(uint id);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WindowOptions GetWindowFlags(Window window);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_SetWindowTitle",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowTitle(Window window, string title);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetWindowTitle",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetWindowTitle(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowIcon(Window window, Graphics.Surface icon);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_SetWindowData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SetWindowData(Window window, string name, void* userData);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GetWindowData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* GetWindowData(Window window, string name);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowPosition(Window window, int x, int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowPosition(Window window, out int x, out int y);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowSize(Window window, int w, int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowSize(Window window, out int w, out int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowBordersSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowBordersSize(
        Window window,
        out int top,
        out int left,
        out int bottom,
        out int right
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowSizeInPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowSizeInPixels(Window window, out int w, out int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowMinimumSize(Window window, int w, int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowMinimumSize(Window window, out int w, out int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowMaximumSize(Window window, int w, int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowMaximumSize(Window window, out int w, out int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowBordered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowBordered(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool bordered
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowResizable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowResizable(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool bordered
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowAlwaysOnTop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowAlwaysOnTop(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool bordered
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_ShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ShowWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_HideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HideWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_RaiseWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RaiseWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_MaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MaximizeWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_MinimizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MinimizeWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_RestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RestoreWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowFullscreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowFullscreen(Window window, WindowOptions flags);

    [LibraryImport(LibraryName, EntryPoint = "SDL_HasWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool HasWindowSurface(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetWindowSurface(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpdateWindowSurface(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int UpdateWindowSurfaceRects(
        Window window,
        [MarshalUsing(
            typeof(ArrayMarshaller<Rectangle, RectangleMarshaller.Rectangle>),
            CountElementName = nameof(numRects)
        )]
        Rectangle[] rects,
        int numRects
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int DestroyWindowSurface(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowGrab(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool grabbed
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowKeyboardGrab(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool grabbed
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowMouseGrab(
        Window window,
        [MarshalUsing(typeof(BoolEnumMarshaller))]
        bool grabbed
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GetWindowGrab(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GetWindowKeyboardGrab(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GetWindowMouseGrab(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetGrabbedWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetGrabbedWindow();

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowMouseRect(
        Window window,
        [MarshalUsing(typeof(RectangleMarshaller))]
        Rectangle rectangle
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(RectangleMarshaller))]
    public static partial Rectangle GetWindowMouseRect(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowBrightness(Window window, float brightness);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowBrightness")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetWindowBrightness(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowOpacity(Window window, float opacity);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetWindowOpacity(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowModalFor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowModalFor(Window modal, Window parent);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowInputFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowInputFocus(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowGammaRamp(
        Window window,
        [In] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] red,
        [In] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] green,
        [In] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] blue
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowGammaRamp(
        Window window,
        [Out] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] red,
        [Out] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] green,
        [Out] [MarshalUsing(typeof(ArrayMarshaller<ushort, ushort>), ConstantElementCount = 256)]
        ushort[] blue
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_SetWindowHitTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SetWindowHitTest(
        Window window,
        delegate* unmanaged[Cdecl]<void*, PointMarshaller.Point*, void*, HitTestResult> callback,
        void* callbackData
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FlashWindow(Window window, FlashOperation operation);

    [LibraryImport(LibraryName, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(nint window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_IsScreenSaverEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IsScreenSaverEnabled();

    [LibraryImport(LibraryName, EntryPoint = "SDL_EnableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void EnableScreenSaver();

    [LibraryImport(LibraryName, EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DisableScreenSaver();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GlCreateContext(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_MakeCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GlMakeCurrent(Window window, Context context);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_GetCurrentWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GlGetCurrentWindow();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_GetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GlGetCurrentContext();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_GetDrawableSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GlGetDrawableSize(Window window, out int w, out int h);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_SetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GlSetSwapInterval(SwapInterval interval);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_GetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SwapInterval GlGetSwapInterval();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_SwapWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GlSwapWindow(Window window);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GL_DeleteContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GlDeleteContext(nint context);

    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayMode
    {
        public uint Format;
        public int W;
        public int H;
        public int RefreshRate;
        public void* DriverData;
    }
}
