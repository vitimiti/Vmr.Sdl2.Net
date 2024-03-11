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
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.EventsManagement;
using Vmr.Sdl2.Net.EventsManagement.DisplayIdEvents;
using Vmr.Sdl2.Net.EventsManagement.WindowIdEvents;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Input.MouseUtilities;
using Vmr.Sdl2.Net.Input.MultiGestureUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.Displays;

namespace Vmr.Sdl2.Net;

public delegate void QuitEventHandler(object? sender, QuitEventArgs eventArgs);

public delegate void DisplayEventHandler(object? sender, DisplayEventArgs eventArgs);

public delegate void DisplayOrientationEventHandler(
    object? sender,
    DisplayOrientationEventArgs eventArgs
);

public delegate void WindowEventHandler(object? sender, WindowEventArgs eventArgs);

public delegate void WindowMovedEventHandler(object? sender, WindowMovedEventArgs eventArgs);

public delegate void WindowResizedEventHandler(object? sender, WindowResizedEventArgs eventArgs);

public delegate void WindowDisplayChangedEventHandler(
    object? sender,
    WindowDisplayChangedEventArgs eventArgs
);

public delegate void SysWmEventHandler(object? sender, SysWmEventArgs eventArgs);

public delegate void KeyboardEventHandler(object? sender, KeyboardEventArgs eventArgs);

public delegate void TextEditingEventHandler(object? sender, TextEditingEventArgs eventArgs);

public delegate void TextInputEventHandler(object? sender, TextInputEventArgs eventArgs);

public delegate void MouseMotionEventHandler(object? sender, MouseMotionEventArgs eventArgs);

public delegate void MouseButtonEventHandler(object? sender, MouseButtonEventArgs eventArgs);

public delegate void MouseWheelEventHandler(object? sender, MouseWheelEventArgs eventArgs);

public delegate void JoystickAxisEventHandler(object? sender, JoystickAxisEventArgs eventArgs);

public delegate void JoystickBallEventHandler(object? sender, JoystickBallEventArgs eventArgs);

public delegate void JoystickHatEventHandler(object? sender, JoystickHatEventArgs eventArgs);

public delegate void JoystickButtonEventHandler(object? sender, JoystickButtonEventArgs eventArgs);

public delegate void JoystickDeviceEventHandler(object? sender, JoystickDeviceEventArgs eventArgs);

public delegate void JoystickBatteryEventHandler(
    object? sender,
    JoystickBatteryEventArgs eventArgs
);

public delegate void GameControllerAxisEventHandler(
    object? sender,
    GameControllerAxisEventArgs eventArgs
);

public delegate void GameControllerButtonEventHandler(
    object? sender,
    GameControllerButtonEventArgs eventArgs
);

public delegate void GameControllerDeviceEventHandler(
    object? sender,
    GameControllerDeviceEventArgs eventArgs
);

public delegate void GameControllerTouchpadEventHandler(
    object? sender,
    GameControllerTouchpadEventArgs eventArgs
);

public delegate void GameControllerSensorEventHandler(
    object? sender,
    GameControllerSensorEventArgs eventArgs
);

public delegate void AudioDeviceEventHandler(object? sender, AudioDeviceEventArgs eventArgs);

public delegate void TouchFingerEventHandler(object? sender, TouchFingerEventArgs eventArgs);

public delegate void MultiGestureEventHandler(object? sender, MultiGestureEventArgs eventArgs);

public delegate void DollarGestureEventHandler(object? sender, DollarGestureEventArgs eventArgs);

public delegate void DropEventHandler(object? sender, DropEventArgs eventArgs);

public delegate void SensorEventHandler(object? sender, SensorEventArgs eventArgs);

