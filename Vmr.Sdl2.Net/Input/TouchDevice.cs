// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.TouchUtilities;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input;

[Serializable]
public class TouchDevice : IEquatable<TouchDevice>
{
    public TouchDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        Id = Sdl.GetTouchDevice(deviceIndex);
        if (Id <= 0)
        {
            errorHandler(Sdl.GetError());
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

    public static string? GetNameForDevice(int deviceIndex, ErrorHandler errorHandler)
    {
        string? result = Sdl.GetTouchName(deviceIndex);
        if (result is null)
        {
            errorHandler(Sdl.GetError());
        }

        return result;
    }

    public Finger GetFinger(int fingerIndex, ErrorHandler errorHandler)
    {
        Finger result = Sdl.GetTouchFinger(Id, fingerIndex);
        if (result == default)
        {
            errorHandler(Sdl.GetError());
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
