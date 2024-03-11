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

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(System.Drawing.Rectangle),
    MarshalMode.Default,
    typeof(RectangleMarshaller)
)]
[CustomMarshaller(
    typeof(System.Drawing.Rectangle),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.Rectangle),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(System.Drawing.Rectangle),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.Rectangle),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
[CustomMarshaller(typeof(System.Drawing.Rectangle), MarshalMode.ElementIn, typeof(ElementIn))]
internal static unsafe class RectangleMarshaller
{
    public static Rectangle ConvertToUnmanaged(System.Drawing.Rectangle managed)
    {
        return new Rectangle
        {
            X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
        };
    }

    public static System.Drawing.Rectangle ConvertToManaged(Rectangle unmanaged)
    {
        return new System.Drawing.Rectangle(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rectangle
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    public static class ElementIn
    {
        public static Rectangle ConvertToUnmanaged(System.Drawing.Rectangle managed)
        {
            return new Rectangle
            {
                X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
            };
        }

        public static System.Drawing.Rectangle ConvertToManaged(Rectangle unmanaged)
        {
            return new System.Drawing.Rectangle(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
        }

        public static void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private Rectangle* _unmanagedPtr;
        private Rectangle _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.Rectangle managed)
        {
            if (managed == System.Drawing.Rectangle.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new Rectangle
            {
                X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
            };

            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (Rectangle* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public Rectangle* ToUnmanaged()
        {
            return _unmanagedPtr;
        }

        public void Free()
        {
            if (_unmanagedPtr is not null)
            {
                _unmanagedPtr = null;
            }

            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }

    public ref struct ManagedToUnmanagedOut
    {
        private System.Drawing.Rectangle _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(Rectangle* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.Rectangle.Empty;
                return;
            }

            _managed = new System.Drawing.Rectangle(
                unmanaged->X,
                unmanaged->Y,
                unmanaged->W,
                unmanaged->H
            );

            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.Rectangle ToManaged()
        {
            return _managed;
        }

        public void Free()
        {
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }

    public ref struct UnmanagedToManagedIn
    {
        private System.Drawing.Rectangle _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(Rectangle* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.Rectangle.Empty;
                return;
            }

            _managed = new System.Drawing.Rectangle(
                unmanaged->X,
                unmanaged->Y,
                unmanaged->W,
                unmanaged->H
            );

            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.Rectangle ToManaged()
        {
            return _managed;
        }

        public void Free()
        {
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }

    public ref struct UnmanagedToManagedOut
    {
        private Rectangle* _unmanagedPtr;
        private Rectangle _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.Rectangle managed)
        {
            if (managed == System.Drawing.Rectangle.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new Rectangle
            {
                X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
            };

            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (Rectangle* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public Rectangle* ToUnmanaged()
        {
            return _unmanagedPtr;
        }

        public void Free()
        {
            if (_unmanagedPtr is not null)
            {
                _unmanagedPtr = null;
            }

            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }
}
