// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerButtonBindUtilities;
using Vmr.Sdl2.Net.Input.SensorUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Imports;

internal static unsafe partial class Sdl
{
    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerAddMappingsFromRw(
        RwOps rw,
        [MarshalUsing(typeof(IntBoolMarshaller))] bool freeRw
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerAddMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerAddMapping(GameControllerMapping mapping);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerNumMappings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerNumMappings();

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerMapping GameControllerMappingForIndex(int index);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerMapping GameControllerMappingForGuid(
        [MarshalUsing(typeof(GuidMarshaller))] Guid guid
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerMapping GameControllerMapping(
        GameController gameController
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_IsGameController")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool IsGameController(int joystickIndex);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerNameForIndex",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerNameForIndex(int joystickIndex);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerPathForIndex",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerPathForIndex(int joystickIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerTypeForIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerType GameControllerTypeForIndex(int joystickIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForDeviceIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerMapping GameControllerMappingForDeviceIndex(
        int joystickIndex
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GameControllerOpen(int joystickIndex);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerFromInstanceID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GameControllerFromInstanceId(int joyId);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GameControllerFromPlayerIndex(int playerIndex);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerName(GameController gameController);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerPath",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerPath(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerType GameControllerGetType(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetPlayerIndex(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerSetPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GameControllerSetPlayerIndex(
        GameController gameController,
        int playerIndex
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort GameControllerGetVendor(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort GameControllerGetProduct(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort GameControllerGetProductVersion(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort GameControllerGetFirmwareVersion(GameController gameController);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetSerial",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerGetSerial(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetSteamHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GameControllerGetSteamHandle(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetAttached")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerGetAttached(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GameControllerGetJoystick(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerEventState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerEventState(int state);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GameControllerUpdate();

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetAxisFromString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerAxis GameControllerGetAxisFromString(string str);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetStringForAxis",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerGetStringForAxis(GameControllerAxis axis);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SdlGameControllerButtonBind GameControllerGetBindForAxis(
        GameController gameController,
        GameControllerAxis axis
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasAxis(
        GameController gameController,
        GameControllerAxis axis
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial short GameControllerGetAxis(
        GameController gameController,
        GameControllerAxis axis
    );

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetButtonFromString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GameControllerButton GameControllerGetButtonFromString(string str);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetStringForButton",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerGetStringForButton(GameControllerButton button);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SdlGameControllerButtonBind GameControllerGetBindForButton(
        GameController gameController,
        GameControllerButton button
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasButton(
        GameController gameController,
        GameControllerButton button
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ButtonState GameControllerGetButton(
        GameController gameController,
        GameControllerButton button
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetNumTouchpads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetNumTouchpads(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetNumTouchpadFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetNumTouchpadFingers(
        GameController gameController,
        int touchpad
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetTouchpadFinger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetTouchpadFinger(
        GameController gameController,
        int touchpad,
        int finger,
        out FingerState state,
        out float x,
        out float y,
        out float pressure
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasSensor(
        GameController gameController,
        SensorType type
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerSetSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerSetSensorEnabled(
        GameController gameController,
        SensorType type,
        [MarshalUsing(typeof(BoolEnumMarshaller))] bool enabled
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerIsSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerIsSensorEnabled(
        GameController gameController,
        SensorType type
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetSensorDataRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GameControllerGetSensorDataRate(
        GameController gameController,
        SensorType type
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetSensorData(
        GameController gameController,
        SensorType type,
        float* data,
        int numValues
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerGetSensorDataWithTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerGetSensorDataWithTimestamp(
        GameController gameController,
        SensorType type,
        out ulong timeStamp,
        float* data,
        int numValues
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerRumble(
        GameController gameController,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint milliseconds
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerRumbleTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerRumbleTriggers(
        GameController gameController,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint milliseconds
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasLed(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasRumble(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerHasRumbleTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(BoolEnumMarshaller))]
    public static partial bool GameControllerHasRumbleTriggers(GameController gameController);

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerSetLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerSetLed(
        GameController gameController,
        byte red,
        byte green,
        byte blue
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerSendEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GameControllerSendEffect(
        GameController gameController,
        [MarshalUsing(typeof(ArrayMarshaller<byte, byte>), CountElementName = nameof(size))]
            byte[] data,
        int size
    );

    [LibraryImport(LibraryName, EntryPoint = "SDL_GameControllerClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GameControllerClose(nint gameController);

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetAppleSFSymbolsNameForButton",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GameControllerGetAppleSfSymbolsNameForButton(
        GameController gameController,
        GameControllerButton button
    );

    [LibraryImport(
        LibraryName,
        EntryPoint = "SDL_GameControllerGetAppleSFSymbolsNameForAxis",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(OwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GameControllerGetAppleSfSymbolsNameForAxis(
        GameController gameController,
        GameControllerAxis axis
    );

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlGameControllerButtonBind
    {
        public GameControllerBindType BindType;
        public int Val0;
        public int Val1;
    }
}
