// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Marshalling;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Vmr.Sdl2.Net.Input;

public static class Keyboard
{
    public static Dictionary<ScanCode, bool> State
    {
        get
        {
            unsafe
            {
                // Do not free this pointer, it belongs to SDL2
                byte* statesPtr = Sdl.GetKeyboardState(out int numKeys);
                ReadOnlySpan<byte> states = new(statesPtr, numKeys);
                ScanCode[] scanCodes = new ScanCode[ScanCodeData.TotalCodes];
                Dictionary<ScanCode, bool> result = new();
                for (int i = 0; i < scanCodes.Length; i++)
                {
                    scanCodes[i] = (ScanCode)i;
                    result.Add(scanCodes[i], ByteBoolMarshaller.ConvertToManaged(states[i]));
                }

                return result;
            }
        }
    }

    public static KeyModifiers ModifiersState
    {
        get => Sdl.GetModState();
        set => Sdl.SetModState(value);
    }

    public static Window GetFocus()
    {
        nint handle = Sdl.GetKeyboardFocus();
        if (handle == nint.Zero)
        {
            throw new KeyboardException("Unable to get the keyboard focused window");
        }

        return new Window(handle, false);
    }

    public static void Reset()
    {
        Sdl.ResetKeyboard();
    }
}
