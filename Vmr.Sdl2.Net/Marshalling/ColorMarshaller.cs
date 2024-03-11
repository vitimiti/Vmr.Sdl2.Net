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

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(typeof(System.Drawing.Color), MarshalMode.Default, typeof(ColorMarshaller))]
internal static class ColorMarshaller
{
    public static System.Drawing.Color ConvertToManaged(Color unmanaged)
    {
        return System.Drawing.Color.FromArgb(unmanaged.A, unmanaged.R, unmanaged.G, unmanaged.B);
    }

    public static Color ConvertToUnmanaged(System.Drawing.Color managed)
    {
        return new Color { R = managed.R, G = managed.G, B = managed.B, A = managed.A };
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }
}
