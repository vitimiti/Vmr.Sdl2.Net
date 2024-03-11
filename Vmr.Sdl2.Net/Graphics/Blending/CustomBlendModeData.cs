// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

namespace Vmr.Sdl2.Net.Graphics.Blending;

[Serializable]
public struct CustomBlendModeData : IEquatable<CustomBlendModeData>
{
    public BlendFactor SrcFactor { get; set; }
    public BlendFactor DstFactor { get; set; }
    public BlendOperation Operation { get; set; }

    public bool Equals(CustomBlendModeData other)
    {
        return SrcFactor == other.SrcFactor
               && DstFactor == other.DstFactor
               && Operation == other.Operation;
    }

    public override bool Equals(object? obj)
    {
        return obj is CustomBlendModeData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)SrcFactor, (int)DstFactor, (int)Operation);
    }

    public override string ToString()
    {
        return $"{{Src Factor: {SrcFactor}, Dst Factor: {DstFactor}, Operation: {Operation}}}";
    }

    public static bool operator ==(CustomBlendModeData left, CustomBlendModeData right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CustomBlendModeData left, CustomBlendModeData right)
    {
        return !(left == right);
    }
}