public static class Events
{
    public static event QuitEventHandler? OnQuit;
    public static event EventHandler? OnAppTerminating;
    public static event EventHandler? OnAppLowMemory;
    public static event EventHandler? OnAppWillEnterBackground;
    public static event EventHandler? OnAppDidEnterBackground;
    public static event EventHandler? OnAppWillEnterForeground;
    public static event EventHandler? OnAppDidEnterForeground;
    public static event EventHandler? OnLocaleChanged;
    public static event DisplayEventHandler? OnDisplayEvent;
    public static event DisplayOrientationEventHandler? OnDisplayOrientation;
    public static event DisplayEventHandler? OnDisplayConnected;
    public static event DisplayEventHandler? OnDisplayDisconnected;
    public static event DisplayEventHandler? OnDisplayMoved;
    public static event WindowEventHandler? OnWindowEvent;
    public static event WindowEventHandler? OnWindowShown;
    public static event WindowEventHandler? OnWindowHidden;
    public static event WindowEventHandler? OnWindowExposed;
    public static event WindowMovedEventHandler? OnWindowMoved;
    public static event WindowResizedEventHandler? OnWindowResized;
    public static event WindowResizedEventHandler? OnWindowSizeChanged;
    public static event WindowEventHandler? OnWindowMinimized;
    public static event WindowEventHandler? OnWindowMaximized;
    public static event WindowEventHandler? OnWindowRestored;
    public static event WindowEventHandler? OnWindowEnter;
    public static event WindowEventHandler? OnWindowLeave;
    public static event WindowEventHandler? OnWindowFocusGained;
    public static event WindowEventHandler? OnWindowFocusLost;
    public static event WindowEventHandler? OnWindowClose;
    public static event WindowEventHandler? OnWindowTakeFocus;
    public static event WindowEventHandler? OnWindowHitTest;
    public static event WindowEventHandler? OnWindowIccProfileChanged;
    public static event WindowDisplayChangedEventHandler? OnWindowDisplayChanged;
    public static event SysWmEventHandler? OnSysWm;
    public static event KeyboardEventHandler? OnKeyDown;
    public static event KeyboardEventHandler? OnKeyUp;
    public static event TextEditingEventHandler? OnTextEditing;
    public static event TextInputEventHandler? OnTextInput;
    public static event EventHandler? OnKeymapChanged;
    public static event TextEditingEventHandler? OnTextEditingExt;
    public static event MouseMotionEventHandler? OnMouseMotion;
    public static event MouseButtonEventHandler? OnMouseButtonDown;
    public static event MouseButtonEventHandler? OnMouseButtonUp;
    public static event MouseWheelEventHandler? OnMouseWheel;
    public static event JoystickAxisEventHandler? OnJoystickAxisMotion;
    public static event JoystickBallEventHandler? OnJoystickBallMotion;
    public static event JoystickHatEventHandler? OnJoystickHatMotion;
    public static event JoystickButtonEventHandler? OnJoystickButtonDown;
    public static event JoystickButtonEventHandler? OnJoystickButtonUp;
    public static event JoystickDeviceEventHandler? OnJoystickDeviceAdded;
    public static event JoystickDeviceEventHandler? OnJoystickDeviceRemoved;
    public static event JoystickBatteryEventHandler? OnJoystickBatteryUpdated;
    public static event GameControllerAxisEventHandler? OnGameControllerAxisMotion;
    public static event GameControllerButtonEventHandler? OnGameControllerButtonDown;
    public static event GameControllerButtonEventHandler? OnGameControllerButtonUp;
    public static event GameControllerDeviceEventHandler? OnGameControllerDeviceAdded;
    public static event GameControllerDeviceEventHandler? OnGameControllerDeviceRemoved;
    public static event GameControllerDeviceEventHandler? OnGameControllerDeviceRemapped;
    public static event GameControllerDeviceEventHandler? OnGameControllerSteamHandleUpdated;
    public static event GameControllerTouchpadEventHandler? OnGameControllerTouchpadDown;
    public static event GameControllerTouchpadEventHandler? OnGameControllerTouchpadMotion;
    public static event GameControllerTouchpadEventHandler? OnGameControllerTouchpadUp;
    public static event GameControllerSensorEventHandler? OnGameControllerSensorUpdate;
    public static event AudioDeviceEventHandler? OnAudioDeviceAdded;
    public static event AudioDeviceEventHandler? OnAudioDeviceRemoved;
    public static event TouchFingerEventHandler? OnFingerMotion;
    public static event TouchFingerEventHandler? OnFingerDown;
    public static event TouchFingerEventHandler? OnFingerUp;
    public static event MultiGestureEventHandler? OnMultiGesture;
    public static event DollarGestureEventHandler? OnDollarGesture;
    public static event DollarGestureEventHandler? OnDollarRecord;
    public static event EventHandler? OnClipboardUpdate;
    public static event DropEventHandler? OnDropBegin;
    public static event DropEventHandler? OnDropFile;
    public static event DropEventHandler? OnDropText;
    public static event DropEventHandler? OnDropComplete;
    public static event SensorEventHandler? OnSensorUpdate;
    public static event EventHandler? OnRenderTargetsReset;
    public static event EventHandler? OnRenderDeviceReset;
    public static event EventHandler? OnPollSentinel;

