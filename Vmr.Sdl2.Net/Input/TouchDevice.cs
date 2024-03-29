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

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.TouchUtilities;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input;

[Serializable]
public class TouchDevice : IEquatable<TouchDevice>
{
    public TouchDevice(int deviceIndex)
    {
        Id = Sdl.GetTouchDevice(deviceIndex);
        if (Id <= 0)
        {
            throw new TouchDeviceException($"Unable to open the touch device index {deviceIndex}");
        }
    }

    public static int Count => Sdl.GetNumTouchDevices();

    public long Id { get; }
    public TouchDeviceType Type => Sdl.GetTouchDeviceType(Id);
    public int CountFingers => Sdl.GetNumTouchFingers(Id);

    public bool Equals(TouchDevice? other)
    {
        return other is not null && Type == other.Type && CountFingers == other.CountFingers;
    }

    public static string GetNameForDevice(int deviceIndex)
    {
        string? result = Sdl.GetTouchName(deviceIndex);
        if (result is null)
        {
            throw new TouchDeviceException(
                $"Unable to get touch device name for index {deviceIndex}"
            );
        }

        return result;
    }

    public static int SaveAllDollarTemplates(RwOps dst)
    {
        int result = Sdl.SaveAllDollarTemplates(dst);
        if (result == 0)
        {
            throw new TouchDeviceException(
                "Unable to save all the dollar templates to the given stream"
            );
        }

        return result;
    }

    public Finger GetFinger(int fingerIndex)
    {
        Finger result = Sdl.GetTouchFinger(Id, fingerIndex);
        if (result == default)
        {
            throw new TouchDeviceException(
                $"Unable to get the touch device finger index {fingerIndex}"
            );
        }

        return result;
    }

    public void RecordGesture()
    {
        int code = Sdl.RecordGesture(Id);
        if (code < 0)
        {
            throw new TouchDeviceException("Unable to record the touch device gesture", code);
        }
    }

    public void SaveDollarTemplate(RwOps dst)
    {
        bool isValid = Sdl.SaveDollarTemplate(Id, dst);
        if (!isValid)
        {
            throw new TouchDeviceException(
                "Unable to save the touch device dollar template to the given stream"
            );
        }
    }

    public int LoadDollarTemplates(RwOps src)
    {
        int result = Sdl.LoadDollarTemplates(Id, src);
        if (result <= 0)
        {
            throw new TouchDeviceException(
                "Unable to load the touch device dollar templates from the given stream"
            );
        }

        return result;
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

        return Equals((TouchDevice)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Type);
    }

    public override string ToString()
    {
        return $"{{ID: {Id}, Type: {Type} Fingers: {CountFingers}}}";
    }

    public static bool operator ==(TouchDevice? left, TouchDevice? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TouchDevice? left, TouchDevice? right)
    {
        return !Equals(left, right);
    }
}
