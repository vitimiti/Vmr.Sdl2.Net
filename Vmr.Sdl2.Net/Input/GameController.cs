// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerButtonBindUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Input.SensorUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<GameController>))]
public class GameController : Joystick, IEquatable<GameController>
{
    private GameController(nint preexistingHandle, bool ownsHandle)
        : base(preexistingHandle, ownsHandle)
    {
    }

    public GameController(int deviceIndex, ErrorHandler errorHandler)
        : base(InitializeBaseFromDevice(deviceIndex, errorHandler), true)
    {
    }

    public new GameControllerType Type => Sdl.GameControllerGetType(this);

    public override int PlayerIndex
    {
        get => Sdl.GameControllerGetPlayerIndex(this);
        set => Sdl.GameControllerSetPlayerIndex(this, value);
    }

    public override JoystickUsbId UsbIdInformation =>
        new()
        {
            Vendor = Sdl.GameControllerGetVendor(this),
            Product = Sdl.GameControllerGetProduct(this)
        };

    public override JoystickVersion VersionInformation =>
        new()
        {
            Product = Sdl.GameControllerGetProductVersion(this),
            Firmware = Sdl.GameControllerGetFirmwareVersion(this)
        };

    public override string? Serial => Sdl.GameControllerGetSerial(this);
    public int CountTouchpads => Sdl.GameControllerGetNumTouchpads(this);
    public override bool HasLed => Sdl.GameControllerHasLed(this);
    public override bool HasRumble => Sdl.GameControllerHasRumble(this);
    public override bool HasRumbleTriggers => Sdl.GameControllerHasRumbleTriggers(this);

    public bool Equals(GameController? other)
    {
        return other is not null
               && Type == other.Type
               && UsbIdInformation == other.UsbIdInformation
               && VersionInformation == other.VersionInformation
               && Serial == other.Serial
               && CountTouchpads == other.CountTouchpads
               && HasLed == other.HasLed
               && HasRumble == other.HasRumble
               && HasRumbleTriggers == other.HasRumbleTriggers
               && base.Equals(other);
    }

    private static nint InitializeBaseFromDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        nint deviceHandle = Sdl.GameControllerOpen(deviceIndex);
        if (deviceHandle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }

