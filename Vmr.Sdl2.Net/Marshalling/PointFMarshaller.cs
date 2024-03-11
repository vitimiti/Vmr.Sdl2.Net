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

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Drawing.PointF), MarshalMode.Default, typeof(PointFMarshaller))]
[CustomMarshaller(
    typeof(System.Drawing.PointF),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.PointF),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(System.Drawing.PointF),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
[CustomMarshaller(typeof(System.Drawing.PointF), MarshalMode.ElementIn, typeof(ElementIn))]
internal static unsafe class PointFMarshaller
{
    public static System.Drawing.PointF ConvertToManaged(PointF unmanaged)
    {
        return new System.Drawing.PointF(unmanaged.X, unmanaged.Y);
    }

    public static PointF ConvertToUnmanaged(System.Drawing.PointF managed)
    {
        return new PointF { X = managed.X, Y = managed.Y };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PointF
    {
        public float X;
        public float Y;
    }

    public static class ElementIn
    {
        public static System.Drawing.PointF ConvertToManaged(PointF unmanaged)
        {
            return new System.Drawing.PointF(unmanaged.X, unmanaged.Y);
        }

        public static PointF ConvertToUnmanaged(System.Drawing.PointF managed)
        {
            return new PointF { X = managed.X, Y = managed.Y };
        }

        public static void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private PointF* _unmanagedPtr;
        private PointF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.PointF managed)
        {
            if (managed == System.Drawing.PointF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new PointF { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (PointF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public PointF* ToUnmanaged()
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
        private System.Drawing.PointF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(PointF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.PointF.Empty;
                return;
            }

            _managed = new System.Drawing.PointF(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.PointF ToManaged()
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
        private PointF* _unmanagedPtr;
        private PointF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.PointF managed)
        {
            if (managed == System.Drawing.PointF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new PointF { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (PointF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public PointF* ToUnmanaged()
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
