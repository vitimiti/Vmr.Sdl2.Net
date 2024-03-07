// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[ContiguousCollectionMarshaller]
[CustomMarshaller(typeof(PointF[]), MarshalMode.Default, typeof(SdlPointFArrayMarshaller))]
[CustomMarshaller(typeof(PointF[]), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToUnmanagedIn))]
internal static unsafe class SdlPointFArrayMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SdlPointF
    {
        public float X;
        public float Y;
    }

    public static SdlPointF* AllocateContainerForUnmanagedElements(
        PointF[]? managed,
        out int numElements
    )
    {
        if (managed is null)
        {
            numElements = 0;
            return null;
        }

        numElements = managed.Length;
        return (SdlPointF*)
            Marshal.AllocCoTaskMem(Math.Max(checked(sizeof(SdlPointF) * numElements), 1));
    }

    public static ReadOnlySpan<PointF> GetManagedValuesSource(PointF[]? managed)
    {
        return managed;
    }

    public static Span<SdlPointF> GetUnmanagedValuesDestination(
        SdlPointF* unmanaged,
        int numElements
    )
    {
        return new Span<SdlPointF>(unmanaged, numElements);
    }

    public static PointF[]? AllocateContainerForManagedElements(SdlPointF* unmanaged, int length)
    {
        return unmanaged is null ? null : new PointF[length];
    }

    public static Span<PointF> GetManagedValuesDestination(PointF[]? managed)
    {
        return managed;
    }

    public static ReadOnlySpan<SdlPointF> GetUnmanagedValuesSource(
        SdlPointF* nativeValue,
        int numElements
    )
    {
        return new ReadOnlySpan<SdlPointF>(nativeValue, numElements);
    }

    public static void Free(SdlPointF* unmanaged)
    {
        Marshal.FreeCoTaskMem((nint)unmanaged);
    }

    public ref struct ManagedToUnmanagedIn
    {
        private PointF[]? _managed;
        private SdlPointF* _unmanaged;
        private Span<SdlPointF> _unmanagedSpan;

        public static int BufferSize { get; } = 0x0200 / sizeof(SdlPointF);

        public static ref PointF GetPinnableReference(PointF[]? managed)
        {
            return ref (
                managed is null
                    ? ref Unsafe.NullRef<PointF>()
                    : ref MemoryMarshal.GetArrayDataReference(managed)
            );
        }

        public void FromManaged(PointF[]? managed, Span<SdlPointF> buffer)
        {
            _unmanaged = null;
            if (managed is null)
            {
                _managed = null;
                _unmanagedSpan = Span<SdlPointF>.Empty;
                return;
            }

            _managed = managed;
            if (managed.Length <= buffer.Length)
            {
                _unmanagedSpan = buffer[..managed.Length];
            }
            else
            {
                _unmanaged = (SdlPointF*)
                    NativeMemory.Alloc(
                        (nuint)Math.Max(checked(managed.Length * sizeof(SdlPointF)), 1)
                    );

                _unmanagedSpan = new Span<SdlPointF>(_unmanaged, managed.Length);
            }
        }

        public ReadOnlySpan<PointF> GetManagedValuesSource()
        {
            return _managed;
        }

        public Span<SdlPointF> GetUnmanagedValuesDestination()
        {
            return _unmanagedSpan;
        }

        public ref SdlPointF GetPinnableReference()
        {
            return ref MemoryMarshal.GetReference(_unmanagedSpan);
        }

        public SdlPointF* ToUnmanaged()
        {
            return (SdlPointF*)Unsafe.AsPointer(ref GetPinnableReference());
        }

        public void Free()
        {
            NativeMemory.Free(_unmanaged);
        }
    }
}
