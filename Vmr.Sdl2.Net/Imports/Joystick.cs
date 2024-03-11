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
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Imports;

internal static partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_LockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void LockJoysticks();

    [LibraryImport(LibraryName, EntryPoint = "SDL_UnlockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockJoysticks();

    [LibraryImport(LibraryName, EntryPoint = "SDL_NumJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int NumJoysticks();

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_JoystickNameForIndex",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? JoystickNameForIndex(int deviceIndex);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_JoystickPathForIndex",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? JoystickPathForIndex(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDevicePlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickGetDevicePlayerIndex(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(GuidMarshaller))]
    public static partial Guid JoystickGetDeviceGuid(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetDeviceVendor(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetDeviceProduct(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetDeviceProductVersion(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial JoystickType JoystickGetDeviceType(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickGetDeviceInstanceId(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint JoystickOpen(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickFromInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint JoystickFromInstanceId(int instanceId);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint JoystickFromPlayerIndex(int playerIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickAttachVirtual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickAttachVirtual(
        JoystickType type,
        int nAxes,
        int nButtons,
        int nHats
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickAttachVirtualEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickAttachVirtualEx(VirtualJoystickDesc desc);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickDetachVirtual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickDetachVirtual(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickIsVirtual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickIsVirtual(int deviceIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSetVirtualAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickSetVirtualAxis(Joystick joystick, int axis, short value);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSetVirtualButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickSetVirtualButton(
        Joystick joystick,
        int button,
        ButtonState value
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSetVirtualHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickSetVirtualHat(Joystick joystick, int hat, HatState value);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_JoystickName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? JoystickName(Joystick joystick);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_JoystickPath",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? JoystickPath(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickGetPlayerIndex(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSetPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void JoystickSetPlayerIndex(Joystick joystick, int playerIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(GuidMarshaller))]
    public static partial Guid JoystickGetGuid(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetVendor(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetProduct(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetProductVersion(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort JoystickGetFirmwareVersion(Joystick joystick);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_JoystickGetSerial",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? JoystickGetSerial(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial JoystickType JoystickGetType(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GetJoystickGUIDInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetJoystickGuidInfo(
        [MarshalUsing(typeof(GuidMarshaller))] Guid guid,
        out ushort vendor,
        out ushort product,
        out ushort version,
        out ushort crc16
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickGetAttached(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickInstanceId(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickNumAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickNumAxes(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickNumBalls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickNumBalls(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickNumHats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickNumHats(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickNumButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickNumButtons(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void JoystickUpdate();

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickEventState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickEventState(int state);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial short JoystickGetAxis(Joystick joystick, int axis);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetAxisInitialState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickGetAxisInitialState(
        Joystick joystick,
        int axis,
        out short state
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HatPositions JoystickGetHat(Joystick joystick, int hat);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetBall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickGetBall(Joystick joystick, int ball, out int dX, out int dY);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickGetButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ButtonState JoystickGetButton(Joystick joystick, int button);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickRumble(
        Joystick joystick,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationInMs
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickRumbleTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickRumbleTriggers(
        Joystick joystick,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationInMs
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickHasLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickHasLed(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickHasRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickHasRumble(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickHasRumbleTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool JoystickHasRumbleTriggers(Joystick joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSetLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickSetLed(Joystick joystick, byte red, byte green, byte blue);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickSendEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int JoystickSendEffect(
        Joystick joystick,
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(size))]
        byte[] data,
        int size
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void JoystickClose(nint joystick);

    [LibraryImport(LibraryName, EntryPoint = "SDL_JoystickCurrentPowerLevel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial JoystickPowerLevel JoystickCurrentPowerLevel(Joystick joystick);
}
