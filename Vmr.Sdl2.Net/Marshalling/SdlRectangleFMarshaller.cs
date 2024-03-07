// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(RectangleF), MarshalMode.Default, typeof(SdlRectangleFMarshaller))]
[CustomMarshaller(
    typeof(RectangleF),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(RectangleF),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(RectangleF),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(RectangleF),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
internal static unsafe class SdlRectangleFMarshaller
{
    public static SdlRectF ConvertToUnmanaged(RectangleF managed)
    {
        return new SdlRectF
        {
            X = managed.X,
            Y = managed.Y,
            W = managed.Width,
            H = managed.Height
        };
    }

    public static RectangleF ConvertToManaged(SdlRectF unmanaged)
    {
        return new RectangleF(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlRectF
    {
        public float X;
        public float Y;
        public float W;
        public float H;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlRectF* _unmanagedPtr;
        private SdlRectF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(RectangleF managed)
        {
            if (managed == RectangleF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlRectF
            {
                X = managed.X,
                Y = managed.Y,
                W = managed.Width,
                H = managed.Height
            };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlRectF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlRectF* ToUnmanaged()
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
        private RectangleF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlRectF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = RectangleF.Empty;
                return;
            }

            _managed = new RectangleF(unmanaged->X, unmanaged->Y, unmanaged->W, unmanaged->H);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public RectangleF ToManaged()
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
        private RectangleF _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlRectF* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = RectangleF.Empty;
                return;
            }

            _managed = new RectangleF(unmanaged->X, unmanaged->Y, unmanaged->W, unmanaged->H);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public RectangleF ToManaged()
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
        private SdlRectF* _unmanagedPtr;
        private SdlRectF _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(RectangleF managed)
        {
            if (managed == RectangleF.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlRectF
            {
                X = managed.X,
                Y = managed.Y,
                W = managed.Width,
                H = managed.Height
            };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlRectF* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlRectF* ToUnmanaged()
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
