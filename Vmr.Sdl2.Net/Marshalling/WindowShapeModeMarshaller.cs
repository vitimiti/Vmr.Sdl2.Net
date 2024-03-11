// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Video.Windowing.Shape;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(Video.Windowing.Shape.WindowShapeMode),
    MarshalMode.Default,
    typeof(WindowShapeModeMarshaller)
)]
[CustomMarshaller(
    typeof(Video.Windowing.Shape.WindowShapeMode),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(Video.Windowing.Shape.WindowShapeMode),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(Video.Windowing.Shape.WindowShapeMode),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(Video.Windowing.Shape.WindowShapeMode),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
internal static unsafe class WindowShapeModeMarshaller
{
    public static WindowShapeMode ConvertToUnmanaged(Video.Windowing.Shape.WindowShapeMode managed)
    {
        return new WindowShapeMode
        {
            Mode = managed.Mode,
            Parameters = new WindowShapeParams
            {
                BinarizationCutoff = managed.Parameters.BinarizationCutoff,
                ColorKey = ColorMarshaller.ConvertToUnmanaged(managed.Parameters.ColorKey)
            }
        };
    }

    public static Video.Windowing.Shape.WindowShapeMode ConvertToManaged(WindowShapeMode unmanaged)
    {
        return new Video.Windowing.Shape.WindowShapeMode
        {
            Mode = unmanaged.Mode,
            Parameters = new Video.Windowing.Shape.WindowShapeParams
            {
                BinarizationCutoff = unmanaged.Parameters.BinarizationCutoff,
                ColorKey = ColorMarshaller.ConvertToManaged(unmanaged.Parameters.ColorKey)
            }
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowShapeParams
    {
        public byte BinarizationCutoff;
        public ColorMarshaller.Color ColorKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowShapeMode
    {
        public ShapeMode Mode;
        public WindowShapeParams Parameters;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private WindowShapeMode* _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Video.Windowing.Shape.WindowShapeMode managed)
        {
            WindowShapeMode unmanaged =
                new()
                {
                    Mode = managed.Mode,
                    Parameters = new WindowShapeParams
                    {
                        BinarizationCutoff = managed.Parameters.BinarizationCutoff,
                        ColorKey =
                            ColorMarshaller.ConvertToUnmanaged(managed.Parameters.ColorKey)
                    }
                };

            _gcHandle = GCHandle.Alloc(unmanaged, GCHandleType.Pinned);
            _unmanaged = &unmanaged;
        }

        public WindowShapeMode* ToUnmanaged()
        {
            return _unmanaged;
        }

        public void Free()
        {
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }

    public ref struct ManagedToUnmanagedOut
    {
        private Video.Windowing.Shape.WindowShapeMode _managed;

        public void FromUnmanaged(WindowShapeMode* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = default;
                return;
            }

            _managed = new Video.Windowing.Shape.WindowShapeMode
            {
                Mode = unmanaged->Mode,
                Parameters = new Video.Windowing.Shape.WindowShapeParams
                {
                    BinarizationCutoff = unmanaged->Parameters.BinarizationCutoff,
                    ColorKey =
                        ColorMarshaller.ConvertToManaged(unmanaged->Parameters.ColorKey)
                }
            };
        }

        public Video.Windowing.Shape.WindowShapeMode ToManaged()
        {
            return _managed;
        }

        public void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct UnmanagedToManagedIn
    {
        private Video.Windowing.Shape.WindowShapeMode _managed;

        public void FromUnmanaged(WindowShapeMode* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = default;
                return;
            }

            _managed = new Video.Windowing.Shape.WindowShapeMode
            {
                Mode = unmanaged->Mode,
                Parameters = new Video.Windowing.Shape.WindowShapeParams
                {
                    BinarizationCutoff = unmanaged->Parameters.BinarizationCutoff,
                    ColorKey =
                        ColorMarshaller.ConvertToManaged(unmanaged->Parameters.ColorKey)
                }
            };
        }

        public Video.Windowing.Shape.WindowShapeMode ToManaged()
        {
            return _managed;
        }

        public void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct UnmanagedToManagedOut
    {
        private WindowShapeMode* _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Video.Windowing.Shape.WindowShapeMode managed)
        {
            WindowShapeMode unmanaged =
                new()
                {
                    Mode = managed.Mode,
                    Parameters = new WindowShapeParams
                    {
                        BinarizationCutoff = managed.Parameters.BinarizationCutoff,
                        ColorKey =
                            ColorMarshaller.ConvertToUnmanaged(managed.Parameters.ColorKey)
                    }
                };

            _gcHandle = GCHandle.Alloc(unmanaged, GCHandleType.Pinned);
            _unmanaged = &unmanaged;
        }

        public WindowShapeMode* ToUnmanaged()
        {
            return _unmanaged;
        }

        public void Free()
        {
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }
}
