// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program002.Settings;

[Serializable]
public readonly struct WindowSettings
{
    public required string Title { get; init; }
    public Point Position { get; init; }
    public required Size Size { get; init; }
    public required WindowOptions Flags { get; init; }
}