// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Win32.SafeHandles;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.SensorUtilities;
using Vmr.Sdl2.Net.Utilities;

namespace Vmr.Sdl2.Net.Input;

[NativeMarshalling(typeof(SafeHandleMarshaller<Sensor>))]
public class Sensor : SafeHandleZeroOrMinusOneIsInvalid
{
    private Sensor(nint preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        handle = preexistingHandle;
    }

    public Sensor(int deviceIndex, ErrorHandler errorHandler)
        : base(true)
    {
        handle = Sdl.SensorOpen(deviceIndex);
        if (handle == nint.Zero)
        {
            errorHandler(Sdl.GetError());
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

    public static SensorType GetTypeForDevice(int deviceIndex, ErrorCodeHandler errorHandler)
    {
        SensorType result = Sdl.SensorGetDeviceType(deviceIndex);
        if (result == SensorType.Invalid)
        {
            errorHandler(Sdl.GetError(), (int)result);
        }

        return result;
    }

    public static int GetNonPortableTypeForDevice(int deviceIndex, ErrorCodeHandler errorHandler)
    {
        int result = Sdl.SensorGetDeviceNonPortableType(deviceIndex);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return result;
    }

    public static int GetInstanceIdForDevice(int deviceIndex, ErrorCodeHandler errorHandler)
    {
        int result = Sdl.SensorGetDeviceInstanceId(deviceIndex);
        if (result < 0)
        {
            errorHandler(Sdl.GetError(), result);
        }

        return result;
    }

    public static Sensor? FromInstanceId(int instanceId, ErrorHandler errorHandler)
    {
        nint sensorHandle = Sdl.SensorFromInstanceId(instanceId);
        if (sensorHandle != nint.Zero)
        {
            return new Sensor(sensorHandle, true);
        }

        errorHandler(Sdl.GetError());
        return null;
    }

    public static void Update()
    {
        Sdl.SensorUpdate();
    }

    public float[]? GetState(int numberOfValues, ErrorCodeHandler errorHandler)
    {
        unsafe
        {
            float* dataPtr = (float*)NativeMemory.Alloc((nuint)(numberOfValues * sizeof(float)));
            try
            {
                int code = Sdl.SensorGetData(this, dataPtr, numberOfValues);
                if (code >= 0)
                {
                    Span<float> data = new(dataPtr, numberOfValues);
                    return data.ToArray();
                }

                errorHandler(Sdl.GetError(), code);
                return null;
            }
            finally
            {
                NativeMemory.Free(dataPtr);
            }
        }
    }

    public FullSensorState GetFullState(int numberOfValues, ErrorCodeHandler errorHandler)
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
                    errorHandler(Sdl.GetError(), code);
                }

                Span<float> data = new(dataPtr, numberOfValues);
                return new FullSensorState
                {
                    TimeStamp = TimeSpan.FromMicroseconds(timeStamp),
                    Data = code < 0 ? null : data.ToArray()
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
