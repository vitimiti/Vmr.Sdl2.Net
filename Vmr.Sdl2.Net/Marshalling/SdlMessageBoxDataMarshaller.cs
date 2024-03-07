// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Video.Messages;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(MessageBoxData),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
internal static unsafe class SdlMessageBoxDataMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlMessageBoxButtonData
    {
        public MessageBoxButtonOptions Flags;
        public int ButtonId;
        public byte* Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlMessageBoxColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlMessageBoxColorScheme
    {
        public nint Colors;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlMessageBoxData
    {
        public MessageBoxOptions Flags;
        public nint Window;
        public byte* Title;
        public byte* Message;
        public int NumButtons;
        public SdlMessageBoxButtonData* Buttons;
        public SdlMessageBoxColorScheme* colorScheme;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlMessageBoxData* _unmanagedPtr;
        private SdlMessageBoxData _unmanaged;
        private GCHandle _gcHandle;
        private GCHandle _internalGcHandle;

        public void FromManaged(MessageBoxData managed)
        {
            SdlMessageBoxButtonData[] sdlButtons = new SdlMessageBoxButtonData[
                managed.Buttons.Length
            ];
            for (int i = 0; i < managed.Buttons.Length; i++)
            {
                sdlButtons[i] = new SdlMessageBoxButtonData
                {
                    Flags = managed.Buttons[i].Flags,
                    ButtonId = managed.Buttons[i].ButtonId,
                    Text = Utf8StringMarshaller.ConvertToUnmanaged(managed.Buttons[i].Text)
                };
            }

            SdlMessageBoxColor[] colors = new SdlMessageBoxColor[5];
            colors[0] = new SdlMessageBoxColor
            {
                R = managed.ColorScheme.Background.R,
                G = managed.ColorScheme.Background.G,
                B = managed.ColorScheme.Background.B
            };

            colors[1] = new SdlMessageBoxColor
            {
                R = managed.ColorScheme.Text.R,
                G = managed.ColorScheme.Text.G,
                B = managed.ColorScheme.Text.B
            };

            colors[2] = new SdlMessageBoxColor
            {
                R = managed.ColorScheme.ButtonBorder.R,
                G = managed.ColorScheme.ButtonBorder.G,
                B = managed.ColorScheme.ButtonBorder.B
            };

            colors[3] = new SdlMessageBoxColor
            {
                R = managed.ColorScheme.ButtonBackground.R,
                G = managed.ColorScheme.ButtonBackground.G,
                B = managed.ColorScheme.ButtonBackground.B
            };

            colors[4] = new SdlMessageBoxColor
            {
                R = managed.ColorScheme.ButtonSelected.R,
                G = managed.ColorScheme.ButtonSelected.G,
                B = managed.ColorScheme.ButtonSelected.B
            };

            fixed (SdlMessageBoxColor* colorsPtr = colors)
            fixed (SdlMessageBoxButtonData* sdlButtonsPtr = sdlButtons)
            {
                SdlMessageBoxColorScheme sdlColorScheme = new() { Colors = (nint)colorsPtr };
                _internalGcHandle = GCHandle.Alloc(sdlColorScheme, GCHandleType.Pinned);
                _unmanaged = new SdlMessageBoxData
                {
                    Flags = managed.Flags,
                    Window = managed.Parent?.DangerousGetHandle() ?? nint.Zero,
                    Title = Utf8StringMarshaller.ConvertToUnmanaged(managed.Title),
                    Message = Utf8StringMarshaller.ConvertToUnmanaged(managed.Message),
                    NumButtons = managed.Buttons.Length,
                    Buttons = sdlButtonsPtr,
                    colorScheme = &sdlColorScheme
                };
            }

            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlMessageBoxData* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlMessageBoxData* ToUnmanaged()
        {
            return _unmanagedPtr;
        }

        public void Free()
        {
            if (_unmanagedPtr is not null)
            {
                _unmanagedPtr = null;
            }

            if (!_gcHandle.IsAllocated)
            {
                return;
            }

            _internalGcHandle.Free();
            _gcHandle.Free();
        }
    }
}
