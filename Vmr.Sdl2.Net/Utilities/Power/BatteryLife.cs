// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Utilities.Power;

public struct BatteryLife
{
    public int SecondsLeft { get; internal init; }
    public int PercentageLeft { get; internal init; }
}
