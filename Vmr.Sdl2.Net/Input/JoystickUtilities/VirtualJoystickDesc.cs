// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

public delegate void VirtualJoystickUpdateFunc(byte[]? userData);

public delegate void VirtualJoystickSetPlayerIndexFunc(byte[]? userData, int playerIndex);

public delegate int VirtualJoystickRumbleFunc(
    byte[]? userData,
    RumbleFrequency rumbleFrequency,
    ErrorCodeHandler errorHandler
);

public delegate int VirtualJoystickRumbleTriggersFunc(
    byte[]? userData,
    RumbleFrequency rumbleFrequency,
    ErrorCodeHandler errorHandler
);

public delegate int VirtualJoystickSetLedFunc(
    byte[]? userData,
    Color color,
    ErrorCodeHandler errorHandler
);

public delegate int VirtualJoystickSendEffect(
    byte[]? userData,
    byte[] data,
    ErrorCodeHandler errorHandler
);

[Serializable]
[NativeMarshalling(typeof(SdlVirtualJoystickDescMarshaller))]
public struct VirtualJoystickDesc : IEquatable<VirtualJoystickDesc>
{
    public required ushort Version { get; set; }
    public required JoystickType Type { get; set; }
    public required ushort NumberOfAxes { get; set; }
    public required ushort NumberOfButtons { get; set; }
    public required ushort NumberOfHats { get; set; }
    public required ushort VendorId { get; set; }
    public required ushort ProductId { get; set; }
    public required GameControllerButtons ButtonMask { get; set; }
    public required GameControllerAxes AxisMask { get; set; }
    public required string Name { get; set; }
    public byte[]? UserData { get; set; }
    public required VirtualJoystickUpdateFunc Update { get; set; }
    public required VirtualJoystickSetPlayerIndexFunc SetPlayerIndex { get; set; }
    public required VirtualJoystickRumbleFunc Rumble { get; set; }
    public required VirtualJoystickRumbleTriggersFunc RumbleTriggers { get; set; }
    public required VirtualJoystickSetLedFunc SetLed { get; set; }
    public required VirtualJoystickSendEffect SendEffect { get; set; }
    public required ErrorCodeHandler RumbleErrorHandler { get; set; }
    public required ErrorCodeHandler RumbleTriggersErrorHandler { get; set; }
    public required ErrorCodeHandler SetLedErrorHandler { get; set; }
    public required ErrorCodeHandler SendEffectErrorHandler { get; set; }

    public bool Equals(VirtualJoystickDesc other)
    {
        return Version == other.Version
               && Type == other.Type
               && NumberOfAxes == other.NumberOfAxes
               && NumberOfButtons == other.NumberOfButtons
               && NumberOfHats == other.NumberOfHats
               && VendorId == other.VendorId
               && ProductId == other.ProductId
               && ButtonMask == other.ButtonMask
               && AxisMask == other.AxisMask
               && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is VirtualJoystickDesc other && Equals(other);
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(Version);
        hashCode.Add((int)Type);
        hashCode.Add(NumberOfAxes);
        hashCode.Add(NumberOfButtons);
        hashCode.Add(NumberOfHats);
        hashCode.Add(VendorId);
        hashCode.Add(ProductId);
        hashCode.Add((int)ButtonMask);
        hashCode.Add((int)AxisMask);
        hashCode.Add(Name);
        hashCode.Add(UserData);
        hashCode.Add(Update);
        hashCode.Add(SetPlayerIndex);
        hashCode.Add(Rumble);
        hashCode.Add(RumbleTriggers);
        hashCode.Add(SetLed);
        hashCode.Add(SendEffect);
        hashCode.Add(RumbleErrorHandler);
        hashCode.Add(RumbleTriggersErrorHandler);
        hashCode.Add(SetLedErrorHandler);
        hashCode.Add(SendEffectErrorHandler);
        return hashCode.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Version: {Version}, Type: {Type}, Number of Axes: {NumberOfAxes}, Number of Buttons: {NumberOfButtons}, Number of Hats: {NumberOfHats}, Vendor ID: {VendorId}, Product ID: {ProductId}, Button Mask: [{ButtonMask}], Axis Mask: [{AxisMask}], Name: {Name}";
    }

    public static bool operator ==(VirtualJoystickDesc left, VirtualJoystickDesc right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(VirtualJoystickDesc left, VirtualJoystickDesc right)
    {
        return !left.Equals(right);
    }
}
