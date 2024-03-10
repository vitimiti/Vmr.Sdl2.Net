// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Vmr.Sdl2.Net.Imports;
using Vmr.Sdl2.Net.Input.GameControllerUtilities.GameControllerMappingUtilities;
using Vmr.Sdl2.Net.Marshalling;

namespace Vmr.Sdl2.Net.Input.GameControllerUtilities;

[Serializable]
[NativeMarshalling(typeof(GameControllerMappingMarshaller))]
public struct GameControllerMapping : IEquatable<GameControllerMapping>
{
    public static int Count => Sdl.GameControllerNumMappings();

    public Guid Guid { get; set; }
    public string? Name { get; set; }
    public GameControllerMappingButton[]? Buttons { get; set; }
    public GameControllerMappingButtonAxis[]? ButtonAxes { get; set; }
    public GameControllerMappingAxis[]? Axes { get; set; }
    public GameControllerMappingAxisButton[]? AxisButtons { get; set; }
    public GameControllerMappingAxisHat[]? AxisHats { get; set; }
    public GameControllerMappingHat[]? Hats { get; set; }
    public OSPlatform? Platform { get; set; }

    internal string ToNativeString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append($"{Guid:N}");
        stringBuilder.Append($",{Name ?? "*"}");
        if (Buttons is not null)
        {
            foreach (GameControllerMappingButton button in Buttons)
            {
                stringBuilder.Append($",{button.ToNativeString()}");
            }
        }

        if (ButtonAxes is not null)
        {
            foreach (GameControllerMappingButtonAxis axisButton in ButtonAxes)
            {
                stringBuilder.Append($",{axisButton.ToNativeString()}");
            }
        }

        if (Axes is not null)
        {
            foreach (GameControllerMappingAxis axis in Axes)
            {
                stringBuilder.Append($",{axis.ToNativeString()}");
            }
        }

        if (AxisButtons is not null)
        {
            foreach (GameControllerMappingAxisButton buttonAxis in AxisButtons)
            {
                stringBuilder.Append($",{buttonAxis.ToNativeString()}");
            }
        }

        if (AxisHats is not null)
        {
            foreach (GameControllerMappingAxisHat hatAxis in AxisHats)
            {
                stringBuilder.Append($",{hatAxis.ToNativeString()}");
            }
        }

        if (Hats is not null)
        {
            foreach (GameControllerMappingHat hat in Hats)
            {
                stringBuilder.Append($",{hat.ToNativeString()}");
            }
        }

        if (Platform is not null)
        {
            stringBuilder.Append($",platform:{Platform.ToString()}");
        }

        return stringBuilder.ToString();
    }

    internal static GameControllerMapping FromNativeString(string nativeString)
    {
        string[] parts = nativeString.Split(',');
        if (parts.Length < 3)
        {
            throw new ArgumentException(
                $"The native string '{nativeString}' doesn't have enough information to generate a new mapping"
            );
        }

        List<GameControllerMappingButton> buttons = [];
        List<GameControllerMappingAxisButton> axisButtons = [];
        foreach (string part in parts)
        {
            if (!part.Contains(":b"))
            {
                continue;
            }

            try
            {
                buttons.Add(GameControllerMappingButton.FromNativeString(part));
            }
            catch (ArgumentException)
            {
                axisButtons.Add(GameControllerMappingAxisButton.FromNativeString(part));
            }
        }

        List<GameControllerMappingAxis> axes = [];
        List<GameControllerMappingButtonAxis> buttonAxes = [];
        foreach (string part in parts)
        {
            if (!part.Contains(":a"))
            {
                continue;
            }

            try
            {
                axes.Add(GameControllerMappingAxis.FromNativeString(part));
            }
            catch (ArgumentException)
            {
                buttonAxes.Add(GameControllerMappingButtonAxis.FromNativeString(part));
            }
        }

        List<GameControllerMappingHat> hats = [];
        List<GameControllerMappingAxisHat> axisHats = [];
        foreach (string part in parts)
        {
            if (!part.Contains(":h"))
            {
                continue;
            }

            try
            {
                hats.Add(GameControllerMappingHat.FromNativeString(part));
            }
            catch (ArgumentException)
            {
                axisHats.Add(GameControllerMappingAxisHat.FromNativeString(part));
            }
        }

        string? platformStr = parts
            .FirstOrDefault(part => part.Contains("platform:"))
            ?.Split(':')[1];

        GuidMarshaller.Guid nativeGuid = Sdl.GuidFromString(parts[0]);
        return new GameControllerMapping
        {
            Guid = GuidMarshaller.ConvertToManaged(nativeGuid),
            Name = parts[1] == "*" ? null : parts[1],
            Buttons = buttons.Count == 0 ? null : buttons.ToArray(),
            ButtonAxes = buttonAxes.Count == 0 ? null : buttonAxes.ToArray(),
            Axes = axes.Count == 0 ? null : axes.ToArray(),
            AxisButtons = buttons.Count == 0 ? null : axisButtons.ToArray(),
            AxisHats = hats.Count == 0 ? null : axisHats.ToArray(),
            Hats = hats.Count == 0 ? null : hats.ToArray(),
            Platform = platformStr is null ? null : OSPlatform.Create(platformStr)
        };
    }

    public bool Equals(GameControllerMapping other)
    {
        return Guid.Equals(other.Guid)
            && Name == other.Name
            && Equals(Buttons, other.Buttons)
            && Equals(ButtonAxes, other.ButtonAxes)
            && Equals(Axes, other.Axes)
            && Equals(AxisButtons, other.AxisButtons)
            && Equals(AxisHats, other.AxisHats)
            && Equals(Hats, other.Hats)
            && Platform.Equals(other.Platform);
    }

    public override bool Equals(object? obj)
    {
        return obj is GameControllerMapping other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Guid, Name, Buttons, ButtonAxes, Axes, AxisButtons, Hats, Platform);
    }

    public override string ToString()
    {
        return $"{{Guid: {Guid}, Name: {Name}, Buttons: [{(Buttons is null ? string.Empty : string.Join(", ", Buttons))}], Button Axes: [{(ButtonAxes is null ? string.Empty : string.Join(", ", ButtonAxes))}], Axes: [{(Axes is null ? string.Empty : string.Join(", ", Axes))}], Axis Buttons: [{(AxisButtons is null ? string.Empty : string.Join(", ", AxisButtons))}], Axis Hats: [{(AxisHats is null ? string.Empty : string.Join(", ", AxisHats))}], Hats: [{(Hats is null ? string.Empty : string.Join(", ", Hats))}], Platform: {Platform}";
    }

    public static bool operator ==(GameControllerMapping left, GameControllerMapping right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameControllerMapping left, GameControllerMapping right)
    {
        return !left.Equals(right);
    }
}
