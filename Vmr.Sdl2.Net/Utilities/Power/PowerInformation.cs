// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net.Utilities.Power;

public delegate void BatteryLifeHandler(BatteryLife batteryLife, PowerState state);

public static class PowerInformation
{
    public static PowerState GetState(BatteryLifeHandler? batteryLife = null)
    {
        PowerState result = Sdl.GetPowerInfo(out int seconds, out int percent);
        batteryLife?.Invoke(
            new BatteryLife { TimeLeft = TimeSpan.FromSeconds(seconds), PercentageLeft = percent },
            result
        );

        return result;
    }
}
