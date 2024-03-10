// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Drawing.Point), MarshalMode.Default, typeof(PointMarshaller))]
[CustomMarshaller(
    typeof(System.Drawing.Point),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.Point),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(System.Drawing.Point),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(System.Drawing.Point),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
[CustomMarshaller(typeof(System.Drawing.Point), MarshalMode.ElementIn, typeof(ElementIn))]
internal static unsafe class PointMarshaller
{
    public static System.Drawing.Point ConvertToManaged(Point unmanaged)
    {
        return new System.Drawing.Point(unmanaged.X, unmanaged.Y);
    }

    public static Point ConvertToUnmanaged(System.Drawing.Point managed)
    {
        return new Point { X = managed.X, Y = managed.Y };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int X;
        public int Y;
    }

    public static class ElementIn
    {
        public static System.Drawing.Point ConvertToManaged(Point unmanaged)
        {
            return new System.Drawing.Point(unmanaged.X, unmanaged.Y);
        }

        public static Point ConvertToUnmanaged(System.Drawing.Point managed)
        {
            return new Point { X = managed.X, Y = managed.Y };
        }

        public static void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private Point* _unmanagedPtr;
        private Point _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.Point managed)
        {
            if (managed == System.Drawing.Point.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new Point { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (Point* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public Point* ToUnmanaged()
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
        private System.Drawing.Point _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(Point* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.Point.Empty;
                return;
            }

            _managed = new System.Drawing.Point(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.Point ToManaged()
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
        private System.Drawing.Point _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(Point* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = System.Drawing.Point.Empty;
                return;
            }

            _managed = new System.Drawing.Point(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public System.Drawing.Point ToManaged()
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
        private Point* _unmanagedPtr;
        private Point _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(System.Drawing.Point managed)
        {
            if (managed == System.Drawing.Point.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new Point { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (Point* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public Point* ToUnmanaged()
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
