# Vmr.Sdl2.Net

[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/vitimiti/Vmr.Sdl2.Net/activity) ![Maintainer](https://img.shields.io/badge/maintainer-Victor_Matia-blue)

[![LGPLv3 license](https://img.shields.io/badge/License-LGPLv3-blue.svg)](http://perso.crans.org/besson/LICENSE.html) [![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)](https://github.com/Naereen/badges/) [![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)

![LGPLv3](https://www.gnu.org/graphics/lgplv3-with-text-154x68.png)

---

This is a set of libraries to import the SDL2 libraries into modern dotnet.

All objects that should normally be created and destroyed in SDL2 now instead use the `using` pattern in dotnet, and
error handler delegates have been created to allow choosing the way errors are managed. By default,
the `Vmr.Sdl2.Net.Utilities.Error.DefaultHandler` and the `Vmr.Sdl2.Net.Utilities.Error.DefaultHandlerWithCode` simply
throw an exception for any error. For example, instead of writing:

```c
int code = SDL_Init(SDL_INIT_VIDEO);
if (code < 0)
{
    printf("Error: %s", SDL_GetError());
    return code;
}

SDL_Window* window = SDL_CreateWindow("title", x, y, w, h, SDL_WINDOW_SHOWN);
if (window == NULL)
{
    printf("Error: %s", SDL_GetError());
    return EXIT_FAILURE;
}

// ...

SDL_DestroyWindow(window);
SDL_Quit();
```

You can instead write:

```csharp
using Application app = new(ApplicationSubsystems.Video, Error.DefaultCodeHandler);
using Window window = new("title", new Point(x, y), new Size(w, h), WindowOptions.Shown, Error.DefaultCodeHandler);
```

At the same time, the `Vmr.Sdl2.Net.Application` class can be inherited to make, for example, a game:

```csharp
internal class Game : Application(ApplicationSubsystems.Video)
{
    protected override void Init()
    {
        base.Init();
        
        // Initialize what you require here.
    }
    
    protected override void Load()
    {
        base.Load();
        
        // Load what you require here.
    }
    
    protected override void Update()
    {
        base.Update();
        
        // Do your update logic here, this goes in the main loop.
    }
    
    protected override void Dispose(bool disposing)
    {
        // Dispose my disposable classes here.
        
        base.Dispose(disposing);
    }
}

public static class Program
{
    public static void Main()
    {
        using Game game = new();
        game.run();
    }
}
```

Refer to the [examples](#examples) for further ways this library can be used, including the aforementioned methods.

## Current Library Support:

- Vmr.Sdl2.Net - version 2.30.1
    - SDL2.dll
        - win-x86
        - win-x64
    - libSDL2.dylib
        - osx-x64
        - osx-arm64
    - libSDL2-2.0.so
        - linux-x64

## How to Use:

If the library is included through a NuGet package, you can use the `GeneratePathProperty="true"` option to easily
access the `targets\Vmr.Sdl2.Net.Runtime.targets` file and import it, which will import all the native runtimes
automatically both during `dotnet run` and `dotnet publish` scenarios. If the library has been included locally, for
example by adding the GitHub repository as a submodule or by downloading the source code, you can use
the `targets\Vmr.Sdl2.Net.LocalRuntime.targets` to achieve the same, as long as you run all commands from the solution,
as the local target depends on the solution directory and may break otherwise.

For example, if you have a project that has installed this library as a NuGet package, you may do:

```xml

<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)' == 'Debug'">
        <DefineDebug>true</DefineDebug>
        <DebugSymbols>true</DebugSymbols>
        <DebugType>full</DebugType>
        <Optimize>false</Optimize>
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)' == 'Release'">
        <DefineDebug>false</DefineDebug>
        <DebugSymbols>false</DebugSymbols>
        <DebugType>none</DebugType>
        <Optimize>true</Optimize>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Vmr.Sdl2.Net" Version="2.30.1" GeneratePathProperty="true"/>
    </ItemGroup>

    <Import Project="$(PkgVmr_Sdl2_Net)\targets\Vmr.Sdl2.Net.Runtime.targets"/>

</Project>
```

But if you had included the source files, you may instead follow one of the examples, and do something such as:

```xml

<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)' == 'Debug'">
        <DefineDebug>true</DefineDebug>
        <DebugSymbols>true</DebugSymbols>
        <DebugType>full</DebugType>
        <Optimize>false</Optimize>
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)' == 'Release'">
        <DefineDebug>true</DefineDebug>
        <DebugSymbols>true</DebugSymbols>
        <DebugType>none</DebugType>
        <Optimize>true</Optimize>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="$(PathToSdl2Project)\Vmr.Sdl2.Net\Vmr.Sdl2.Net.csproj"/>
    </ItemGroup>

    <Import Project="$(PathToSdl2Project)\targets\Vmr.Sdl2.Net.LocalRuntime.targets"/>

</Project>

```

You may see any of the existing examples to further see the differences between the native library and this library and
how to better use its systems.

**Note**: the `Application` class adds a parameter to allow checking for SDL2 version mismatches as this library is
tightly coupled with one SDL2 version and the user of this library may want to ensure the versions are at the very least
compatible.

## Examples

These are a series of projects using the existing libraries to show examples on how you may want to use them, as well as
showcase similarities and differences with the native library API.

These examples somewhat lack error checking and assume the usage of the critical error to stop errors from propagating
and crashing the program on any error instead. You may want to take a different approach depending on your project's
needs.

### Example 001:

A rough, simple example on how to create a basic window and handle the quit events.

### Example 002:

A more refined example on how to load a BMP file and display it into the window's screen surface. This makes use of the
inherited properties of the `Application` class.

### Example 003:

Practically identical to [Example 002](#example-002), but further adds onto the event system.

### Example 004:

Similar to [Example 003](#example-003), this project shows how to use events, but also how to load multiple textures and
dispose them safely.

## The Code Of Conduct

Any contributor or participant in this project must adhere to
the [Covenant Contributor Code of Conduct](CODE_OF_CONDUCT.md).

## TODO:

- Add public documentation (XML docs)
- Finish the main library
- Add other SDL2 libraries
- Finish examples once library is ready
