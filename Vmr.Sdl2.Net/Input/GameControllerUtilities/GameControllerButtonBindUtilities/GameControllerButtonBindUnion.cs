// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerButtonBindUtilities;

[StructLayout(LayoutKind.Explicit)]
public struct GameControllerButtonBindUnion
{
    [FieldOffset(0)]
    public int Button;

    [FieldOffset(0)]
    public int Axis;

    [FieldOffset(0)]
    public GameControllerButtonBindHat Hat;
}
