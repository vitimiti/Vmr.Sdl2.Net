// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
