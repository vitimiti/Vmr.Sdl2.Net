// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.JoystickUtilities;

[Serializable]
public readonly struct AxisInitialState
{
    public bool HasInitialValue { get; internal init; }
    public short InitialValue { get; internal init; }
}
