// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class CpuInfo
{
    public static int LogicalCores => Sdl.GetCpuCount();
    public static int CacheLineSizeInB => Sdl.GetCpuCacheLineSize();
    public static int SystemRamInMiB => Sdl.GetSystemRam();
    public static bool HasRdtsc => Sdl.HasRdtsc();
    public static bool HasAltiVec => Sdl.HasAltiVec();
    public static bool HasMmx => Sdl.HasMmx();
    public static bool Has3DNow => Sdl.Has3DNow();
    public static bool HasSse => Sdl.HasSse();
    public static bool HasSse2 => Sdl.HasSse2();
    public static bool HasSse3 => Sdl.HasSse3();
    public static bool HasSse41 => Sdl.HasSse41();
    public static bool HasSse42 => Sdl.HasSse42();
    public static bool HasAvx => Sdl.HasAvx();
    public static bool HasAvx2 => Sdl.HasAvx2();
    public static bool HasAvx512F => Sdl.HasAvx512F();
    public static bool HasArmSimd => Sdl.HasArmSimd();
    public static bool HasNeon => Sdl.HasNeon();
    public static bool HasLsx => Sdl.HasLsx();
    public static bool HasLasx => Sdl.HasLasx();
}
