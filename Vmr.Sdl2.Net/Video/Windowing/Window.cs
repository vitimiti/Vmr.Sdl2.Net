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

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Video.Displays;
using Vmr.Sdl2.Net.Video.OpenGl;

namespace Vmr.Sdl2.Net.Video.Windowing;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Window>))]
public class Window : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<Window>
{
    private Dictionary<string, int> _dataLengths = new();

    private Window(Window preexistingWindow)
        : base(false)
    {
        handle = preexistingWindow.handle;
    }

    internal Window(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Window(string title, Point position, Size size, WindowOptions flags)
        : base(true)
    {
        handle = Sdl.CreateWindow(title, position.X, position.Y, size.Width, size.Height, flags);
        if (handle == nint.Zero)
        {
            throw new WindowException("Unable to create a new window");
        }
    }

    public Window(byte[] data)
        : base(true)
    {
        unsafe
        {
            fixed (byte* dataPtr = data)
            {
                handle = Sdl.CreateWindowFrom(dataPtr);
            }
        }

        if (handle == nint.Zero)
        {
            throw new WindowException("Unable to create a new window from the given data");
        }
    }

    public Window(uint id)
        : base(true)
    {
        handle = Sdl.GetWindowFromId(id);
        if (handle == nint.Zero)
        {
            throw new WindowException($"Unable to create a new window from the id {id}");
        }
    }

    public WindowOptions Flags => Sdl.GetWindowFlags(this);

    public string Title
    {
        get => Sdl.GetWindowTitle(this);
        set => Sdl.SetWindowTitle(this, value);
    }

    public Point Position
    {
        get
        {
            Sdl.GetWindowPosition(this, out int x, out int y);
            return new Point(x, y);
        }
        set => Sdl.SetWindowPosition(this, value.X, value.Y);
    }

    public Size Size
    {
        get
        {
            Sdl.GetWindowSize(this, out int w, out int h);
            return new Size(w, h);
        }
        set => Sdl.SetWindowSize(this, value.Width, value.Height);
    }

    public Size SizeInPixels
    {
        get
        {
            Sdl.GetWindowSizeInPixels(this, out int w, out int h);
            return new Size(w, h);
        }
    }

    public Size MinimumSize
    {
        get
        {
            Sdl.GetWindowMinimumSize(this, out int w, out int h);
            return new Size(w, h);
        }
        set => Sdl.SetWindowMinimumSize(this, value.Width, value.Height);
    }

    public Size MaximumSize
    {
        get
        {
            Sdl.GetWindowMaximumSize(this, out int w, out int h);
            return new Size(w, h);
        }
        set => Sdl.SetWindowMaximumSize(this, value.Width, value.Height);
    }

    public bool HasSurface => Sdl.HasWindowSurface(this);

    public bool Grab
    {
        get => Sdl.GetWindowGrab(this);
        set => Sdl.SetWindowGrab(this, value);
    }

    public bool KeyboardGrab
    {
        get => Sdl.GetWindowKeyboardGrab(this);
        set => Sdl.SetWindowKeyboardGrab(this, value);
    }

    public bool MouseGrab
    {
        get => Sdl.GetWindowMouseGrab(this);
        set => Sdl.SetWindowMouseGrab(this, value);
    }

    public float Brightness => Sdl.GetWindowBrightness(this);
    public float Opacity => Sdl.GetWindowOpacity(this);

    public Size DrawableSize
    {
        get
        {
            Sdl.GlGetDrawableSize(this, out int w, out int h);
            return new Size(w, h);
        }
    }

    public bool Equals(Window? other)
    {
        return other is not null
               && Flags == other.Flags
               && Title == other.Title
               && Position == other.Position
               && Size == other.Size
               && SizeInPixels == other.SizeInPixels
               && MinimumSize == other.MinimumSize
               && MaximumSize == other.MaximumSize
               && HasSurface == other.HasSurface
               && Grab == other.Grab
               && KeyboardGrab == other.KeyboardGrab
               && MouseGrab == other.MouseGrab
               && Math.Abs(Brightness - other.Brightness) < float.Epsilon
               && Math.Abs(Opacity - other.Opacity) < float.Epsilon
               && DrawableSize == other.DrawableSize;
    }

    public static Window GetGrabbed()
    {
        nint result = Sdl.GetGrabbedWindow();
        if (result == nint.Zero)
        {
            throw new WindowException("Unable to get the grabbed window");
        }

        return new Window(result, false);
    }

    public int GetDisplayIndex()
    {
        int result = Sdl.GetWindowDisplayIndex(this);
        if (result < 0)
        {
            throw new WindowException("Unable to get the window display index", result);
        }

        return result;
    }

    public void SetDisplayMode(DisplayMode? mode)
    {
        int code = Sdl.SetWindowDisplayMode(this, mode);
        if (code < 0)
        {
            throw new WindowException(
                $"Unable to set the window display mode to mode {mode}",
                code
            );
        }
    }

    public DisplayMode GetDisplayMode()
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetWindowDisplayMode(this, &mode);
            if (code < 0)
            {
                throw new WindowException("Unable to get the window display mode");
            }

            return new DisplayMode(mode);
        }
    }

    public byte[] GetIccProfile()
    {
        byte[]? result = Sdl.GetWindowIccProfile(this, out _);
        if (result is null)
        {
            throw new WindowException("Unable to get the window ICC profile data");
        }

        return result;
    }

    public uint GetPixelFormat()
    {
        uint result = Sdl.GetWindowPixelFormat(this);
        if (result == 0)
        {
            throw new WindowException("Unable to get the window pixel format");
        }

        return result;
    }

    public uint GetId()
    {
        uint result = Sdl.GetWindowId(this);
        if (result == 0)
        {
            throw new WindowException("Unable to get the window ID");
        }

        return result;
    }

    public void SetIcon(Surface icon)
    {
        Sdl.SetWindowIcon(this, icon);
    }

    public void SetData(string name, byte[]? data)
    {
        unsafe
        {
            fixed (byte* dataPtr = data)
            {
                _ = Sdl.SetWindowData(this, name, dataPtr);
            }
        }

        _dataLengths.Add(name, data?.Length ?? 0);
    }

    public byte[]? GetData(string name)
    {
        unsafe
        {
            byte* data = (byte*)Sdl.GetWindowData(this, name);
            if (data is null)
            {
                return null;
            }

            int length = _dataLengths[name];
            Span<byte> result = new(data, length);
            return result.ToArray();
        }
    }

    public WindowBorderSize GetBorderSize()
    {
        int code = Sdl.GetWindowBordersSize(
            this,
            out int top,
            out int left,
            out int bottom,
            out int right
        );

        if (code < 0)
        {
            throw new WindowException("Unable to get the window border size", code);
        }

        return new WindowBorderSize { Top = top, Left = left, Bottom = bottom, Right = right };
    }

    public void SetBordered(bool isBordered)
    {
        Sdl.SetWindowBordered(this, isBordered);
    }

    public void SetResizable(bool isResizable)
    {
        Sdl.SetWindowResizable(this, isResizable);
    }

    public void SetAlwaysOnTop(bool isAlwaysOnTop)
    {
        Sdl.SetWindowAlwaysOnTop(this, isAlwaysOnTop);
    }

    public void Show()
    {
        Sdl.ShowWindow(this);
    }

    public void Hide()
    {
        Sdl.HideWindow(this);
    }

    public void Raise()
    {
        Sdl.RaiseWindow(this);
    }

    public void Maximize()
    {
        Sdl.MaximizeWindow(this);
    }

    public void Minimize()
    {
        Sdl.MinimizeWindow(this);
    }

    public void Restore()
    {
        Sdl.RestoreWindow(this);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        WindowOptions options = WindowOptions.None;
        if (isFullScreen)
        {
            options |= WindowOptions.FullScreen;
        }

        int code = Sdl.SetWindowFullscreen(this, options);
        if (code < 0)
        {
            throw new WindowException(
                $"Unable to set the full screen mode to {isFullScreen}",
                code
            );
        }
    }

    public void SetDesktopFullScreen(bool isFullScreen)
    {
        WindowOptions options = WindowOptions.None;
        if (isFullScreen)
        {
            options |= WindowOptions.FullScreenDesktop;
        }

        int code = Sdl.SetWindowFullscreen(this, options);
        if (code < 0)
        {
            throw new WindowException(
                $"Unable to set the desktop full screen mode to {isFullScreen}",
                code
            );
        }
    }

    public Surface GetSurface()
    {
        nint result = Sdl.GetWindowSurface(this);
        if (result == nint.Zero)
        {
            throw new WindowException("Unable to get the window surface");
        }

        return new Surface(result, false);
    }

    public void UpdateSurface()
    {
        int code = Sdl.UpdateWindowSurface(this);
        if (code < 0)
        {
            throw new WindowException("Unable to update the window surface", code);
        }
    }

    public void UpdateSurface(Rectangle[] rectangles)
    {
        int code = Sdl.UpdateWindowSurfaceRects(this, rectangles, rectangles.Length);
        if (code < 0)
        {
            throw new WindowException(
                $"Unable to update the window surface with {rectangles.Length} rectangles",
                code
            );
        }
    }

    public void DestroySurface()
    {
        int code = Sdl.DestroyWindowSurface(this);
        if (code < 0)
        {
            throw new WindowException("Unable to destroy the window surface", code);
        }
    }

    public void SetMouseRectangle(Rectangle rectangle)
    {
        int code = Sdl.SetWindowMouseRect(this, rectangle);
        if (code < 0)
        {
            throw new WindowException($"Unable to set the mouse rectangle to {rectangle}", code);
        }
    }

    public Rectangle GetMouseRectangle()
    {
        Rectangle result = Sdl.GetWindowMouseRect(this);
        if (result == Rectangle.Empty)
        {
            throw new WindowException("Unable to get the mouse rectangle");
        }

        return result;
    }

    public void SetBrightness(float brightness)
    {
        int code = Sdl.SetWindowBrightness(this, brightness);
        if (code < 0)
        {
            throw new WindowException(
                $"Unable to set the window brightness to {brightness:F2}",
                code
            );
        }
    }

    public void SetOpacity(float opacity)
    {
        int code = Sdl.SetWindowOpacity(this, opacity);
        if (code < 0)
        {
            throw new WindowException($"Unable to set the window opacity to {opacity:F2}", code);
        }
    }

    public void SetModalFor(Window parent)
    {
        int code = Sdl.SetWindowModalFor(this, parent);
        if (code < 0)
        {
            throw new WindowException($"Unable to set window as modal for {parent}", code);
        }
    }

    public void SetInputFocus()
    {
        int code = Sdl.SetWindowInputFocus(this);
        if (code < 0)
        {
            throw new WindowException("Unable to set the window input focus", code);
        }
    }

    public void SetGammaRamp(WindowGammaRamp gammaRamp)
    {
        int code = Sdl.SetWindowGammaRamp(this, gammaRamp.Red, gammaRamp.Green, gammaRamp.Blue);
        if (code < 0)
        {
            throw new WindowException($"Unable to set the window gamma ramp to {gammaRamp}", code);
        }
    }

    public WindowGammaRamp GetGammaRamp()
    {
        ushort[] red = new ushort[256];
        ushort[] green = new ushort[256];
        ushort[] blue = new ushort[256];
        int code = Sdl.GetWindowGammaRamp(this, red, green, blue);
        if (code < 0)
        {
            throw new WindowException("Unable to get the window gamma ramp", code);
        }

        return new WindowGammaRamp(red, green, blue);
    }

    public void SetHitTest(HitTestFunction callback, byte[]? data)
    {
        int dataLength = data?.Length ?? 0;
        unsafe
        {
            fixed (byte* dataPtr = data)
            {
                int code = Sdl.SetWindowHitTest(
                    this,
                    (delegate* unmanaged[Cdecl]<
                        void*,
                        PointMarshaller.Point*,
                        void*,
                        HitTestResult>)
                    Marshal.GetFunctionPointerForDelegate(
                        (
                            void* windowPtr,
                            PointMarshaller.Point* sdlPoint,
                            void* passedDataPtr
                        ) =>
                        {
                            PointMarshaller.UnmanagedToManagedIn marshaller = new();
                            marshaller.FromUnmanaged(sdlPoint);
                            Span<byte> dataBytes = Span<byte>.Empty;
                            if (passedDataPtr is not null)
                            {
                                dataBytes = new Span<byte>(passedDataPtr, dataLength);
                            }

                            return callback.Invoke(
                                new Window((nint)windowPtr, false),
                                marshaller.ToManaged(),
                                dataBytes.IsEmpty ? null : dataBytes.ToArray()
                            );
                        }
                    ),
                    dataPtr
                );

                if (code < 0)
                {
                    throw new WindowException("Unable to set the window hit test function", code);
                }
            }
        }
    }

    public void Flash(FlashOperation operation)
    {
        int code = Sdl.FlashWindow(this, operation);
        if (code < 0)
        {
            throw new WindowException($"Unable to set the window flash operation to {operation}");
        }
    }

    public Context CreateOpenGlContext()
    {
        nint result = Sdl.GlCreateContext(this);
        if (result == nint.Zero)
        {
            throw new WindowException("Unable to create the window OpenGL context");
        }

        return new Context(result, true);
    }

    public void MakeOpenGlContextCurrent(Context context)
    {
        int code = Sdl.GlMakeCurrent(this, context);
        if (code < 0)
        {
            throw new WindowException("Unable to set the window current OpenGL context");
        }
    }

    public void Swap()
    {
        Sdl.GlSwapWindow(this);
    }

    public void WarpMouse(Point position)
    {
        Sdl.WarpMouseInWindow(this, position.X, position.Y);
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.DestroyWindow(handle);
        handle = nint.Zero;
        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Window)obj);
    }

    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Flags);
        code.Add(Title);
        code.Add(Position);
        code.Add(Size);
        code.Add(SizeInPixels);
        code.Add(MinimumSize);
        code.Add(MaximumSize);
        code.Add(HasSurface);
        code.Add(Grab);
        code.Add(KeyboardGrab);
        code.Add(MouseGrab);
        code.Add(Brightness);
        code.Add(Opacity);
        code.Add(DrawableSize);
        return code.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Flags: {Flags}, Title: {Title}, Position: {Position}, Size: {Size}, Size In Pixels: {SizeInPixels}, Minimum Size: {MinimumSize}, Maximum Size: {MaximumSize}, Has Surface: {HasSurface}}}";
    }

    public static bool operator ==(Window? left, Window? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Window? left, Window? right)
    {
        return !Equals(left, right);
    }
}
