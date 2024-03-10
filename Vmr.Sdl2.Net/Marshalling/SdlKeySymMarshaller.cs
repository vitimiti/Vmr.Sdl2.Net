// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;

using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class SdlKeySymMarshaller
{
    public static KeySymbol ConvertToManaged(SdlKeySym unmanaged)
    {
        return new KeySymbol
        {
            ScanCode = unmanaged.ScanCode,
            KeyCode = unmanaged.Sym,
            Modifiers = unmanaged.Modifiers
        };
    }

    public static SdlKeySym ConvertToUnmanaged(KeySymbol managed)
    {
        return new SdlKeySym
        {
            ScanCode = managed.ScanCode,
            Sym = managed.KeyCode,
            Modifiers = managed.Modifiers,
            Unused = 0
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SdlKeySym
    {
        public ScanCode ScanCode;
        public KeyCode Sym;
        public KeyModifiers Modifiers;
        public uint Unused;
    }
}
