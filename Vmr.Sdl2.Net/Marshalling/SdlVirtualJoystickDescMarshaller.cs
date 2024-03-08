// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Vmr.Sdl2.Net.Input.GameControllerUtilities;
using Vmr.Sdl2.Net.Input.JoystickUtilities;

namespace Vmr.Sdl2.Net.Marshalling;

[CustomMarshaller(
    typeof(VirtualJoystickDesc),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(ManagedToUnmanagedIn)
)]
internal static unsafe class SdlVirtualJoystickDescMarshaller
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SdlVirtualJoystickDesc
    {
        public ushort Version;
        public JoystickType Type;
        public ushort NAxes;
        public ushort NButtons;
        public ushort NHats;
        public ushort VendorId;
        public ushort ProductId;
        private ushort _padding;
        public GameControllerButtons ButtonMask;
        public GameControllerAxes AxisMask;
        public byte* Name;
        public void* UserData;
        public delegate* unmanaged[Cdecl]<void*, void> Update;
        public delegate* unmanaged[Cdecl]<void*, int, void> SetPlayerIndex;
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, int> Rumble;
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, int> RumbleTriggers;
        public delegate* unmanaged[Cdecl]<void*, byte, byte, byte, int> SetLed;
        public delegate* unmanaged[Cdecl]<void*, void*, int, int> SendEffect;
    }

    public ref struct ManagedToUnmanagedIn
    {
        private SdlVirtualJoystickDesc* _unmanaged;
        private GCHandle _gcHandle;
        private GCHandle _descHandle;

        public void FromManaged(VirtualJoystickDesc managed)
        {
            fixed (byte* userDataPtr = managed.UserData)
            {
                SdlVirtualJoystickDesc native =
                    new()
                    {
                        Version = managed.Version,
                        Type = managed.Type,
                        NAxes = managed.NumberOfAxes,
                        NButtons = managed.NumberOfButtons,
                        NHats = managed.NumberOfHats,
                        VendorId = managed.VendorId,
                        ProductId = managed.ProductId,
                        ButtonMask = managed.ButtonMask,
                        AxisMask = managed.AxisMask,
                        Name = Utf8StringMarshaller.ConvertToUnmanaged(managed.Name),
                        UserData = userDataPtr,
                        Update = (delegate* unmanaged[Cdecl]<void*, void>)
                            Marshal.GetFunctionPointerForDelegate(
                                (void* userData) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    managed.Update(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray()
                                    );
                                }
                            ),
                        SetPlayerIndex = (delegate* unmanaged[Cdecl]<void*, int, void>)
                            Marshal.GetFunctionPointerForDelegate(
                                (void* userData, int playerIndex) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    managed.SetPlayerIndex(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray(),
                                        playerIndex
                                    );
                                }
                            ),
                        Rumble = (delegate* unmanaged[Cdecl]<void*, ushort, ushort, int>)
                            Marshal.GetFunctionPointerForDelegate(
                                (
                                    void* userData,
                                    ushort lowFrequencyRumble,
                                    ushort highFrequencyRumble
                                ) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    return managed.Rumble(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray(),
                                        new RumbleFrequency
                                        {
                                            Low = lowFrequencyRumble, High = highFrequencyRumble
                                        },
                                        managed.RumbleErrorHandler
                                    );
                                }
                            ),
                        RumbleTriggers = (delegate* unmanaged[Cdecl]<void*, ushort, ushort, int>)
                            Marshal.GetFunctionPointerForDelegate(
                                (
                                    void* userData,
                                    ushort lowFrequencyRumble,
                                    ushort highFrequencyRumble
                                ) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    return managed.RumbleTriggers(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray(),
                                        new RumbleFrequency
                                        {
                                            Low = lowFrequencyRumble, High = highFrequencyRumble
                                        },
                                        managed.RumbleTriggersErrorHandler
                                    );
                                }
                            ),
                        SetLed = (delegate* unmanaged[Cdecl]<void*, byte, byte, byte, int>)
                            Marshal.GetFunctionPointerForDelegate(
                                (void* userData, byte red, byte green, byte blue) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    return managed.SetLed(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray(),
                                        Color.FromArgb(red, green, blue),
                                        managed.SetLedErrorHandler
                                    );
                                }
                            ),
                        SendEffect = (delegate* unmanaged[Cdecl]<void*, void*, int, int>)
                            Marshal.GetFunctionPointerForDelegate(
                                (void* userData, void* data, int size) =>
                                {
                                    int length = managed.UserData?.Length ?? 0;
                                    Span<byte> managedUserData = Span<byte>.Empty;
                                    if (userData is not null && managed.UserData is not null)
                                    {
                                        managedUserData = new Span<byte>(userData, length);
                                    }

                                    Span<byte> managedData = new(data, size);
                                    return managed.SendEffect(
                                        managedUserData.IsEmpty ? null : managedUserData.ToArray(),
                                        managedData.ToArray(),
                                        managed.SendEffectErrorHandler
                                    );
                                }
                            )
                    };

                _unmanaged = &native;
                _gcHandle = GCHandle.Alloc(managed, GCHandleType.Pinned);
                _descHandle = GCHandle.Alloc(native, GCHandleType.Pinned);
            }
        }

        public SdlVirtualJoystickDesc* ToUnmanaged()
        {
            return _unmanaged;
        }

        public void Free()
        {
            if (_descHandle.IsAllocated)
            {
                _descHandle.Free();
            }

            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
        }
    }
}
