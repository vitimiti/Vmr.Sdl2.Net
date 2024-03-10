// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerButtonBindUtilities;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[StructLayout(LayoutKind.Sequential)]
public struct GameControllerButtonBind
{
    public GameControllerBindType BindType { get; set; }
    public GameControllerButtonBindUnion Value { get; set; }
}
