// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Finger), MarshalMode.Default, typeof(SdlFingerMarshaller))]
[CustomMarshaller(typeof(Finger), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
internal static unsafe class SdlFingerMarshaller
{
    public static Finger ConvertToManaged(SdlFinger* unmanaged)
    {
        if (unmanaged is null)
        {
            return default;
        }

        return new Finger
        {
            Id = unmanaged->Id,
            Position = new PointF(unmanaged->X, unmanaged->Y),
            Pressure = unmanaged->Pressure
        };
    }

    public static SdlFinger* ConvertToUnmanaged(Finger managed)
    {
        if (managed == default)
        {
            return null;
        }

        SdlFinger unmanaged =
            new()
            {
                Id = managed.Id,
                X = managed.Position.X,
                Y = managed.Position.Y,
                Pressure = managed.Pressure
            };

        return &unmanaged;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlFinger
    {
        public long Id;
        public float X;
        public float Y;
        public float Pressure;
    }

    public ref struct ManagedToUnmanagedOut
    {
        private Finger _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(SdlFinger* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = default;
                return;
            }

            _managed = ConvertToManaged(unmanaged);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Finger ToManaged()
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
}
