// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices.Marshalling;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Video.Messages;

[NativeMarshalling(typeof(MessageBoxDataMarshaller))]
public struct MessageBoxData
{
    public required MessageBoxOptions Flags { get; set; }
    public Window? Parent { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public required MessageBoxButtonData[] Buttons { get; set; }
    public required MessageBoxColorScheme ColorScheme { get; set; }
}
