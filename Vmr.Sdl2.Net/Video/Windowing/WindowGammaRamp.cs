// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video.Windowing;

public struct WindowGammaRamp : IEquatable<WindowGammaRamp>
{
    private ushort[] _blue = new ushort[256];
    private ushort[] _green = new ushort[256];
    private ushort[] _red = new ushort[256];

    public WindowGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
    {
        if (red.Length != 256)
        {
            throw new ArgumentOutOfRangeException(
                nameof(red),
                $"The {nameof(red)} array must be exactly 256 items, but was instead {red.Length} items"
            );
        }

        if (green.Length != 256)
        {
            throw new ArgumentOutOfRangeException(
                nameof(red),
                $"The {nameof(green)} array must be exactly 256 items, but was instead {green.Length} items"
            );
        }

        if (blue.Length != 256)
        {
            throw new ArgumentOutOfRangeException(
                nameof(blue),
                $"The {nameof(blue)} array must be exactly 256 items, but was instead {blue.Length} items"
            );
        }

        _red = red;
        _green = green;
        _blue = blue;
    }

    public ushort[] Red
    {
        get => _red;
        set
        {
            if (value.Length != 256)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Red),
                    $"The {nameof(Red)} array must be exactly 256 items, but was instead {value.Length} items"
                );
            }

            _red = value;
        }
    }

    public ushort[] Green
    {
        get => _green;
        set
        {
            if (value.Length != 256)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Green),
                    $"The {nameof(Green)} array must be exactly 256 items, but was instead {value.Length} items"
                );
            }

            _green = value;
        }
    }

    public ushort[] Blue
    {
        get => _blue;
        set
        {
            if (value.Length != 256)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Blue),
                    $"The {nameof(Blue)} array must be exactly 256 items, but was instead {value.Length} items"
                );
            }

            _blue = value;
        }
    }

    public bool Equals(WindowGammaRamp other)
    {
        return _blue.Equals(other._blue) && _green.Equals(other._green) && _red.Equals(other._red);
    }

    public override bool Equals(object? obj)
    {
        return obj is WindowGammaRamp other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_blue, _green, _red);
    }

    public override string ToString()
    {
        return
            $"{{Red: [{string.Join(", ", Red)}], Green: [{string.Join(", ", Green)}], Blue: [{string.Join(", ", Blue)}]}}";
    }

    public static bool operator ==(WindowGammaRamp left, WindowGammaRamp right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WindowGammaRamp left, WindowGammaRamp right)
    {
        return !left.Equals(right);
    }
}
