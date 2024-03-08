// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.OpenGl;

namespace Vmr.Sdl2.Net.Video;

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

    public Window(
        string title,
        Point position,
        Size size,
        WindowOptions flags,
        ErrorHandler errorHandler
    )
        : base(true)
    {
        handle = Sdl.CreateWindow(title, position.X, position.Y, size.Width, size.Height, flags);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Window(byte[] data, ErrorHandler errorHandler)
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
            errorHandler(Sdl.GetError());
        }
    }

    public Window(uint id, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.GetWindowFromId(id);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
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

    public static Window? GetGrabbed(ErrorHandler errorHandler)
    {
        nint result = Sdl.GetGrabbedWindow();
        if (result != nint.Zero)
        {
            return new Window(result, false);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public int GetDisplayIndex(ErrorCodeHandler errorHandler)
    {
        int result = Sdl.GetWindowDisplayIndex(this);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return result;
    }

    public void SetDisplayMode(DisplayMode? mode, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowDisplayMode(this, mode);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public DisplayMode? GetDisplayMode(ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            Sdl.DisplayMode mode = new();
            int code = Sdl.GetWindowDisplayMode(this, &mode);
            if (code >= 0)
            {
                return new DisplayMode(mode);
            }

            errorHandler(Sdl.GetError(), code);
            return null;
        }
    }

    public byte[]? GetIccProfile(ErrorHandler errorHandler)
    {
        byte[]? result = Sdl.GetWindowIccProfile(this, out _);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public uint GetPixelFormat(ErrorHandler errorHandler)
    {
        uint result = Sdl.GetWindowPixelFormat(this);
        if (result == 0)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public uint GetId(ErrorHandler errorHandler)
    {
        uint result = Sdl.GetWindowId(this);
        if (result == 0)
        {
            errorHandler(Sdl.GetError());
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
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = data[i];
            }

            return result;
        }
    }

    public WindowBorderSize GetBorderSize(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GetWindowBordersSize(
            this,
            out int top,
            out int left,
            out int bottom,
            out int right
        );

        if (code >= 0)
        {
            return new WindowBorderSize { Top = top, Left = left, Bottom = bottom, Right = right };
        }

        errorHandler(Sdl.GetError(), code);
        return new WindowBorderSize();
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

    public void SetFullScreen(bool isFullScreen, ErrorCodeHandler errorHandler)
    {
        WindowOptions options = WindowOptions.None;
        if (isFullScreen)
        {
            options |= WindowOptions.FullScreen;
        }

        int code = Sdl.SetWindowFullscreen(this, options);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetDesktopFullScreen(bool isFullScreen, ErrorCodeHandler errorHandler)
    {
        WindowOptions options = WindowOptions.None;
        if (isFullScreen)
        {
            options |= WindowOptions.FullScreenDesktop;
        }

        int code = Sdl.SetWindowFullscreen(this, options);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public Surface? GetSurface(ErrorHandler errorHandler)
    {
        nint result = Sdl.GetWindowSurface(this);
        if (result != nint.Zero)
        {
            return new Surface(result, false);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public void UpdateSurface(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.UpdateWindowSurface(this);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void UpdateSurface(Rectangle[] rectangles, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.UpdateWindowSurfaceRects(this, rectangles, rectangles.Length);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void DestroySurface(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.DestroyWindowSurface(this);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetMouseRectangle(Rectangle rectangle, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowMouseRect(this, rectangle);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public Rectangle GetMouseRectangle(ErrorHandler errorHandler)
    {
        Rectangle result = Sdl.GetWindowMouseRect(this);
        if (result == Rectangle.Empty)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public void SetBrightness(float brightness, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowBrightness(this, brightness);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetOpacity(float opacity, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowOpacity(this, opacity);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetModalFor(Window parent, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowModalFor(this, parent);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetInputFocus(ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowInputFocus(this);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetGammaRamp(WindowGammaRamp gammaRamp, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.SetWindowGammaRamp(this, gammaRamp.Red, gammaRamp.Green, gammaRamp.Blue);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public WindowGammaRamp GetGammaRamp(ErrorCodeHandler errorHandler)
    {
        ushort[] red = new ushort[256];
        ushort[] green = new ushort[256];
        ushort[] blue = new ushort[256];
        int code = Sdl.GetWindowGammaRamp(this, red, green, blue);
        if (code >= 0)
        {
            return new WindowGammaRamp(red, green, blue);
        }

        errorHandler(Sdl.GetError(), code);
        return new WindowGammaRamp();
    }

    public void SetHitTest(HitTestFunction callback, byte[]? data, ErrorCodeHandler errorHandler)
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
                        SdlPointMarshaller.SdlPoint*,
                        void*,
                        HitTestResult>)
                    Marshal.GetFunctionPointerForDelegate(
                        (
                            void* windowPtr,
                            SdlPointMarshaller.SdlPoint* sdlPoint,
                            void* passedDataPtr
                        ) =>
                        {
                            SdlPointMarshaller.UnmanagedToManagedIn marshaller = new();
                            marshaller.FromUnmanaged(sdlPoint);
                            byte[]? dataBytes = null;
                            if (passedDataPtr is not null)
                            {
                                dataBytes = new byte[dataLength];
                                for (int i = 0; i < dataLength; i++)
                                {
                                    dataBytes[i] = ((byte*)passedDataPtr)[i];
                                }
                            }

                            return callback.Invoke(
                                new Window((nint)windowPtr, false),
                                marshaller.ToManaged(),
                                dataBytes
                            );
                        }
                    ),
                    dataPtr
                );

                if (code < 0)
                {
                    errorHandler(Sdl.GetError(), code);
                }
            }
        }
    }

    public void Flash(FlashOperation operation, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.FlashWindow(this, operation);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public Context? CreateOpenGlContext(ErrorHandler errorHandler)
    {
        nint result = Sdl.GlCreateContext(this);
        if (result != nint.Zero)
        {
            return new Context(result, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public void MakeOpenGlContextCurrent(Context context, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GlMakeCurrent(this, context);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
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
