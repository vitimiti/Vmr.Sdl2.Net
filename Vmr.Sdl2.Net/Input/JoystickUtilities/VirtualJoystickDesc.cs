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
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net. If
// not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

public delegate void VirtualJoystickUpdateFunc(byte[]? userData);

public delegate void VirtualJoystickSetPlayerIndexFunc(byte[]? userData, int playerIndex);

public delegate int VirtualJoystickRumbleFunc(byte[]? userData, RumbleFrequency rumbleFrequency);

public delegate int VirtualJoystickRumbleTriggersFunc(
    byte[]? userData,
    RumbleFrequency rumbleFrequency
);

public delegate int VirtualJoystickSetLedFunc(byte[]? userData, Color color);

public delegate int VirtualJoystickSendEffect(byte[]? userData, byte[] data);

[Serializable]
[NativeMarshalling(typeof(VirtualJoystickDescMarshaller))]
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

    public bool Equals(VirtualJoystickDesc other)
    {
        return System.Version == other.Version
               && System.Type == other.Type
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
        hashCode.Add(System.Version);
        hashCode.Add((int)System.Type);
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
        return hashCode.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"{{Version: {System.Version}, Type: {System.Type}, Number of Axes: {NumberOfAxes}, Number of Buttons: {NumberOfButtons}, Number of Hats: {NumberOfHats}, Vendor ID: {VendorId}, Product ID: {ProductId}, Button Mask: [{ButtonMask}], Axis Mask: [{AxisMask}], Name: {Name}";
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
