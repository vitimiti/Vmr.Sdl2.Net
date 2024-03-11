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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.EventsManagement;
using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Input.MouseUtilities;
using Vmr.Sdl2.Net.Input.SensorUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    public const int Query = -1;
    public const int Disable = 0;
    public const int Enable = 1;

    [LibraryImport(LibraryName, EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PollEvent(out SdlEvent @event);

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlCommonEvent
    {
        public EventType Type;
        public uint TimeStamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlDisplayEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint Display;
        public DisplayEventId Event;
        private byte _padding1;
        private byte _padding2;
        private byte _padding3;
        public int Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlWindowEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public WindowEventId Event;
        private byte _padding1;
        private byte _padding2;
        private byte _padding3;
        public int Data1;
        public int Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlKeyboardEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public KeyState State;
        public byte Repeat;
        private byte _padding2;
        private byte _padding3;
        public KeySymbolMarshaller.KeySymbol KeySymbol;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlTextEditingEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public fixed byte Text[32];
        public int Start;
        public int Length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlTextEditingExtEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public byte* Text;
        public int Start;
        public int Length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlTextInputEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public fixed byte Text[32];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlMouseMotionEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public uint Which;
        public uint State;
        public int X;
        public int Y;
        public int XRel;
        public int YRel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlMouseButtonEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public uint Which;
        public byte Button;
        public ButtonState State;
        public byte Clicks;
        private byte _padding1;
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlMouseWheelEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public uint Which;
        public int X;
        public int Y;
        public MouseWheelDirection Direction;
        public float PreciseX;
        public float PreciseY;
        public int MouseX;
        public int MouseY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickAxisEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Axis;
        private byte _padding1;
        private byte _padding2;
        private byte _padding3;
        public short Value;
        private ushort _padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickBallEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Ball;
        private byte _padding1;
        private byte _padding2;
        private byte _padding3;
        public short XRel;
        public short YRel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickHatEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Hat;
        public HatPositions Value;
        private byte _padding1;
        private byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickButtonEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Button;
        public ButtonState State;
        private byte _padding1;
        private byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickDeviceEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public int Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlJoystickBatteryEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public JoystickPowerLevel Level;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerAxisEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Axis;
        public byte _padding1;
        public byte _padding2;
        public byte _padding3;
        public short Value;
        private ushort _padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerButtonEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public byte Button;
        public ButtonState State;
        private byte _padding1;
        private byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerDeviceEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public int Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerTouchpadEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public int Touchpad;
        public int Finger;
        public float X;
        public float Y;
        public float Pressure;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerSensorEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long Which;
        public SensorType Sensor;
        public fixed float Data[3];
        public ulong TimeStampUs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlAudioDeviceEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint Which;
        public byte IsCapture;
        private byte _padding1;
        private byte _padding2;
        private byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlTouchFingerEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long TouchId;
        public long FingerId;
        public float X;
        public float Y;
        public float Dx;
        public float Dy;
        public float Pressure;
        public uint WindowId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlMultiGestureEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long TouchId;
        public float DTheta;
        public float DDist;
        public float X;
        public float Y;
        public ushort NumFingers;
        private ushort _padding;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlDollarGestureEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public long TouchId;
        public long GestureId;
        public uint NumFingers;
        public float Error;
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlDropEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public byte* File;
        public uint WindowId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlSensorEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public int Which;
        public fixed float Data[6];
        public ulong TimeStampUs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlQuitEvent
    {
        public EventType Type;
        public uint TimeStamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlUserEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public uint WindowId;
        public int Code;
        public void* Data1;
        public void* Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlSysWmEvent
    {
        public EventType Type;
        public uint TimeStamp;
        public nint Msg;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SdlEvent
    {
        [FieldOffset(0)] public EventType Type;

        [FieldOffset(0)] public SdlCommonEvent Common;

        [FieldOffset(0)] public SdlDisplayEvent Display;

        [FieldOffset(0)] public SdlWindowEvent Window;

        [FieldOffset(0)] public SdlKeyboardEvent Key;

        [FieldOffset(0)] public SdlTextEditingEvent Edit;

        [FieldOffset(0)] public SdlTextEditingExtEvent EditExt;

        [FieldOffset(0)] public SdlTextInputEvent Text;

        [FieldOffset(0)] public SdlMouseMotionEvent Motion;

        [FieldOffset(0)] public SdlMouseButtonEvent Button;

        [FieldOffset(0)] public SdlMouseWheelEvent Wheel;

        [FieldOffset(0)] public SdlJoystickAxisEvent JAxis;

        [FieldOffset(0)] public SdlJoystickBallEvent JBall;

        [FieldOffset(0)] public SdlJoystickHatEvent JHat;

        [FieldOffset(0)] public SdlJoystickButtonEvent JButton;

        [FieldOffset(0)] public SdlJoystickDeviceEvent JDevice;

        [FieldOffset(0)] public SdlJoystickBatteryEvent JBattery;

        [FieldOffset(0)] public SdlGameControllerAxisEvent CAxis;

        [FieldOffset(0)] public SdlGameControllerButtonEvent CButton;

        [FieldOffset(0)] public SdlGameControllerDeviceEvent CDevice;

        [FieldOffset(0)] public SdlGameControllerTouchpadEvent CTouchpad;

        [FieldOffset(0)] public SdlGameControllerSensorEvent CSensor;

        [FieldOffset(0)] public SdlAudioDeviceEvent ADevice;

        [FieldOffset(0)] public SdlSensorEvent Sensor;

        [FieldOffset(0)] public SdlQuitEvent QuitEvent;

        [FieldOffset(0)] public SdlUserEvent User;

        [FieldOffset(0)] public SdlSysWmEvent SysWm;

        [FieldOffset(0)] public SdlTouchFingerEvent TFinger;

        [FieldOffset(0)] public SdlMultiGestureEvent MGesture;

        [FieldOffset(0)] public SdlDollarGestureEvent DGesture;

        [FieldOffset(0)] public SdlDropEvent Drop;

        [FieldOffset(0)] private fixed byte _padding[56];
    }
}
