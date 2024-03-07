// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Rectangle), MarshalMode.Default, typeof(SdlRectangleMarshaller))]
[CustomMarshaller(
    typeof(Rectangle),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
[CustomMarshaller(
    typeof(Rectangle),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
[CustomMarshaller(
    typeof(Rectangle),
    MarshalMode.UnmanagedToManagedIn,
    typeof(UnmanagedToManagedIn)
)]
[CustomMarshaller(
    typeof(Rectangle),
    MarshalMode.UnmanagedToManagedOut,
    typeof(UnmanagedToManagedOut)
)]
internal static unsafe class SdlRectangleMarshaller
{
    public static SdlRect ConvertToUnmanaged(Rectangle managed)
    {
        return new SdlRect
        {
            X = managed.X,
            Y = managed.Y,
            W = managed.Width,
            H = managed.Height
        };
    }

    public static Rectangle ConvertToManaged(SdlRect unmanaged)
    {
        return new Rectangle(unmanaged.X, unmanaged.Y, unmanaged.W, unmanaged.H);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlRect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlRect* _unmanagedPtr;
        private SdlRect _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Rectangle managed)
        {
            if (managed == Rectangle.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlRect
            {
                X = managed.X,
                Y = managed.Y,
                W = managed.Width,
                H = managed.Height
            };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlRect* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlRect* ToUnmanaged()
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
        private Rectangle _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlRect* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = Rectangle.Empty;
                return;
            }

            _managed = new Rectangle(unmanaged->X, unmanaged->Y, unmanaged->W, unmanaged->H);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Rectangle ToManaged()
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
        private Rectangle _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlRect* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = Rectangle.Empty;
                return;
            }

            _managed = new Rectangle(unmanaged->X, unmanaged->Y, unmanaged->W, unmanaged->H);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Rectangle ToManaged()
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
        private SdlRect* _unmanagedPtr;
        private SdlRect _unmanaged;
        private GCHandle _gcHandle;

        public void FromManaged(Rectangle managed)
        {
            if (managed == Rectangle.Empty)
            {
                _unmanagedPtr = null;
                return;
            }

            _unmanaged = new SdlRect
            {
                X = managed.X,
                Y = managed.Y,
                W = managed.Width,
                H = managed.Height
            };
            _gcHandle = GCHandle.Alloc(_unmanaged, GCHandleType.Pinned);
            fixed (SdlRect* ptr = &_unmanaged)
            {
                _unmanagedPtr = ptr;
            }
        }

        public SdlRect* ToUnmanaged()
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
