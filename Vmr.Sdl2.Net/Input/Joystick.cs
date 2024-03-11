// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Input;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<Joystick>))]
public class Joystick : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<Joystick>
{
    internal Joystick(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Joystick(int deviceIndex)
        : base(true)
    {
        handle = Sdl.JoystickOpen(deviceIndex);
        if (handle == nint.Zero)
        {
            throw new JoystickException($"Unable to open the joystick device index {deviceIndex}");
        }
    }

    public Joystick(VirtualJoystick device)
        : base(true)
    {
        handle = Sdl.JoystickOpen(device.Index);
        if (handle == nint.Zero)
        {
            throw new JoystickException("Unable to open the virtual joystick");
        }
    }

    public static int Count => Sdl.NumJoysticks();

    public virtual int PlayerIndex
    {
        get => Sdl.JoystickGetPlayerIndex(this);
        set => Sdl.JoystickSetPlayerIndex(this, value);
    }

    public Guid Guid => Sdl.JoystickGetGuid(this);

    public virtual JoystickUsbId UsbIdInformation =>
        new() { Vendor = Sdl.JoystickGetVendor(this), Product = Sdl.JoystickGetProduct(this) };

    public virtual JoystickVersion VersionInformation =>
        new()
        {
            Product = Sdl.JoystickGetProductVersion(this),
            Firmware = Sdl.JoystickGetFirmwareVersion(this)
        };

    public virtual string? Serial => Sdl.JoystickGetSerial(this);
    public JoystickType Type => Sdl.JoystickGetType(this);

    public JoystickGuidInfo GuidInfo
    {
        get
        {
            Sdl.GetJoystickGuidInfo(
                Guid,
                out ushort vendor,
                out ushort product,
                out ushort version,
                out ushort crc16
            );

            return new JoystickGuidInfo
            {
                Vendor = vendor, Product = product, Version = version, Crc16 = crc16
            };
        }
    }

    public virtual bool HasLed => Sdl.JoystickHasLed(this);
    public virtual bool HasRumble => Sdl.JoystickHasRumble(this);
    public virtual bool HasRumbleTriggers => Sdl.JoystickHasRumbleTriggers(this);
    public JoystickPowerLevel CurrentPowerLevel => Sdl.JoystickCurrentPowerLevel(this);
    public virtual bool IsAttached => Sdl.JoystickGetAttached(this);

    public bool Equals(Joystick? other)
    {
        return other is not null
               && Guid == other.Guid
               && UsbIdInformation == other.UsbIdInformation
               && VersionInformation == other.VersionInformation
               && Serial == other.Serial
               && Type == other.Type
               && GuidInfo == other.GuidInfo
               && HasLed == other.HasLed
               && HasRumble == other.HasRumble
               && HasRumbleTriggers == other.HasRumbleTriggers;
    }

    public static void LockAll()
    {
        Sdl.LockJoysticks();
    }

    public static void UnlockAll()
    {
        Sdl.UnlockJoysticks();
    }

    public static int GetPlayerIndexForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDevicePlayerIndex(deviceIndex);
    }

