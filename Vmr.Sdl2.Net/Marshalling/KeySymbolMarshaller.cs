// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

internal static class KeySymbolMarshaller
{
    public static Input.KeyboardUtilities.KeySymbol ConvertToManaged(KeySymbol unmanaged)
    {
        return new Input.KeyboardUtilities.KeySymbol
        {
            ScanCode = unmanaged.ScanCode,
            KeyCode = unmanaged.Sym,
            Modifiers = unmanaged.Modifiers
        };
    }

    public static KeySymbol ConvertToUnmanaged(Input.KeyboardUtilities.KeySymbol managed)
    {
        return new KeySymbol
        {
            ScanCode = managed.ScanCode,
            Sym = managed.KeyCode,
            Modifiers = managed.Modifiers,
            Unused = 0
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeySymbol
    {
        public ScanCode ScanCode;
        public KeyCode Sym;
        public KeyModifiers Modifiers;
        public uint Unused;
    }
}
