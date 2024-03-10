// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.CommonUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

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

    public Joystick(int deviceIndex, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.JoystickOpen(deviceIndex);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
        }
    }

    public Joystick(VirtualJoystick device, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.JoystickOpen(device.Index);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
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
                Vendor = vendor,
                Product = product,
                Version = version,
                Crc16 = crc16
            };
        }
    }

    public virtual bool HasLed => Sdl.JoystickHasLed(this);
    public virtual bool HasRumble => Sdl.JoystickHasRumble(this);
    public virtual bool HasRumbleTriggers => Sdl.JoystickHasRumbleTriggers(this);
    public JoystickPowerLevel CurrentPowerLevel => Sdl.JoystickCurrentPowerLevel(this);

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

    public static string? GetNameForDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        string? result = Sdl.JoystickNameForIndex(deviceIndex);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static string? GetPathForDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        string? result = Sdl.JoystickPathForIndex(deviceIndex);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public static Joystick? FromInstanceId(int instanceId, ErrorHandler errorHandler)
    {
        nint joystickHandle = Sdl.JoystickFromInstanceId(instanceId);
        if (joystickHandle != nint.Zero)
        {
            return new Joystick(joystickHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static Joystick? FromPlayerIndex(int playerIndex, ErrorHandler errorHandler)
    {
        nint joystickHandle = Sdl.JoystickFromPlayerIndex(playerIndex);
        if (joystickHandle != nint.Zero)
        {
            return new Joystick(joystickHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static void Update()
    {
        Sdl.JoystickUpdate();
    }

    public static bool QueryIfEventIsEnabled(ErrorCodeHandler errorHandler)
    {
        int result = Sdl.JoystickEventState(Sdl.Query);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return IntBoolMarshaller.ConvertToManaged(result);
    }

    public static void EnableEvent(bool enabled, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickEventState(enabled ? Sdl.Enable : Sdl.Disable);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public static bool DeviceIsGameController(int deviceIndex)
    {
        return Sdl.IsGameController(deviceIndex);
    }

    public virtual string? GetPath(ErrorHandler errorHandler)
    {
        string? result = Sdl.JoystickPath(this);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public void SetVirtualAxis(int axisIndex, short value, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickSetVirtualAxis(this, axisIndex, value);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetVirtualButton(int buttonIndex, ButtonState value, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickSetVirtualButton(this, buttonIndex, value);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public void SetVirtualHat(int hatIndex, HatState value, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickSetVirtualHat(this, hatIndex, value);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public virtual string? GetName(ErrorHandler errorHandler)
    {
        string? result = Sdl.JoystickName(this);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public virtual bool IsAttached(ErrorHandler errorHandler)
    {
        bool result = Sdl.JoystickGetAttached(this);
        if (result)
        {
            return result;
        }

        errorHandler(Sdl.GetError());
        return result;
    }

    public int GetInstanceId(ErrorCodeHandler errorHandler)
    {
        int id = Sdl.JoystickInstanceId(this);
        if (id < 0)
        {
            errorHandler(Sdl.GetError(), id);
        }

        return id;
    }

    public int GetNumberOfAxes(ErrorCodeHandler errorHandler)
    {
        int axes = Sdl.JoystickNumAxes(this);
        if (axes < 0)
        {
            errorHandler(Sdl.GetError(), axes);
        }

        return axes;
    }

    public int GetNumberOfBalls(ErrorCodeHandler errorHandler)
    {
        int axes = Sdl.JoystickNumBalls(this);
        if (axes < 0)
        {
            errorHandler(Sdl.GetError(), axes);
        }

        return axes;
    }

    public int GetNumberOfHats(ErrorCodeHandler errorHandler)
    {
        int axes = Sdl.JoystickNumHats(this);
        if (axes < 0)
        {
            errorHandler(Sdl.GetError(), axes);
        }

        return axes;
    }

    public int GetNumberOfButtons(ErrorCodeHandler errorHandler)
    {
        int axes = Sdl.JoystickNumButtons(this);
        if (axes < 0)
        {
            errorHandler(Sdl.GetError(), axes);
        }

        return axes;
    }

    public short GetAxisValue(int axisIndex, ErrorHandler errorHandler)
    {
        short value = Sdl.JoystickGetAxis(this, axisIndex);
        if (value == 0)
        {
            errorHandler(Sdl.GetError());
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

    public Point GetBallAxisDelta(int ballIndex, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickGetBall(this, ballIndex, out int dX, out int dY);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }

        return new Point(dX, dY);
    }

    public ButtonState GetButtonState(int buttonIndex)
    {
        return Sdl.JoystickGetButton(this, buttonIndex);
    }

    public virtual void Rumble(
        RumbleFrequency frequency,
        TimeSpan time,
        ErrorCodeHandler errorHandler
    )
    {
        int code = Sdl.JoystickRumble(this, frequency.Low, frequency.High, (uint)time.Milliseconds);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public virtual void RumbleTriggers(
        RumbleFrequency frequency,
        TimeSpan time,
        ErrorCodeHandler errorHandler
    )
    {
        int code = Sdl.JoystickRumbleTriggers(
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

    public virtual void SetLed(Color color, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickSetLed(this, color.R, color.G, color.B);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
        }
    }

    public virtual void SendEffect(byte[] data, ErrorCodeHandler errorHandler)
    {
        int code = Sdl.JoystickSendEffect(this, data, data.Length);
        if (code < 0)
        {
            errorHandler(Sdl.GetError(), code);
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
        return $"{{Player Index: {PlayerIndex}, Guid: {Guid}, USB ID Information: {UsbIdInformation}, Version Information: {VersionInformation}, Serial: {Serial}, Type: {Type}, GUID Info: {GuidInfo}, Has LED: {HasLed}, Has Rumble: {HasRumble}, Has Rumble Triggers: {HasRumbleTriggers}, Current Power Level: {CurrentPowerLevel}}}";
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