        return deviceHandle;
    }

    public static void AddMappings(RwOps rwOps, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerAddMappingsFromRw(rwOps, false);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static void AddMapping(GameControllerMapping mapping, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerAddMapping(mapping);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static GameControllerMapping GetMappingForIndex(
        int mappingIndex,
        ErrorHandler errorHandler
    )
    {
        GameControllerMapping result = Sdl.GameControllerMappingForIndex(mappingIndex);
        if (result == default)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static GameControllerMapping GetMappingForDevice(
        Guid deviceGuid,
        ErrorHandler errorHandler
    )
    {
        GameControllerMapping result = Sdl.GameControllerMappingForGuid(deviceGuid);
        if (result == default)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static new string? GetNameForDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        string? result = Sdl.GameControllerNameForIndex(deviceIndex);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static new string? GetPathForDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        string? result = Sdl.GameControllerPathForIndex(deviceIndex);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static new GameControllerType GetTypeForDevice(int deviceIndex)
    {
        return Sdl.GameControllerTypeForIndex(deviceIndex);
    }

    public static GameControllerMapping GetMappingForDevice(
        int deviceIndex,
        ErrorHandler errorHandler
    )
    {
        GameControllerMapping result = Sdl.GameControllerMappingForIndex(deviceIndex);
        if (result == default)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static new GameController? FromInstanceId(int instanceId, ErrorHandler errorHandler)
    {
        nint gameControllerHandle = Sdl.GameControllerFromInstanceId(instanceId);
        if (gameControllerHandle != nint.Zero)
        {
            return new GameController(gameControllerHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static new GameController? FromPlayerIndex(int playerIndex, ErrorHandler errorHandler)
    {
        nint joystickHandle = Sdl.GameControllerFromPlayerIndex(playerIndex);
        if (joystickHandle != nint.Zero)
        {
            return new GameController(joystickHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static new bool QueryIfEventIsEnabled(ErrorCodeHandler errorHandler)
    {
        int result = Sdl.GameControllerEventState(Sdl.Query);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return IntBoolMarshaller.ConvertToManaged(result);
    }

    public static new void EnableEvent(bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerEventState(enabled ? Sdl.Enable : Sdl.Disable);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static new void Update()
    {
        Sdl.GameControllerUpdate();
    }

    public GameControllerMapping GetMapping(ErrorHandler errorHandler)
    {
        GameControllerMapping result = Sdl.GameControllerMapping(this);
        if (result == default)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public override string? GetName(ErrorHandler errorHandler)
    {
        string? result = Sdl.GameControllerName(this);
        if (result is not null)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public override string? GetPath(ErrorHandler errorHandler)
    {
        string? result = Sdl.GameControllerPath(this);
        if (result is not null)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public SteamSafeHandle? GetSteamHandle(ErrorHandler errorHandler)
    {
        nuint steamHandle = new(Sdl.GameControllerGetSteamHandle(this));
        if (steamHandle == nuint.Zero)
        {
            errorHandler(Sdl.GetError());
            return null;
        }

        unsafe
        {
            nint convertedHandle = new(steamHandle.ToPointer());
            return new SteamSafeHandle(convertedHandle, true);
        }
    }

    public override bool IsAttached(ErrorHandler errorHandler)
    {
        bool result = Sdl.GameControllerGetAttached(this);
        if (result)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return result;
    }

    public Joystick? GetJoystick(ErrorHandler errorHandler)
    {
        nint joystickHandle = Sdl.GameControllerGetJoystick(this);
        if (joystickHandle != nint.Zero)
        {
            return new Joystick(joystickHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public GameControllerButtonBind GetBindForAxis(GameControllerAxis axis)
    {
        Sdl.SdlGameControllerButtonBind native = Sdl.GameControllerGetBindForAxis(this, axis);
        return new GameControllerButtonBind
        {
            BindType = native.BindType,
            Value = new GameControllerButtonBindUnion
            {
                Hat = new GameControllerButtonBindHat
                {
                    Hat = native.Val0, HatMask = native.Val1
                }
            }
        };
    }

    public bool HasAxis(GameControllerAxis axis)
    {
        return Sdl.GameControllerHasAxis(this, axis);
    }

    public short GetAxisValue(GameControllerAxis axis, ErrorHandler errorHandler)
    {
        short result = Sdl.GameControllerGetAxis(this, axis);
        if (result == 0)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public GameControllerButtonBind GetBindForButton(GameControllerButton button)
    {
        Sdl.SdlGameControllerButtonBind native = Sdl.GameControllerGetBindForButton(this, button);
        return new GameControllerButtonBind
        {
            BindType = native.BindType,
            Value = new GameControllerButtonBindUnion
            {
                Hat = new GameControllerButtonBindHat
                {
                    Hat = native.Val0, HatMask = native.Val1
                }
            }
        };
    }

    public bool HasButton(GameControllerButton button)
    {
        return Sdl.GameControllerHasButton(this, button);
    }

    public ButtonState GetButtonState(GameControllerButton button, ErrorHandler errorHandler)
    {
        ButtonState result = Sdl.GameControllerGetButton(this, button);
        if (result == ButtonState.Released)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public int CountTouchpadFingers(int touchpadIndex)
    {
        return Sdl.GameControllerGetNumTouchpadFingers(this, touchpadIndex);
    }

    public TouchpadFingerState GetTouchpadFingerState(
        int touchpadIndex,
        int fingerIndex,
        ErrorCodeHandler errorHandler
    )
    {
        int code = Sdl.GameControllerGetTouchpadFinger(
            this,
            touchpadIndex,
            fingerIndex,
            out FingerState state,
            out float x,
            out float y,
            out float pressure
        );

        if (code >= 0)
        {
            return new TouchpadFingerState
            {
                State = state, Position = new PointF(x, y), Pressure = pressure
            };
        }

        errorHandler(Sdl.GetError(), code);
        return default;
    }

    public bool HasSensor(SensorType type)
    {
        return Sdl.GameControllerHasSensor(this, type);
    }

    public void SetSensorEnabled(SensorType type, bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerSetSensorEnabled(this, type, enabled);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public bool IsSensorEnabled(SensorType type)
    {
        return Sdl.GameControllerIsSensorEnabled(this, type);
    }

    public float GetSensorDataRate(SensorType type, ErrorHandler errorHandler)
    {
        float result = Sdl.GameControllerGetSensorDataRate(this, type);
        if (result <= 0F)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public float[]? GetSensorData(
        SensorType type,
        int numberOfDataItems,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfDataItems * sizeof(float)));
            try
            {
                int code = Sdl.GameControllerGetSensorData(this, type, dataPtr, numberOfDataItems);
                if (code >= 0)
                {
                    Span<float> data = new(dataPtr, numberOfDataItems);
                    return data.ToArray();
                }

                errorHandler(Sdl.GetError(), code);
                return null;
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public FullSensorData GetFullSensorData(
        SensorType type,
        int numberOfDataItems,
        ErrorCodeHandler errorHandler
    )
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfDataItems * sizeof(float)));
            try
            {
                int code = Sdl.GameControllerGetSensorDataWithTimestamp(
                    this,
                    type,
                    out ulong timeStamp,
                    dataPtr,
                    numberOfDataItems
                );
                Span<float> data = dataPtr is null
                    ? Span<float>.Empty
                    : new Span<float>(dataPtr, numberOfDataItems);
                FullSensorData result =
                    new()
                    {
                        TimeStamp = TimeSpan.FromMicroseconds(timeStamp),
                        Data = data.IsEmpty ? null : data.ToArray()
                    };
                if (code >= 0)
                {
                    return result;
                }

                errorHandler(Sdl.GetError(), code);
                return result;
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public override void Rumble(
        RumbleFrequency frequency,
        TimeSpan time,
        ErrorCodeHandler errorHandler
    )
    {
        int code = Sdl.GameControllerRumble(
            this,
            frequency.Low,
            frequency.High,
            (uint)time.Milliseconds
        );

        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public override void RumbleTriggers(
        RumbleFrequency frequency,
        TimeSpan time,
        ErrorCodeHandler errorHandler
    )
    {
        int code = Sdl.GameControllerRumbleTriggers(
            this,
            frequency.Low,
            frequency.High,
            (uint)time.Milliseconds
        );

        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public override void SetLed(Color color, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerSetLed(this, color.R, color.G, color.B);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public override void SendEffect(byte[] data, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.GameControllerSendEffect(this, data, data.Length);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public string? GetAppleSfSymbolsNameForButton(
        GameControllerButton button,
        ErrorHandler errorHandler
    )
    {
        string? result = Sdl.GameControllerGetAppleSfSymbolsNameForButton(this, button);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public string? GetAppleSfSymbolsNameForAxis(GameControllerAxis axis, ErrorHandler errorHandler)
    {
        string? result = Sdl.SDL_GameControllerGetAppleSfSymbolsNameForAxis(this, axis);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.GameControllerClose(handle);
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

        return Equals((GameController)obj);
    }

    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(Type);
        code.Add(UsbIdInformation);
        code.Add(VersionInformation);
        code.Add(Serial);
        code.Add(CountTouchpads);
        code.Add(HasLed);
        code.Add(HasRumble);
        code.Add(HasRumbleTriggers);
        code.Add(base.GetHashCode());
        return code.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Type: {Type}, Player Index: {PlayerIndex}, USB ID Information: {UsbIdInformation}, Version Information: {VersionInformation}, Serial: {Serial}, Touchpads Count: {CountTouchpads}, Has LED: {HasLed}, Has Rumble: {HasRumble}, Has Rumble Triggers: {HasRumbleTriggers}}}";
    }

    public static bool operator ==(GameController? left, GameController? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GameController? left, GameController? right)
    {
        return !Equals(left, right);
    }
}
