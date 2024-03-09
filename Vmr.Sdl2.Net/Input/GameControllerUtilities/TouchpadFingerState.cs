// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[Serializable]
public readonly struct TouchpadFingerState
{
    public FingerState State { get; internal init; }
    public PointF Position { get; internal init; }
    public float Pressure { get; internal init; }
}
