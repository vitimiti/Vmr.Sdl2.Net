// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.SensorUtilities;

public readonly struct FullState
{
    public TimeSpan TimeStamp { get; internal init; }
    public float[]? Data { get; internal init; }
}
