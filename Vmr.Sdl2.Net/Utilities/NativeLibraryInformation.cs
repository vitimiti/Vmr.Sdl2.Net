// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public static class NativeLibraryInformation
{
    public static Version Version
    {
        get
        {
            Sdl.GetVersion(out Version version);
            return version;
        }
    }

    public static string? GetRevisionStr => Sdl.GetRevision();
}
