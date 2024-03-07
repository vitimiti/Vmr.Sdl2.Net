// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Video;

[Serializable]
[NativeMarshalling(typeof(SafeHandleMarshaller<DisplayMode>))]
public class DisplayMode : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<DisplayMode>
{
    private int _driverDataLength;
    private GCHandle _gcHandle;

    internal DisplayMode(Sdl.DisplayMode unmanaged)
        : base(true)
    {
        _gcHandle = GCHandle.Alloc(unmanaged, GCHandleType.Pinned);
        handle = _gcHandle.AddrOfPinnedObject();
    }

    private unsafe Sdl.DisplayMode* UnsafeHandle => (Sdl.DisplayMode*)handle;

    public uint Format
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->Format;
            }
        }
        set
        {
            unsafe
            {
                UnsafeHandle->Format = value;
            }
        }
    }

    public Size Size
    {
        get
        {
            unsafe
            {
                return new Size(UnsafeHandle->W, UnsafeHandle->H);
            }
        }
        set
        {
            unsafe
            {
                UnsafeHandle->W = value.Width;
                UnsafeHandle->H = value.Height;
            }
        }
    }

    public int RefreshRate
    {
        get
        {
            unsafe
            {
                return UnsafeHandle->RefreshRate;
            }
        }
        set
        {
            unsafe
            {
                UnsafeHandle->RefreshRate = value;
            }
        }
    }

    public byte[]? DriverData
    {
        get
        {
            unsafe
            {
                byte* driverData = (byte*)UnsafeHandle->DriverData;

                if (driverData is null)
                {
                    return null;
                }

                if (_driverDataLength == 0)
                {
                    return Array.Empty<byte>();
                }

                byte[] result = new byte[_driverDataLength];
                for (int i = 0; i < _driverDataLength; i++)
                {
                    result[i] = driverData[i];
                }

                return result;
            }
        }
        set
        {
            unsafe
            {
                if (value is null)
                {
                    _driverDataLength = 0;
                    UnsafeHandle->DriverData = null;
                    return;
                }

                _driverDataLength = value.Length;
                fixed (byte* driverDataHandle = value)
                {
                    UnsafeHandle->DriverData = driverDataHandle;
                }
            }
        }
    }

    public bool Equals(DisplayMode? other)
    {
        return other is not null
               && Format == other.Format
               && Size == other.Size
               && RefreshRate == other.RefreshRate;
    }

    protected override bool ReleaseHandle()
    {
        if (_gcHandle.IsAllocated)
        {
            _gcHandle.Free();
        }

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

        return Equals((DisplayMode)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Format, Size, RefreshRate);
    }

    public override string ToString()
    {
        return
            $"{{Format: {Format}, Size: {Size}, Refresh Rate: {RefreshRate}, Driver Data: [{string.Join(", ", DriverData ?? Array.Empty<byte>())}]}}";
    }

    public static bool operator ==(DisplayMode? left, DisplayMode? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(DisplayMode? left, DisplayMode? right)
    {
        return !Equals(left, right);
    }
}
