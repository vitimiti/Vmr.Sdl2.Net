// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(PointF), MarshalMode.Default, typeof(SdlPointFMarshaller))]
[CustomMarshaller(typeof(PointF), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToUnmanagedIn))]
[CustomMarshaller(typeof(PointF), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
[CustomMarshaller(typeof(PointF), MarshalMode.UnmanagedToManagedOut, typeof(UnmanagedToManagedOut))]
[CustomMarshaller(typeof(PointF), MarshalMode.ElementIn, typeof(ElementIn))]
internal static unsafe class SdlPointFMarshaller
{
    public static PointF ConvertToManaged(SdlPointF unmanaged)
    {
        return new PointF(unmanaged.X, unmanaged.Y);
    }

    public static SdlPointF ConvertToUnmanaged(PointF managed)
    {
        return new SdlPointF { X = managed.X, Y = managed.Y };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlPointF
    {
        public float X;
        public float Y;
    }

    public static class ElementIn
    {
        public static PointF ConvertToManaged(SdlPointF unmanaged)
        {
            return new PointF(unmanaged.X, unmanaged.Y);
        }

        public static SdlPointF ConvertToUnmanaged(PointF managed)
        {
            return new SdlPointF { X = managed.X, Y = managed.Y };
        }

        public static void Free()
        {
            // Nothing to do here
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlPointF* _unmanagedPtr;
        private SdlPointF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(PointF managed)
        {
            if (managed == PointF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlPointF { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlPointF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlPointF* ToUnmanaged()
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
        private PointF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlPointF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = PointF.Empty;
                return;
            }

            _managed = new PointF(unmanaged->X, unmanaged->Y);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public PointF ToManaged()
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
        private SdlPointF* _unmanagedPtr;
        private SdlPointF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(PointF managed)
        {
            if (managed == PointF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlPointF { X = managed.X, Y = managed.Y };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlPointF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlPointF* ToUnmanaged()
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
