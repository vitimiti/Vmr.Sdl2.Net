// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[ContiguousCollectionMarshaller]
[CustomMarshaller(typeof(Point[]), MarshalMode.Default, typeof(SdlPointArrayMarshaller))]
[CustomMarshaller(typeof(Point[]), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToUnmanagedIn))]
internal static unsafe class SdlPointArrayMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPoint
    {
        public int X;
        public int Y;
    }

    public static SdlPoint* AllocateContainerForUnmanagedElements(
        Point[]? managed,
        out int numElements
    )
    {
        if (managed is null)
        {
            numElements = 0;
            return null;
        }

        numElements = managed.Length;
        return (SdlPoint*)
            Marshal.AllocCoTaskMem(Math.Max(checked(sizeof(SdlPoint) * numElements), 1));
    }

    public static ReadOnlySpan<Point> GetManagedValuesSource(Point[]? managed)
    {
        return managed;
    }

    public static Span<SdlPoint> GetUnmanagedValuesDestination(SdlPoint* unmanaged, int numElements)
    {
        return new Span<SdlPoint>(unmanaged, numElements);
    }

    public static Point[]? AllocateContainerForManagedElements(SdlPoint* unmanaged, int length)
    {
        return unmanaged is null ? null : new Point[length];
    }

    public static Span<Point> GetManagedValuesDestination(Point[]? managed)
    {
        return managed;
    }

    public static ReadOnlySpan<SdlPoint> GetUnmanagedValuesSource(
        SdlPoint* nativeValue,
        int numElements
    )
    {
        return new ReadOnlySpan<SdlPoint>(nativeValue, numElements);
    }

    public static void Free(SdlPoint* unmanaged)
    {
        Marshal.FreeCoTaskMem((nint)unmanaged);
    }

    public ref struct ManagedToUnmanagedIn
    {
        private Point[]? _managed;
        private SdlPoint* _unmanaged;
        private Span<SdlPoint> _unmanagedSpan;

        public static int BufferSize { get; } = 0x0200 / sizeof(SdlPoint);

        public static ref Point GetPinnableReference(Point[]? managed)
        {
            return ref (
                managed is null
                    ? ref Unsafe.NullRef<Point>()
                    : ref MemoryMarshal.GetArrayDataReference(managed)
            );
        }

        public void FromManaged(Point[]? managed, Span<SdlPoint> buffer)
        {
            _unmanaged = null;
            if (managed is null)
            {
                _managed = null;
                _unmanagedSpan = Span<SdlPoint>.Empty;
                return;
            }

            _managed = managed;
            if (managed.Length <= buffer.Length)
            {
                _unmanagedSpan = buffer[..managed.Length];
            }
            else
            {
                _unmanaged = (SdlPoint*)
                    NativeMemory.Alloc(
                        (nuint)Math.Max(checked(managed.Length * sizeof(SdlPoint)), 1)
                    );

                _unmanagedSpan = new Span<SdlPoint>(_unmanaged, managed.Length);
            }
        }

        public ReadOnlySpan<Point> GetManagedValuesSource()
        {
            return _managed;
        }

        public Span<SdlPoint> GetUnmanagedValuesDestination()
        {
            return _unmanagedSpan;
        }

        public ref SdlPoint GetPinnableReference()
        {
            return ref MemoryMarshal.GetReference(_unmanagedSpan);
        }

        public SdlPoint* ToUnmanaged()
        {
            return (SdlPoint*)Unsafe.AsPointer(ref GetPinnableReference());
        }

        public void Free()
        {
            NativeMemory.Free(_unmanaged);
        }
    }
}
