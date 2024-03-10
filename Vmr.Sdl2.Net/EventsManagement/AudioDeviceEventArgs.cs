// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

namespace Vmr.Sdl2.Net.EventsManagement;

public class AudioDeviceEventArgs(
    EventType type,
    TimeSpan timeStamp,
    uint audioDeviceIndex,
    bool isCapture
) : EventArgs
{
    public EventType Type { get; private set; } = type;
    public TimeSpan TimeStamp { get; private set; } = timeStamp;
    public uint AudioDeviceIndex { get; private set; } = audioDeviceIndex;
    public bool IsCapture { get; private set; } = isCapture;
}
