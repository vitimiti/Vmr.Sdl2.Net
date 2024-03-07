// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Point), MarshalMode.Default, typeof(SdlPointMarshaller))]
[CustomMarshaller(typeof(Point), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToUnmanagedIn))]
[CustomMarshaller(typeof(Point), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
[CustomMarshaller(typeof(Point), MarshalMode.UnmanagedToManagedIn, typeof(UnmanagedToManagedIn))]
[CustomMarshaller(typeof(Point), MarshalMode.UnmanagedToManagedOut, typeof(UnmanagedToManagedOut))]
[CustomMarshaller(typeof(Point), MarshalMode.ElementIn, typeof(ElementIn))]
internal static unsafe class SdlPointMarshaller
{
    public static Point ConvertToManaged(SdlPoint unmanaged)
    {
        return new Point(unmanaged.X, unmanaged.Y);
    }

    public static SdlPoint ConvertToUnmanaged(Point managed)
    {
        return new SdlPoint { X = managed.X, Y = managed.Y };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlPoint
    {
        public int X;
        public int Y;
    }

    public static class ElementIn
    {
        public static Point ConvertToManaged(SdlPoint unmanaged)
        {
            return new Point(unmanaged.X, unmanaged.Y);
        }

        public static SdlPoint ConvertToUnmanaged(Point managed)
        {
            return new SdlPoint { X = managed.X, Y = managed.Y };
        }

        public static void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlPoint* _unmanagedPtr;
        private SdlPoint _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Point managed)
        {
            if (managed == Point.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlPoint { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlPoint* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlPoint* ToUnmanaged()
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
        private Point _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlPoint* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = Point.Empty;
                return;
            }

            _managed = new Point(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Point ToManaged()
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
        private Point _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlPoint* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = Point.Empty;
                return;
            }

            _managed = new Point(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Point ToManaged()
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
        private SdlPoint* _unmanagedPtr;
        private SdlPoint _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Point managed)
        {
            if (managed == Point.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlPoint { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlPoint* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlPoint* ToUnmanaged()
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
