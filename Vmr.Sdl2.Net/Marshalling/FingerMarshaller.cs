// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(Input.Finger), MarshalMode.Default, typeof(FingerMarshaller))]
[CustomMarshaller(
    typeof(Input.Finger),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(ManagedToUnmanagedOut)
)]
internal static unsafe class FingerMarshaller
{
    public static Input.Finger ConvertToManaged(Finger* unmanaged)
    {
        if (unmanaged is null)
        {
            return default;
        }

        return new Input.Finger
        {
            Id = unmanaged->Id,
            Position = new PointF(unmanaged->X, unmanaged->Y),
            Pressure = unmanaged->Pressure
        };
    }

    public static Finger* ConvertToUnmanaged(Input.Finger managed)
    {
        if (managed == default)
        {
            return null;
        }

        Finger unmanaged =
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
    internal struct Finger
    {
        public long Id;
        public float X;
        public float Y;
        public float Pressure;
    }

    public ref struct ManagedToUnmanagedOut
    {
        private Input.Finger _managed;
        private GCHandle _gcHandle;

        public void FromUnmanaged(Finger* unmanaged)
        {
            if (unmanaged is null)
            {
                _managed = default;
                return;
            }

            _managed = ConvertToManaged(unmanaged);
            _gcHandle = GCHandle.Alloc(_managed, GCHandleType.Pinned);
        }

        public Input.Finger ToManaged()
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
