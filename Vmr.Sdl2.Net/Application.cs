// The Vmr.Sdl2.Net library implements SDL2 in dotnet with .NET conventions and safety features.
// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>
//
// This file is part of Vmr.Sdl2.Net.
//
// Vmr.Sdl2.Net is free software:you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// Vmr.Sdl2.Net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Vmr.Sdl2.Net.
// If not, see <https://www.gnu.org/licenses/>.

using Vmr.Sdl2.Net.Exceptions;
using Vmr.Sdl2.Net.Imports;

namespace Vmr.Sdl2.Net;

public class Application : IDisposable
{
    private readonly ApplicationSubsystems _subsystems;

    public Application(ApplicationSubsystems subsystems)
    {
        _subsystems = subsystems;
        ShouldQuit = false;
    }

    public static ApplicationSubsystems InitializedSubsystems =>
        Sdl.WasInit(ApplicationSubsystems.None);

    public static bool ShouldQuit { get; set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        Sdl.Quit();
    }

    protected virtual void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            // Nothing to do here.
        }
    }

    ~Application()
    {
        Dispose(false);
    }

    protected virtual void Init()
    {
        int code = Sdl.Init(_subsystems);
        if (code < 0)
        {
            throw new AppException(
                $"Unable to initialize SDL2 with subsystems [{_subsystems}]",
                code
            );
        }
    }

    protected virtual void Load() { }

    protected virtual void Update()
    {
        Events.Poll();
    }

    public void Run()
    {
        Init();
        Load();
        while (!ShouldQuit)
        {
            Update();
        }
    }
}
