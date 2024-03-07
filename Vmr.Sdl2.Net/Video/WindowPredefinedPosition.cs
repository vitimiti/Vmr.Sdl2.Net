// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Video;

public readonly struct WindowPredefinedPosition
{
    public const uint UndefinedMask = 0x1FFF0000U;
    public const uint CenteredMask = 0x2FFF0000u;

    public static readonly int Undefined = UndefinedDisplay(0);
    public static readonly int Centered = CenteredDisplay(0);

    public static int UndefinedDisplay(int displayIndex)
    {
        return (int)(UndefinedMask | displayIndex);
    }

    public static int CenteredDisplay(int displayIndex)
    {
        return (int)(CenteredMask | displayIndex);
    }

    public static bool IsUndefined(int position)
    {
        return (position & 0xFFFF0000) == UndefinedMask;
    }

    public static bool IsCentered(int position)
    {
        return (position & 0xFFFF0000) == CenteredMask;
    }
}
