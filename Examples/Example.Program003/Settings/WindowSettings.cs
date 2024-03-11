// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;

namespace Example.Program003.Settings;

[Serializable]
public readonly struct WindowSettings
{
    public required string Title { get; init; }
    public Point Position { get; init; }
    public required Size Size { get; init; }
}
