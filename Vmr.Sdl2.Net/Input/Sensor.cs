// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.If
// not, see <https://www.gnu.org/licenses/>.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.SensorUtilities;

namespace Vmr.Sdl2.Net.Input;

[NativeMarshalling(typeof(SafeHandleMarshaller<Sensor>))]
public class Sensor : SafeHandleZeroOrMinusOneIsInvalid
{
    private Sensor(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Sensor(int deviceIndex)
        : base(true)
    {
        handle = Sdl.SensorOpen(deviceIndex);
        if (handle == nint.Zero)
        {
            throw new SensorException($"Unable to open the sensor for device index {deviceIndex}");
        }
    }

    public static int Count => Sdl.NumSensors();

    public string? Name => Sdl.SensorGetName(this);
    public SensorType Type => Sdl.SensorGetType(this);
    public int NonPortableType => Sdl.SensorGetNonPortableType(this);
    public int InstanceId => Sdl.SensorGetInstanceId(this);

    public static void LockAll()
    {
        Sdl.LockSensors();
    }

    public static void UnlockAll()
    {
        Sdl.UnlockSensors();
    }

    public static SensorType GetTypeForDevice(int deviceIndex)
    {
        SensorType result = Sdl.SensorGetDeviceType(deviceIndex);
        if (result == SensorType.Invalid)
        {
            throw new SensorException(
                $"Unable to get the sensor type for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static int GetNonPortableTypeForDevice(int deviceIndex)
    {
        int result = Sdl.SensorGetDeviceNonPortableType(deviceIndex);
        if (result < 0)
        {
            throw new SensorException(
                $"Unable to get the sensor non portable type for device index {deviceIndex}",
                result
            );
        }

        return result;
    }

    public static int GetInstanceIdForDevice(int deviceIndex)
    {
        int result = Sdl.SensorGetDeviceInstanceId(deviceIndex);
        if (result < 0)
        {
            throw new SensorException(
                $"Unable to get the sensor instance ID for device index {deviceIndex}"
            );
        }

        return result;
    }

    public static Sensor FromInstanceId(int instanceId)
    {
        nint sensorHandle = Sdl.SensorFromInstanceId(instanceId);
        if (sensorHandle == nint.Zero)
        {
            throw new SensorException($"Unable to get sensor from instance ID {instanceId}");
        }

        return new Sensor(sensorHandle, true);
    }

    public static void Update()
    {
        Sdl.SensorUpdate();
    }

    public float[] GetState(int numberOfValues)
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfValues * sizeof(float)));
            try
            {
                int code = Sdl.SensorGetData(this, dataPtr, numberOfValues);
                if (code < 0)
                {
                    throw new SensorException("Unable to get the sensor state", code);
                }

                Span<float> data = new(dataPtr, numberOfValues);
                return data.ToArray();
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public FullSensorState GetFullState(int numberOfValues)
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfValues * sizeof(float)));
            try
            {
                int code = Sdl.SensorGetDataWithTimeStamp(
                    this,
                    out ulong timeStamp,
                    dataPtr,
                    numberOfValues
                );

                if (code < 0)
                {
                    throw new SensorException("Unable to get the sensor full data", code);
                }

                Span<float> data = new(dataPtr, numberOfValues);
                return new FullSensorState
                {
                    TimeStamp = TimeSpan.FromMicroseconds(timeStamp), Data = data.ToArray()
                };
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    protected override bool ReleaseHandle()
    {
        if (handle == nint.Zero)
        {
            return true;
        }

        Sdl.SensorClose(handle);
        handle = nint.Zero;
        return true;
    }
}
