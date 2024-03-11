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
    typeof(System.Drawing.RectangleF),
    MarshalMode.Default,
    typeof(RectangleFMarshaller)
)]
[CustomMarshaller(
    typeof(System.Drawing.RectangleF),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.RectangleF),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(System.Drawing.RectangleF),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.RectangleF),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
internal static unsafe class RectangleFMarshaller
{
    public static RectangleF ConvertToUnmanaged(System.Drawing.RectangleF managed)
    {
        return new RectangleF
        {
            X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
        };
    }

    public static System.Drawing.RectangleF ConvertToManaged(RectangleF unmanaged)
    {
        return new System.Drawing.RectangleF(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RectangleF
    {
        public float X;
        public float Y;
        public float W;
        public float H;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private RectangleF* _unmanagedPtr;
        private RectangleF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.RectangleF managed)
        {
            if (managed == System.Drawing.RectangleF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new RectangleF
            {
                X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
            };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (RectangleF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public RectangleF* ToUnmanaged()
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
        private System.Drawing.RectangleF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(RectangleF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.RectangleF.Empty;
                return;
            }

            _managed = new System.Drawing.RectangleF(
                unmanaged->X,
                unmanaged->Y,
                unmanaged->W,
                unmanaged->H
            );

            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.RectangleF ToManaged()
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
        private System.Drawing.RectangleF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(RectangleF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.RectangleF.Empty;
                return;
            }

            _managed = new System.Drawing.RectangleF(
                unmanaged->X,
                unmanaged->Y,
                unmanaged->W,
                unmanaged->H
            );

            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.RectangleF ToManaged()
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
        private RectangleF* _unmanagedPtr;
        private RectangleF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.RectangleF managed)
        {
            if (managed == System.Drawing.RectangleF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new RectangleF
            {
                X = managed.X, Y = managed.Y, W = managed.Width, H = managed.Height
            };

            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (RectangleF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public RectangleF* ToUnmanaged()
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
