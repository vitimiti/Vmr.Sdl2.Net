// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerButtonBindUtilities;

[StructLayout(LayoutKind.Sequential)]
public struct GameControllerButtonBindHat
{
    public int Hat;
    public int HatMask;
}