    public static Guid GetGuidForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceGuid(deviceIndex);
    }

    public static ushort GetVendorForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceVendor(deviceIndex);
    }

    public static ushort GetProductForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceProduct(deviceIndex);
    }

    public static ushort GetProductVersionForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceProductVersion(deviceIndex);
    }

    public static JoystickType GetTypeForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceType(deviceIndex);
    }

    public static int GetInstanceIdForDevice(int deviceIndex)
    {
        return Sdl.JoystickGetDeviceInstanceId(deviceIndex);
    }

    public static bool IsVirtualDevice(int deviceIndex)
    {
        return Sdl.JoystickIsVirtual(deviceIndex);
    }

    public static string GetNameForDevice(int deviceIndex)
    {
        string? result = Sdl.JoystickNameForIndex(deviceIndex);
        if (result is null)
        {
            throw new JoystickException(
                $"Unable to get the joystick name for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static string GetPathForDevice(int deviceIndex)
    {
        string? result = Sdl.JoystickPathForIndex(deviceIndex);
        if (result is null)
        {
            throw new JoystickException(
                $"Unable to get the joystick path for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static Joystick FromInstanceId(int instanceId)
    {
        nint joystickHandle = Sdl.JoystickFromInstanceId(instanceId);
        if (joystickHandle == nint.Zero)
        {
            throw new JoystickException(
                $"Unable to get the joystick from the instance ID {instanceId}"
            );
        }

        return new Joystick(joystickHandle, true);
    }

    public static Joystick FromPlayerIndex(int playerIndex)
    {
        nint joystickHandle = Sdl.JoystickFromPlayerIndex(playerIndex);
        if (joystickHandle == nint.Zero)
        {
            throw new JoystickException(
                $"Unable to get the joystick from the player index {playerIndex}"
            );
        }

        return new Joystick(joystickHandle, true);
    }

    public static void Update()
    {
        Sdl.JoystickUpdate();
    }

    public static bool QueryIfEventIsEnabled()
    {
        int result = Sdl.JoystickEventState(Sdl.Query);
        if (result < 0)
        {
            throw new JoystickException("Unable to query if the joystick event is enabled");
        }

        return IntBoolMarshaller.ConvertToManaged(result);
    }

    public static void EnableEvent(bool enabled)
    {
        int code = Sdl.JoystickEventState(enabled ? Sdl.Enable : Sdl.Disable);
        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to set the joystick event state to {(enabled ? "enabled" : "disabled")}"
            );
        }
    }

    public static bool DeviceIsGameController(int deviceIndex)
    {
        return Sdl.IsGameController(deviceIndex);
    }

    public virtual string GetPath()
    {
        string? result = Sdl.JoystickPath(this);
        if (result is null)
        {
            throw new JoystickException("Unable to get the joystick path");
        }

        return result;
    }

    public void SetVirtualAxis(int axisIndex, short value)
    {
        int code = Sdl.JoystickSetVirtualAxis(this, axisIndex, value);
        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to set the virtual axis {axisIndex} to {value}",
                code
            );
        }
    }

    public void SetVirtualButton(int buttonIndex, ButtonState value)
    {
        int code = Sdl.JoystickSetVirtualButton(this, buttonIndex, value);
        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to set the virtual button {buttonIndex} to {value}",
                code
            );
        }
    }

    public void SetVirtualHat(int hatIndex, HatState value)
    {
        int code = Sdl.JoystickSetVirtualHat(this, hatIndex, value);
        if (code < 0)
        {
            throw new JoystickException($"Unable to set the hat {hatIndex} to [{value}]", code);
        }
    }

    public virtual string GetName()
    {
        string? result = Sdl.JoystickName(this);
        if (result is null)
        {
            throw new JoystickException("Unable to get the joystick name");
        }

        return result;
    }

    public int GetInstanceId()
    {
        int id = Sdl.JoystickInstanceId(this);
        if (id < 0)
        {
            throw new JoystickException("Unable to get the joystick instance ID");
        }

        return id;
    }

    public int GetNumberOfAxes()
    {
        int axes = Sdl.JoystickNumAxes(this);
        if (axes < 0)
        {
            throw new JoystickException("Unable to get the joystick number of axes");
        }

        return axes;
    }

    public int GetNumberOfBalls()
    {
        int axes = Sdl.JoystickNumBalls(this);
        if (axes < 0)
        {
            throw new JoystickException("Unable to get the joystick number of balls");
        }

        return axes;
    }

    public int GetNumberOfHats()
    {
        int axes = Sdl.JoystickNumHats(this);
        if (axes < 0)
        {
            throw new JoystickException("Unable to get the joystick number of hats");
        }

        return axes;
    }

    public int GetNumberOfButtons()
    {
        int axes = Sdl.JoystickNumButtons(this);
        if (axes < 0)
        {
            throw new JoystickException("Unable to get the joystick number of buttons");
        }

        return axes;
    }

    public short GetAxisValue(int axisIndex)
    {
        short value = Sdl.JoystickGetAxis(this, axisIndex);
        if (value == 0 && Sdl.GetError() != null)
        {
            throw new JoystickException(
                $"Unable to get the joystick axis value for index {axisIndex}"
            );
        }

        return value;
    }

    public AxisInitialState GetAxisInitialState(int axisIndex)
    {
        bool hasInitialValue = Sdl.JoystickGetAxisInitialState(this, axisIndex, out short state);
        return new AxisInitialState { HasInitialValue = hasInitialValue, InitialValue = state };
    }

    public HatPositions GetHatState(int hatIndex)
    {
        return Sdl.JoystickGetHat(this, hatIndex);
    }

    public Point GetBallAxisDelta(int ballIndex)
    {
        int code = Sdl.JoystickGetBall(this, ballIndex, out int dX, out int dY);
        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to get the joystick ball axis delta for index {ballIndex}",
                code
            );
        }

        return new Point(dX, dY);
    }

    public ButtonState GetButtonState(int buttonIndex)
    {
        return Sdl.JoystickGetButton(this, buttonIndex);
    }

    public virtual void Rumble(RumbleFrequency frequency, TimeSpan time)
    {
        int code = Sdl.JoystickRumble(this, frequency.Low, frequency.High, (uint)time.Milliseconds);
        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to set the joystick rumble frequency {frequency} for {time.Milliseconds}ms",
                code
            );
        }
    }

    public virtual void RumbleTriggers(RumbleFrequency frequency, TimeSpan time)
    {
        int code = Sdl.JoystickRumbleTriggers(
            this,
            frequency.Low,
            frequency.High,
            (uint)time.Milliseconds
        );

        if (code < 0)
        {
            throw new JoystickException(
                $"Unable to set the joystick triggers rumble frequency {frequency} for {time.Milliseconds}ms",
                code
            );
        }
    }

    public virtual void SetLed(Color color)
    {
        int code = Sdl.JoystickSetLed(this, color.R, color.G, color.B);
        if (code < 0)
        {
            throw new JoystickException($"Unable to set the joystick LED color to {color}", code);
        }
    }

    public virtual void SendEffect(byte[] data)
    {
        int code = Sdl.JoystickSendEffect(this, data, data.Length);
        if (code < 0)
        {
            throw new JoystickException("Unable to send the given effect data to the joystick");
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.JoystickClose(handle);
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

        return Equals((Joystick)obj);
    }

    public override int GetHashCode()
    {
        HashCode code = new();
        code.Add(PlayerIndex);
        code.Add(Guid);
        code.Add(UsbIdInformation);
        code.Add(VersionInformation);
        code.Add(Serial);
        code.Add(Type);
        code.Add(GuidInfo);
        code.Add(HasLed);
        code.Add(HasRumble);
        code.Add(HasRumbleTriggers);
        return code.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Player Index: {PlayerIndex}, Guid: {Guid}, USB ID Information: {UsbIdInformation}, Version Information: {VersionInformation}, Serial: {Serial}, Type: {Type}, GUID Info: {GuidInfo}, Has LED: {HasLed}, Has Rumble: {HasRumble}, Has Rumble Triggers: {HasRumbleTriggers}, Current Power Level: {CurrentPowerLevel}}}";
    }

    public static bool operator ==(Joystick? left, Joystick? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Joystick? left, Joystick? right)
    {
        return !Equals(left, right);
    }
}
