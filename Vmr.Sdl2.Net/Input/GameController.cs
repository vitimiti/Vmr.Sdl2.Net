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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.CommonUtilities;
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

    public GameController(int deviceIndex)
        : base(InitializeBaseFromDevice(deviceIndex), true)
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
    public override bool IsAttached => Sdl.GameControllerGetAttached(this);

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

    private static nint InitializeBaseFromDevice(int deviceIndex)
    {
        nint deviceHandle = Sdl.GameControllerOpen(deviceIndex);
        if (deviceHandle == nint.Zero)
        {
            throw new GameControllerException(
                $"Unable to open game controller at index {deviceIndex}"
            );
        }

        return deviceHandle;
    }

    public static void AddMappings(RwOps rwOps)
    {
        int code = Sdl.GameControllerAddMappingsFromRw(rwOps, false);
        if (code < 0)
        {
            throw new GameControllerException("Unable to add the given mappings", code);
        }
    }

    public static void AddMapping(GameControllerMapping mapping)
    {
        int code = Sdl.GameControllerAddMapping(mapping);
        if (code < 0)
        {
            throw new GameControllerException($"Unable to add the mapping {mapping}", code);
        }
    }

    public static GameControllerMapping GetMappingForIndex(int mappingIndex)
    {
        GameControllerMapping result = Sdl.GameControllerMappingForIndex(mappingIndex);
        if (result == default)
        {
            throw new GameControllerException(
                $"Unable to get the mapping for the index {mappingIndex}"
            );
        }

        return result;
    }

    public static GameControllerMapping GetMappingForDevice(Guid deviceGuid)
    {
        GameControllerMapping result = Sdl.GameControllerMappingForGuid(deviceGuid);
        if (result == default)
        {
            throw new GameControllerException(
                $"Unable to get the mapping for the device with GUID {deviceGuid}"
            );
        }

        return result;
    }

    public static new string GetNameForDevice(int deviceIndex)
    {
        string? result = Sdl.GameControllerNameForIndex(deviceIndex);
        if (result is null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller name for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static new string GetPathForDevice(int deviceIndex)
    {
        string? result = Sdl.GameControllerPathForIndex(deviceIndex);
        if (result is null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller path for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static new GameControllerType GetTypeForDevice(int deviceIndex)
    {
        return Sdl.GameControllerTypeForIndex(deviceIndex);
    }

    public static GameControllerMapping GetMappingForDevice(int deviceIndex)
    {
        GameControllerMapping result = Sdl.GameControllerMappingForIndex(deviceIndex);
        if (result == default)
        {
            throw new GameControllerException(
                $"Unable to get the game controller mapping for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static new GameController FromInstanceId(int instanceId)
    {
        nint gameControllerHandle = Sdl.GameControllerFromInstanceId(instanceId);
        if (gameControllerHandle == nint.Zero)
        {
            throw new GameControllerException(
                $"Unable to get the game controller from instance ID {instanceId}"
            );
        }

        return new GameController(gameControllerHandle, true);
    }

    public static new GameController FromPlayerIndex(int playerIndex)
    {
        nint joystickHandle = Sdl.GameControllerFromPlayerIndex(playerIndex);
        if (joystickHandle == nint.Zero)
        {
            throw new GameControllerException(
                $"Unable to get the game controller from player index {playerIndex}"
            );
        }

        return new GameController(joystickHandle, true);
    }

    public static new bool QueryIfEventIsEnabled()
    {
        int result = Sdl.GameControllerEventState(Sdl.Query);
        if (result < 0)
        {
            throw new GameControllerException("Unable to query game controller event state");
        }

        return IntBoolMarshaller.ConvertToManaged(result);
    }

    public static new void EnableEvent(bool enabled)
    {
        int code = Sdl.GameControllerEventState(enabled ? Sdl.Enable : Sdl.Disable);
        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to set the game controller event state to {(enabled ? "enabled" : "disabled")}"
            );
        }
    }

    public static new void Update()
    {
        Sdl.GameControllerUpdate();
    }

    public GameControllerMapping GetMapping()
    {
        GameControllerMapping result = Sdl.GameControllerMapping(this);
        if (result == default)
        {
            throw new GameControllerException("Unable to get the game controller mapping");
        }

        return result;
    }

    public override string GetName()
    {
        string? result = Sdl.GameControllerName(this);
        if (result is null)
        {
            throw new GameControllerException("Unable to get the game controller name");
        }

        return result;
    }

    public override string GetPath()
    {
        string? result = Sdl.GameControllerPath(this);
        if (result is null)
        {
            throw new GameControllerException("Unable to get the game controller path");
        }

        return result;
    }

    public SteamSafeHandle GetSteamHandle()
    {
        nuint steamHandle = new(Sdl.GameControllerGetSteamHandle(this));
        if (steamHandle == nuint.Zero)
        {
            throw new GameControllerException("Unable to get the steam handle");
        }

        unsafe
        {
            nint convertedHandle = new(steamHandle.ToPointer());
            return new SteamSafeHandle(convertedHandle, true);
        }
    }

    public Joystick GetJoystick()
    {
        nint joystickHandle = Sdl.GameControllerGetJoystick(this);
        if (joystickHandle == nint.Zero)
        {
            throw new GameControllerException(
                "Unable to get the game controller joystick base class"
            );
        }

        return new Joystick(joystickHandle, true);
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

    public short GetAxisValue(GameControllerAxis axis)
    {
        short result = Sdl.GameControllerGetAxis(this, axis);
        if (result == 0 && Sdl.GetError() is not null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller axis {axis} value"
            );
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

    public ButtonState GetButtonState(GameControllerButton button)
    {
        ButtonState result = Sdl.GameControllerGetButton(this, button);
        if (result == ButtonState.Released && Sdl.GetError() is not null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller button {button} state"
            );
        }

        return result;
    }

    public int CountTouchpadFingers(int touchpadIndex)
    {
        return Sdl.GameControllerGetNumTouchpadFingers(this, touchpadIndex);
    }

    public TouchpadFingerState GetTouchpadFingerState(int touchpadIndex, int fingerIndex)
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

        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to get the touchpad {touchpadIndex} finger {fingerIndex} state",
                code
            );
        }

        return new TouchpadFingerState
        {
            State = state, Position = new PointF(x, y), Pressure = pressure
        };
    }

    public bool HasSensor(SensorType type)
    {
        return Sdl.GameControllerHasSensor(this, type);
    }

    public void SetSensorEnabled(SensorType type, bool enabled)
    {
        int code = Sdl.GameControllerSetSensorEnabled(this, type, enabled);
        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to set the game controller sensor {type} as {(enabled ? "enabled" : "disabled")}",
                code
            );
        }
    }

    public bool IsSensorEnabled(SensorType type)
    {
        return Sdl.GameControllerIsSensorEnabled(this, type);
    }

    public float GetSensorDataRate(SensorType type)
    {
        float result = Sdl.GameControllerGetSensorDataRate(this, type);
        if (result <= 0F)
        {
            throw new GameControllerException(
                $"Unable to get the game controller sensor {type} data rate"
            );
        }

        return result;
    }

    public float[] GetSensorData(SensorType type, int numberOfDataItems)
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfDataItems * sizeof(float)));
            try
            {
                int code = Sdl.GameControllerGetSensorData(this, type, dataPtr, numberOfDataItems);
                if (code < 0)
                {
                    throw new GameControllerException(
                        $"Unable to get the game controller sensor {type} data",
                        code
                    );
                }

                Span<float> data = new(dataPtr, numberOfDataItems);
                return data.ToArray();
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public FullSensorData GetFullSensorData(SensorType type, int numberOfDataItems)
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

                if (code < 0)
                {
                    throw new GameControllerException(
                        $"Unable to get the game controller sensor {type} full data",
                        code
                    );
                }

                return result;
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public override void Rumble(RumbleFrequency frequency, TimeSpan time)
    {
        int code = Sdl.GameControllerRumble(
            this,
            frequency.Low,
            frequency.High,
            (uint)time.Milliseconds
        );

        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to rumble the game controller with frequency {frequency} for {time.Milliseconds}ms",
                code
            );
        }
    }

    public override void RumbleTriggers(RumbleFrequency frequency, TimeSpan time)
    {
        int code = Sdl.GameControllerRumbleTriggers(
            this,
            frequency.Low,
            frequency.High,
            (uint)time.Milliseconds
        );

        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to rumble the game controller triggers with frequency {frequency} for {time.Milliseconds}ms",
                code
            );
        }
    }

    public override void SetLed(Color color)
    {
        int code = Sdl.GameControllerSetLed(this, color.R, color.G, color.B);
        if (code < 0)
        {
            throw new GameControllerException(
                $"Unable to set the game controller LED color to {color}",
                code
            );
        }
    }

    public override void SendEffect(byte[] data)
    {
        int code = Sdl.GameControllerSendEffect(this, data, data.Length);
        if (code < 0)
        {
            throw new GameControllerException(
                "Unable to send the given effect data to the game controller",
                code
            );
        }
    }

    public string GetAppleSfSymbolsNameForButton(GameControllerButton button)
    {
        string? result = Sdl.GameControllerGetAppleSfSymbolsNameForButton(this, button);
        if (result is null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller Apple SF symbols name for button {button}"
            );
        }

        return result;
    }

    public string GetAppleSfSymbolsNameForAxis(GameControllerAxis axis)
    {
        string? result = Sdl.SDL_GameControllerGetAppleSfSymbolsNameForAxis(this, axis);
        if (result is null)
        {
            throw new GameControllerException(
                $"Unable to get the game controller Apple SF symbols name for axis {axis}"
            );
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
