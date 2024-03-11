// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Video.Messages;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(Video.Messages.MessageBoxData),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
internal static unsafe class MessageBoxDataMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MessageBoxButtonData
    {
        public MessageBoxButtonOptions Flags;
        public int ButtonId;
        public byte* Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MessageBoxColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MessageBoxColorScheme
    {
        public nint Colors;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MessageBoxData
    {
        public MessageBoxOptions Flags;
        public nint Window;
        public byte* Title;
        public byte* Message;
        public int NumButtons;
        public MessageBoxButtonData* Buttons;
        public MessageBoxColorScheme* colorScheme;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private MessageBoxData* _unmanagedPtr;
        private MessageBoxData _unmanaged;
        private GCHandle _gcHandle;
        private GCHandle _internalGcHandle;

        public void FromManaged(Video.Messages.MessageBoxData managed)
        {
            MessageBoxButtonData[] sdlButtons = new MessageBoxButtonData[managed.Buttons.Length];

            for (int i = 0; i < managed.Buttons.Length; i++)
            {
                sdlButtons[i] = new MessageBoxButtonData
                {
                    Flags = managed.Buttons[i].Flags,
                    ButtonId = managed.Buttons[i].ButtonId,
                    Text = Utf8StringMarshaller.ConvertToUnmanaged(managed.Buttons[i].Text)
                };
            }

            MessageBoxColor[] colors = new MessageBoxColor[5];
            colors[0] = new MessageBoxColor
            {
                R = managed.ColorScheme.Background.R,
                G = managed.ColorScheme.Background.G,
                B = managed.ColorScheme.Background.B
            };

            colors[1] = new MessageBoxColor
            {
                R = managed.ColorScheme.Text.R,
                G = managed.ColorScheme.Text.G,
                B = managed.ColorScheme.Text.B
            };

            colors[2] = new MessageBoxColor
            {
                R = managed.ColorScheme.ButtonBorder.R,
                G = managed.ColorScheme.ButtonBorder.G,
                B = managed.ColorScheme.ButtonBorder.B
            };

            colors[3] = new MessageBoxColor
            {
                R = managed.ColorScheme.ButtonBackground.R,
                G = managed.ColorScheme.ButtonBackground.G,
                B = managed.ColorScheme.ButtonBackground.B
            };

            colors[4] = new MessageBoxColor
            {
                R = managed.ColorScheme.ButtonSelected.R,
                G = managed.ColorScheme.ButtonSelected.G,
                B = managed.ColorScheme.ButtonSelected.B
            };

            fixed (MessageBoxColor* colorsPtr = colors)
            fixed (MessageBoxButtonData* sdlButtonsPtr = sdlButtons)
            {
                MessageBoxColorScheme colorScheme = new() { Colors = (nint)colorsPtr };
                _internalGcHandle = GCHandle.Alloc(colorScheme, GCHandleType.Pinned);
                _unmanaged = new MessageBoxData
                {
                    Flags = managed.Flags,
                    Window = managed.Parent?.DangerousGetHandle() ?? nint.Zero,
                    Title = Utf8StringMarshaller.ConvertToUnmanaged(managed.Title),
                    Message = Utf8StringMarshaller.ConvertToUnmanaged(managed.Message),
                    NumButtons = managed.Buttons.Length,
                    Buttons = sdlButtonsPtr,
                    colorScheme = &colorScheme
                };
            }

            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (MessageBoxData* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public MessageBoxData* ToUnmanaged()
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
