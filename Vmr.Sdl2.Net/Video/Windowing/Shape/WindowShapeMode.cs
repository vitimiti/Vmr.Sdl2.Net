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

using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Video.Windowing.Shape;

[Serializable]
[NativeMarshalling(typeof(WindowShapeModeMarshaller))]
public struct WindowShapeMode : IEquatable<WindowShapeMode>
{
    public ShapeMode Mode { get; set; }
    public WindowShapeParams Parameters { get; set; }

    public bool Equals(WindowShapeMode other)
    {
        return Mode == other.Mode && Parameters.Equals(other.Parameters);
    }

    public override bool Equals(object? obj)
    {
        return obj is WindowShapeMode other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Mode, Parameters);
    }

    public override string ToString()
    {
        return $"{{Mode: {Mode}, Parameters: {Parameters}}}";
    }

    public static bool operator ==(WindowShapeMode left, WindowShapeMode right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WindowShapeMode left, WindowShapeMode right)
    {
        return !left.Equals(right);
    }
}
