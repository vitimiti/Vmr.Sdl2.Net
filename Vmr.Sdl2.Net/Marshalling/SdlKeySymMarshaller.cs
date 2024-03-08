// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Input;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class SdlKeySymMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlKeySym
    {
        public ScanCode ScanCode;
        public KeyCode Sym;
        public KeyModifiers Modifiers;
        public uint Unused;
    }
}
