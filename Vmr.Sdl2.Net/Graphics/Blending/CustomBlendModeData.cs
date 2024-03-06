// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

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
