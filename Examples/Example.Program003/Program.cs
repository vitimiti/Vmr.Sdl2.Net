// The Vmr.Sdl2.Net library implements SDL2 in dotnet with dotnet conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using System.Drawing;

using Example.Program003;
using Example.Program003.Settings;

using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

try
{
    Game game =
        new(
            new WindowSettings
            {
                Title = "Example Vmr.Sdl2.Net Program 003",
                Position = new Point(WindowPredefinedPosition.Centered),
                Size = new Size(640, 480)
            }
        );

    game.Run();
}
catch (Exception e)
{
    MessageBox.Show(MessageBoxOptions.Error, "Unmanaged Exception", e.ToString(), null);
    throw;
}
