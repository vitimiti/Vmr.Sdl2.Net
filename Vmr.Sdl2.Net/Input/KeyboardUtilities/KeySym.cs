// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.Input.KeyboardUtilities;

public readonly struct KeySym
{
    public ScanCode ScanCode { get; internal init; }
    public KeyCode Sym { get; internal init; }
    public KeyModifiers Modifiers { get; internal init; }
}