    internal static void Poll()
    {
        while (Sdl.PollEvent(out Sdl.SdlEvent ev) != 0)
        {
            switch (ev.Type)
            {
                case EventType.Quit:
                    OnQuit?.Invoke(
                        null,
                        new QuitEventArgs(
                            ev.QuitEvent.Type,
                            TimeSpan.FromMilliseconds(ev.QuitEvent.TimeStamp)
                        )
                    );

                    break;
                case EventType.AppTerminating:
                    OnAppTerminating?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.AppLowMemory:
                    OnAppLowMemory?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.AppWillEnterBackground:
                    OnAppWillEnterBackground?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.AppDidEnterBackground:
                    OnAppDidEnterBackground?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.AppWillEnterForeground:
                    OnAppWillEnterForeground?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.AppDidEnterForeground:
                    OnAppDidEnterForeground?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.LocaleChanged:
                    OnLocaleChanged?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.DisplayEvent:
                    OnDisplayEvent?.Invoke(
                        null,
                        new DisplayEventArgs(
                            ev.Display.Type,
                            TimeSpan.FromMilliseconds(ev.Display.TimeStamp),
                            ev.Display.Display
                        )
                    );

                    switch (ev.Display.Event)
                    {
                        case DisplayEventId.Orientation:
                            OnDisplayOrientation?.Invoke(
                                null,
                                new DisplayOrientationEventArgs(
                                    ev.Display.Type,
                                    TimeSpan.FromMilliseconds(ev.Display.TimeStamp),
                                    ev.Display.Display,
                                    (DisplayOrientation)ev.Display.Data
                                )
                            );

                            break;
                        case DisplayEventId.Connected:
                            OnDisplayConnected?.Invoke(
                                null,
                                new DisplayEventArgs(
                                    ev.Display.Type,
                                    TimeSpan.FromMilliseconds(ev.Display.TimeStamp),
                                    ev.Display.Display
                                )
                            );

                            break;
                        case DisplayEventId.Disconnected:
                            OnDisplayDisconnected?.Invoke(
                                null,
                                new DisplayEventArgs(
                                    ev.Display.Type,
                                    TimeSpan.FromMilliseconds(ev.Display.TimeStamp),
                                    ev.Display.Display
                                )
                            );

                            break;
                        case DisplayEventId.Moved:
                            OnDisplayMoved?.Invoke(
                                null,
                                new DisplayEventArgs(
                                    ev.Display.Type,
                                    TimeSpan.FromMilliseconds(ev.Display.TimeStamp),
                                    ev.Display.Display
                                )
                            );

                            break;
                        case DisplayEventId.None:
                        default:
                            break;
                    }

                    break;
                case EventType.WindowEvent:
                    OnWindowEvent?.Invoke(
                        null,
                        new WindowEventArgs(
                            ev.Window.Type,
                            TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                            ev.Window.WindowId,
                            ev.Window.Event
                        )
                    );

                    switch (ev.Window.Event)
                    {
                        case WindowEventId.Shown:
                            OnWindowShown?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Hidden:
                            OnWindowHidden?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Exposed:
                            OnWindowExposed?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Moved:
                            OnWindowMoved?.Invoke(
                                null,
                                new WindowMovedEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event,
                                    new Point(ev.Window.Data1, ev.Window.Data2)
                                )
                            );

                            break;
                        case WindowEventId.Resized:
                            OnWindowResized?.Invoke(
                                null,
                                new WindowResizedEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event,
                                    new Size(ev.Window.Data1, ev.Window.Data2)
                                )
                            );

                            break;
                        case WindowEventId.SizeChanged:
                            OnWindowSizeChanged?.Invoke(
                                null,
                                new WindowResizedEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event,
                                    new Size(ev.Window.Data1, ev.Window.Data2)
                                )
                            );

                            break;
                        case WindowEventId.Minimized:
                            OnWindowMinimized?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Maximized:
                            OnWindowMaximized?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Restored:
                            OnWindowRestored?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Enter:
                            OnWindowEnter?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Leave:
                            OnWindowLeave?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.FocusGained:
                            OnWindowFocusGained?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.FocusLost:
                            OnWindowFocusLost?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.Close:
                            OnWindowClose?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.TakeFocus:
                            OnWindowTakeFocus?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.HitTest:
                            OnWindowHitTest?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.IccProfileChanged:
                            OnWindowIccProfileChanged?.Invoke(
                                null,
                                new WindowEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event
                                )
                            );

