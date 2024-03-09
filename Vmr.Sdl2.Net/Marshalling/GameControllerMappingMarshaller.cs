// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input.GameControllerUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(GameControllerMapping),
    MarshalMode.Default,
    typeof(GameControllerMappingMarshaller)
)]
internal static unsafe class GameControllerMappingMarshaller
{
    public static GameControllerMapping ConvertToManaged(byte* unmanaged)
    {
        string? managedStr = Utf8StringMarshaller.ConvertToManaged(unmanaged);
        return managedStr is null ? default : GameControllerMapping.FromNativeString(managedStr);
    }

    public static byte* ConvertToUnmanaged(GameControllerMapping managed)
    {
        return managed == default
            ? null
            : Utf8StringMarshaller.ConvertToUnmanaged(managed.ToNativeString());
    }
}
