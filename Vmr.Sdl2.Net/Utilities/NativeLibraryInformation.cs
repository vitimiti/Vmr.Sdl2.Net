// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities;

public delegate void VersionMismatchHandler(Version expectedVersion, Version version);

public static class NativeLibraryInformation
{
    internal static Version ExpectedVersion => new(2, 30, 0);

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