                            break;
                        case WindowEventId.DisplayChanged:
                            OnWindowDisplayChanged?.Invoke(
                                null,
                                new WindowDisplayChangedEventArgs(
                                    ev.Window.Type,
                                    TimeSpan.FromMilliseconds(ev.Window.TimeStamp),
                                    ev.Window.WindowId,
                                    ev.Window.Event,
                                    ev.Window.Data1
                                )
                            );

                            break;
                        case WindowEventId.None:
                        default:
                            break;
                    }

                    break;
                case EventType.SysWmEvent:
                    OnSysWm?.Invoke(
                        null,
                        new SysWmEventArgs(
                            ev.SysWm.Type,
                            TimeSpan.FromMilliseconds(ev.SysWm.TimeStamp),
                            new SysWmMessageSafeHandle(ev.SysWm.Msg, true)
                        )
                    );

                    break;
                case EventType.KeyDown:
                    OnKeyDown?.Invoke(
                        null,
                        new KeyboardEventArgs(
                            ev.Key.Type,
                            TimeSpan.FromMilliseconds(ev.Key.TimeStamp),
                            ev.Key.WindowId,
                            ev.Key.State,
                            ByteBoolMarshaller.ConvertToManaged(ev.Key.Repeat),
                            KeySymbolMarshaller.ConvertToManaged(ev.Key.KeySymbol)
                        )
                    );

                    break;
                case EventType.KeyUp:
                    OnKeyUp?.Invoke(
                        null,
                        new KeyboardEventArgs(
                            ev.Key.Type,
                            TimeSpan.FromMilliseconds(ev.Key.TimeStamp),
                            ev.Key.WindowId,
                            ev.Key.State,
                            ByteBoolMarshaller.ConvertToManaged(ev.Key.Repeat),
                            KeySymbolMarshaller.ConvertToManaged(ev.Key.KeySymbol)
                        )
                    );

                    break;
                case EventType.TextEditing:
                    unsafe
                    {
                        byte* text = ev.Edit.Text;
                        OnTextEditing?.Invoke(
                            null,
                            new TextEditingEventArgs(
                                ev.Edit.Type,
                                TimeSpan.FromMilliseconds(ev.Edit.TimeStamp),
                                ev.Edit.WindowId,
                                Utf8StringMarshaller.ConvertToManaged(text),
                                ev.Edit.Start,
                                ev.Edit.Length
                            )
                        );
                    }

                    break;
                case EventType.TextInput:
                    unsafe
                    {
                        byte* text = ev.Text.Text;
                        OnTextInput?.Invoke(
                            null,
                            new TextInputEventArgs(
                                ev.Text.Type,
                                TimeSpan.FromMilliseconds(ev.Text.TimeStamp),
                                ev.Text.WindowId,
                                Utf8StringMarshaller.ConvertToManaged(text)
                            )
                        );
                    }

                    break;
                case EventType.KeymapChanged:
                    OnKeymapChanged?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.TextEditingExt:
                    unsafe
                    {
                        byte* text = ev.Edit.Text;
                        OnTextEditingExt?.Invoke(
                            null,
                            new TextEditingEventArgs(
                                ev.Edit.Type,
                                TimeSpan.FromMilliseconds(ev.Edit.TimeStamp),
                                ev.Edit.WindowId,
                                Utf8StringMarshaller.ConvertToManaged(text),
                                ev.Edit.Start,
                                ev.Edit.Length
                            )
                        );
                    }

                    break;
                case EventType.MouseMotion:
                    OnMouseMotion?.Invoke(
                        null,
                        new MouseMotionEventArgs(
                            ev.Motion.Type,
                            TimeSpan.FromMilliseconds(ev.Motion.TimeStamp),
                            ev.Motion.WindowId,
                            ev.Motion.Which,
                            ev.Motion.State,
                            new Point(ev.Motion.X, ev.Motion.Y),
                            new MouseRelativeMotion { X = ev.Motion.XRel, Y = ev.Motion.YRel }
                        )
                    );

                    break;
                case EventType.MouseButtonDown:
                    OnMouseButtonDown?.Invoke(
                        null,
                        new MouseButtonEventArgs(
                            ev.Button.Type,
                            TimeSpan.FromMilliseconds(ev.Button.TimeStamp),
                            ev.Button.WindowId,
                            ev.Button.Which,
                            ev.Button.Button,
                            ev.Button.State,
                            ev.Button.Clicks,
                            new Point(ev.Button.X, ev.Button.Y)
                        )
                    );

                    break;
                case EventType.MouseButtonUp:
                    OnMouseButtonUp?.Invoke(
                        null,
                        new MouseButtonEventArgs(
                            ev.Button.Type,
                            TimeSpan.FromMilliseconds(ev.Button.TimeStamp),
                            ev.Button.WindowId,
                            ev.Button.Which,
                            ev.Button.Button,
                            ev.Button.State,
                            ev.Button.Clicks,
                            new Point(ev.Button.X, ev.Button.Y)
                        )
                    );

                    break;
                case EventType.MouseWheel:
                    OnMouseWheel?.Invoke(
                        null,
                        new MouseWheelEventArgs(
                            ev.Wheel.Type,
                            TimeSpan.FromMilliseconds(ev.Wheel.TimeStamp),
                            ev.Wheel.WindowId,
                            ev.Wheel.Which,
                            new ScrollAmount { Horizontal = ev.Wheel.X, Vertical = ev.Wheel.Y },
                            ev.Wheel.Direction,
                            new PreciseScrollAmount
                            {
                                Horizontal = ev.Wheel.PreciseX, Vertical = ev.Wheel.PreciseY
                            },
                            new Point(ev.Wheel.MouseX, ev.Wheel.MouseY)
                        )
                    );

                    break;
                case EventType.JoystickAxisMotion:
                    OnJoystickAxisMotion?.Invoke(
                        null,
                        new JoystickAxisEventArgs(
                            ev.JAxis.Type,
                            TimeSpan.FromMilliseconds(ev.JAxis.TimeStamp),
                            ev.JAxis.Which,
                            ev.JAxis.Axis,
                            ev.JAxis.Value
                        )
                    );

                    break;
                case EventType.JoystickBallMotion:
                    OnJoystickBallMotion?.Invoke(
                        null,
                        new JoystickBallEventArgs(
                            ev.JBall.Type,
                            TimeSpan.FromMilliseconds(ev.JBall.TimeStamp),
                            ev.JBall.Which,
                            ev.JBall.Ball,
                            new BallRelativeMotion { X = ev.JBall.XRel, Y = ev.JBall.YRel }
                        )
                    );

                    break;
                case EventType.JoystickHatMotion:
                    OnJoystickHatMotion?.Invoke(
                        null,
                        new JoystickHatEventArgs(
                            ev.JHat.Type,
                            TimeSpan.FromMilliseconds(ev.JHat.TimeStamp),
                            ev.JHat.Which,
                            ev.JHat.Hat,
                            ev.JHat.Value
                        )
                    );

                    break;
                case EventType.JoystickButtonDown:
                    OnJoystickButtonDown?.Invoke(
                        null,
                        new JoystickButtonEventArgs(
                            ev.JButton.Type,
                            TimeSpan.FromMilliseconds(ev.JButton.TimeStamp),
                            ev.JButton.Which,
                            ev.JButton.Button,
                            ev.JButton.State
                        )
                    );

                    break;
                case EventType.JoystickButtonUp:
                    OnJoystickButtonUp?.Invoke(
                        null,
                        new JoystickButtonEventArgs(
                            ev.JButton.Type,
                            TimeSpan.FromMilliseconds(ev.JButton.TimeStamp),
                            ev.JButton.Which,
                            ev.JButton.Button,
                            ev.JButton.State
                        )
                    );

                    break;
                case EventType.JoystickDeviceAdded:
                    OnJoystickDeviceAdded?.Invoke(
                        null,
                        new JoystickDeviceEventArgs(
                            ev.JDevice.Type,
                            TimeSpan.FromMilliseconds(ev.JDevice.TimeStamp),
                            ev.JDevice.Which
                        )
                    );

                    break;
                case EventType.JoystickDeviceRemoved:
                    OnJoystickDeviceRemoved?.Invoke(
                        null,
                        new JoystickDeviceEventArgs(
                            ev.JDevice.Type,
                            TimeSpan.FromMilliseconds(ev.JDevice.TimeStamp),
                            ev.JDevice.Which
                        )
                    );

                    break;
                case EventType.JoystickBatteryUpdated:
                    OnJoystickBatteryUpdated?.Invoke(
                        null,
                        new JoystickBatteryEventArgs(
                            ev.JBattery.Type,
                            TimeSpan.FromMilliseconds(ev.JBattery.TimeStamp),
                            ev.JBattery.Which,
                            ev.JBattery.Level
                        )
                    );

                    break;
                case EventType.GameControllerAxisMotion:
                    OnGameControllerAxisMotion?.Invoke(
                        null,
                        new GameControllerAxisEventArgs(
                            ev.CAxis.Type,
                            TimeSpan.FromMilliseconds(ev.CAxis.TimeStamp),
                            ev.CAxis.Which,
                            (GameControllerAxis)ev.CAxis.Axis,
                            ev.CAxis.Value
                        )
                    );

                    break;
                case EventType.GameControllerButtonDown:
                    OnGameControllerButtonDown?.Invoke(
                        null,
                        new GameControllerButtonEventArgs(
                            ev.CButton.Type,
                            TimeSpan.FromMilliseconds(ev.CButton.TimeStamp),
                            ev.CButton.Which,
                            (GameControllerButton)ev.CButton.Button,
                            ev.CButton.State
                        )
                    );

                    break;
                case EventType.GameControllerButtonUp:
                    OnGameControllerButtonUp?.Invoke(
                        null,
                        new GameControllerButtonEventArgs(
                            ev.CButton.Type,
                            TimeSpan.FromMilliseconds(ev.CButton.TimeStamp),
                            ev.CButton.Which,
                            (GameControllerButton)ev.CButton.Button,
                            ev.CButton.State
                        )
                    );

                    break;
                case EventType.GameControllerDeviceAdded:
                    OnGameControllerDeviceAdded?.Invoke(
                        null,
                        new GameControllerDeviceEventArgs(
                            ev.CDevice.Type,
                            TimeSpan.FromMilliseconds(ev.CDevice.TimeStamp),
                            ev.CDevice.Which
                        )
                    );

                    break;
                case EventType.GameControllerDeviceRemoved:
                    OnGameControllerDeviceRemoved?.Invoke(
                        null,
                        new GameControllerDeviceEventArgs(
                            ev.CDevice.Type,
                            TimeSpan.FromMilliseconds(ev.CDevice.TimeStamp),
                            ev.CDevice.Which
                        )
                    );

                    break;
                case EventType.GameControllerDeviceRemapped:
                    OnGameControllerDeviceRemapped?.Invoke(
                        null,
                        new GameControllerDeviceEventArgs(
                            ev.CDevice.Type,
                            TimeSpan.FromMilliseconds(ev.CDevice.TimeStamp),
                            ev.CDevice.Which
                        )
                    );

                    break;
                case EventType.GameControllerTouchpadDown:
                    OnGameControllerTouchpadDown?.Invoke(
                        null,
                        new GameControllerTouchpadEventArgs(
                            ev.CTouchpad.Type,
                            TimeSpan.FromMilliseconds(ev.CTouchpad.TimeStamp),
                            ev.CTouchpad.Which,
                            ev.CTouchpad.Touchpad,
                            ev.CTouchpad.Finger,
                            new PointF(ev.CTouchpad.X, ev.CTouchpad.Y),
                            ev.CTouchpad.Pressure
                        )
                    );

                    break;
                case EventType.GameControllerTouchpadMotion:
                    OnGameControllerTouchpadMotion?.Invoke(
                        null,
                        new GameControllerTouchpadEventArgs(
                            ev.CTouchpad.Type,
                            TimeSpan.FromMilliseconds(ev.CTouchpad.TimeStamp),
                            ev.CTouchpad.Which,
                            ev.CTouchpad.Touchpad,
                            ev.CTouchpad.Finger,
                            new PointF(ev.CTouchpad.X, ev.CTouchpad.Y),
                            ev.CTouchpad.Pressure
                        )
                    );

                    break;
                case EventType.GameControllerTouchpadUp:
                    OnGameControllerTouchpadUp?.Invoke(
                        null,
                        new GameControllerTouchpadEventArgs(
                            ev.CTouchpad.Type,
                            TimeSpan.FromMilliseconds(ev.CTouchpad.TimeStamp),
                            ev.CTouchpad.Which,
                            ev.CTouchpad.Touchpad,
                            ev.CTouchpad.Finger,
                            new PointF(ev.CTouchpad.X, ev.CTouchpad.Y),
                            ev.CTouchpad.Pressure
                        )
                    );

                    break;
                case EventType.GameControllerSensorUpdate:
                    unsafe
                    {
                        Span<float> data = new(ev.CSensor.Data, 3);
                        OnGameControllerSensorUpdate?.Invoke(
                            null,
                            new GameControllerSensorEventArgs(
                                ev.CSensor.Type,
                                TimeSpan.FromMilliseconds(ev.CSensor.TimeStamp),
                                ev.CSensor.Which,
                                ev.CSensor.Sensor,
                                data.ToArray(),
                                TimeSpan.FromMicroseconds(ev.CSensor.TimeStampUs)
                            )
                        );
                    }

                    break;
                case EventType.GameControllerSteamHandleUpdated:
                    OnGameControllerSteamHandleUpdated?.Invoke(
                        null,
                        new GameControllerDeviceEventArgs(
                            ev.CDevice.Type,
                            TimeSpan.FromMilliseconds(ev.CDevice.TimeStamp),
                            ev.CDevice.Which
                        )
                    );

                    break;
                case EventType.FingerDown:
                    OnFingerDown?.Invoke(
                        null,
                        new TouchFingerEventArgs(
                            ev.TFinger.Type,
                            TimeSpan.FromMilliseconds(ev.TFinger.TimeStamp),
                            ev.TFinger.TouchId,
                            ev.TFinger.FingerId,
                            new PointF(ev.TFinger.X, ev.TFinger.Y),
                            new PointF(ev.TFinger.Dx, ev.TFinger.Dy),
                            ev.TFinger.Pressure,
                            ev.TFinger.WindowId
                        )
                    );

                    break;
                case EventType.FingerUp:
                    OnFingerUp?.Invoke(
                        null,
                        new TouchFingerEventArgs(
                            ev.TFinger.Type,
                            TimeSpan.FromMilliseconds(ev.TFinger.TimeStamp),
                            ev.TFinger.TouchId,
                            ev.TFinger.FingerId,
                            new PointF(ev.TFinger.X, ev.TFinger.Y),
                            new PointF(ev.TFinger.Dx, ev.TFinger.Dy),
                            ev.TFinger.Pressure,
                            ev.TFinger.WindowId
                        )
                    );

                    break;
                case EventType.FingerMotion:
                    OnFingerMotion?.Invoke(
                        null,
                        new TouchFingerEventArgs(
                            ev.TFinger.Type,
                            TimeSpan.FromMilliseconds(ev.TFinger.TimeStamp),
                            ev.TFinger.TouchId,
                            ev.TFinger.FingerId,
                            new PointF(ev.TFinger.X, ev.TFinger.Y),
                            new PointF(ev.TFinger.Dx, ev.TFinger.Dy),
                            ev.TFinger.Pressure,
                            ev.TFinger.WindowId
                        )
                    );

                    break;
                case EventType.DollarGesture:
                    OnDollarGesture?.Invoke(
                        null,
                        new DollarGestureEventArgs(
                            ev.DGesture.Type,
                            TimeSpan.FromMilliseconds(ev.DGesture.TimeStamp),
                            ev.DGesture.TouchId,
                            ev.DGesture.GestureId,
                            ev.DGesture.NumFingers,
                            ev.DGesture.Error,
                            new PointF(ev.DGesture.X, ev.DGesture.Y)
                        )
                    );

                    break;
                case EventType.DollarRecord:
                    OnDollarRecord?.Invoke(
                        null,
                        new DollarGestureEventArgs(
                            ev.DGesture.Type,
                            TimeSpan.FromMilliseconds(ev.DGesture.TimeStamp),
                            ev.DGesture.TouchId,
                            ev.DGesture.GestureId,
                            ev.DGesture.NumFingers,
                            ev.DGesture.Error,
                            new PointF(ev.DGesture.X, ev.DGesture.Y)
                        )
                    );

                    break;
                case EventType.MultiGesture:
                    OnMultiGesture?.Invoke(
                        null,
                        new MultiGestureEventArgs(
                            ev.MGesture.Type,
                            TimeSpan.FromMilliseconds(ev.MGesture.TimeStamp),
                            ev.MGesture.TouchId,
                            new MultiGestureDelta
                            {
                                Theta = ev.MGesture.DTheta, Distance = ev.MGesture.DDist
                            },
                            new PointF(ev.MGesture.X, ev.MGesture.Y),
                            ev.MGesture.NumFingers
                        )
                    );

                    break;
                case EventType.ClipboardUpdate:
                    OnClipboardUpdate?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.DropFile:
                    unsafe
                    {
                        OnDropFile?.Invoke(
                            null,
                            new DropEventArgs(
                                ev.Drop.Type,
                                TimeSpan.FromMilliseconds(ev.Drop.TimeStamp),
                                Utf8StringMarshaller.ConvertToManaged(ev.Drop.File),
                                ev.Drop.WindowId
                            )
                        );
                    }

                    break;
                case EventType.DropText:
                    unsafe
                    {
                        OnDropText?.Invoke(
                            null,
                            new DropEventArgs(
                                ev.Drop.Type,
                                TimeSpan.FromMilliseconds(ev.Drop.TimeStamp),
                                Utf8StringMarshaller.ConvertToManaged(ev.Drop.File),
                                ev.Drop.WindowId
                            )
                        );
                    }

                    break;
                case EventType.DropBegin:
                    unsafe
                    {
                        OnDropBegin?.Invoke(
                            null,
                            new DropEventArgs(
                                ev.Drop.Type,
                                TimeSpan.FromMilliseconds(ev.Drop.TimeStamp),
                                Utf8StringMarshaller.ConvertToManaged(ev.Drop.File),
                                ev.Drop.WindowId
                            )
                        );
                    }

                    break;
                case EventType.DropComplete:
                    unsafe
                    {
                        OnDropComplete?.Invoke(
                            null,
                            new DropEventArgs(
                                ev.Drop.Type,
                                TimeSpan.FromMilliseconds(ev.Drop.TimeStamp),
                                Utf8StringMarshaller.ConvertToManaged(ev.Drop.File),
                                ev.Drop.WindowId
                            )
                        );
                    }

                    break;
                case EventType.AudioDeviceAdded:
                    OnAudioDeviceAdded?.Invoke(
                        null,
                        new AudioDeviceEventArgs(
                            ev.ADevice.Type,
                            TimeSpan.FromMilliseconds(ev.ADevice.TimeStamp),
                            ev.ADevice.Which,
                            ByteBoolMarshaller.ConvertToManaged(ev.ADevice.IsCapture)
                        )
                    );

                    break;
                case EventType.AudioDeviceRemoved:
                    OnAudioDeviceRemoved?.Invoke(
                        null,
                        new AudioDeviceEventArgs(
                            ev.ADevice.Type,
                            TimeSpan.FromMilliseconds(ev.ADevice.TimeStamp),
                            ev.ADevice.Which,
                            ByteBoolMarshaller.ConvertToManaged(ev.ADevice.IsCapture)
                        )
                    );

                    break;
                case EventType.SensorUpdate:
                    unsafe
                    {
                        Span<float> data = new(ev.Sensor.Data, 6);
                        OnSensorUpdate?.Invoke(
                            null,
                            new SensorEventArgs(
                                ev.Sensor.Type,
                                TimeSpan.FromMilliseconds(ev.Sensor.TimeStamp),
                                ev.Sensor.Which,
                                data.ToArray(),
                                TimeSpan.FromMicroseconds(ev.Sensor.TimeStampUs)
                            )
                        );
                    }

                    break;
                case EventType.RenderTargetsReset:
                    OnRenderTargetsReset?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.RenderDeviceReset:
                    OnRenderDeviceReset?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.PollSentinel:
                    OnPollSentinel?.Invoke(null, EventArgs.Empty);
                    break;
                case EventType.FirstEvent:
                case EventType.GameControllerUpdateCompleteReservedForSdl3:
                case EventType.UserEvent:
                case EventType.LastEvent:
                default:
                    break;
            }
        }
    }
}
