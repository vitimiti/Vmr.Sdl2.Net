// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Runtime.InteropServices;
using System.Text;

using Example.Program002.Settings;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program004;

public class Game : IDisposable
{
    private readonly Application? _application;
    private readonly Surface? _screenSurface;
    private readonly Dictionary<TextureType, Texture> _textures = new();
    private readonly Window? _window;
    private Texture? _currentTexture;
    private bool _quit;

    public Game(WindowSettings windowSettings)
    {
        _application = new Application(
            ApplicationSubsystems.Video,
            CriticalErrorWithCode,
            (expectedVersion, version) =>
                MessageBox.Show(
                    MessageBoxOptions.Warning,
                    "SDL2 Version Mismatch",
                    GenerateMismatchInfo(expectedVersion, version),
                    null,
                    (_, _) => Console.WriteLine(GenerateMismatchInfo(expectedVersion, version))
                )
        );

        _window = new Window(
            windowSettings.Title,
            windowSettings.Position,
            windowSettings.Size,
            windowSettings.Flags,
            CriticalError
        );

        _screenSurface = _window.GetSurface(CriticalError);
    }

    public static ErrorHandler ErrorHandler { get; set; } = CriticalError;
    public static ErrorCodeHandler ErrorCodeHandler { get; set; } = CriticalErrorWithCode;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private static string GenerateMismatchInfo(Version expectedVersion, Version version)
    {
        return
            $"Expected SDL2 v{expectedVersion}, but SDL2 v{version} was found. Some unexpected behaviors or errors may be encountered, reinstall the game or update your SDL2 version to match v{expectedVersion}";
    }

    private static void CriticalError(string? message)
    {
        MessageBox.Show(
            MessageBoxOptions.Error,
            "Critical Error",
            message ?? string.Empty,
            null,
            (innerMessage, code) =>
                throw new Exception(message, new ExternalException(innerMessage, code))
        );

        throw new ExternalException(message);
    }

    private static void CriticalErrorWithCode(string? message, int code)
    {
        StringBuilder messageBuilder = new();
        messageBuilder.AppendLine(message);
        messageBuilder.AppendLine($"Return Code: 0x{code:X8}");
        MessageBox.Show(
            MessageBoxOptions.Error,
            "Critical Error",
            messageBuilder.ToString(),
            null,
            (innerMessage, innerCode) =>
                throw new Exception(
                    messageBuilder.ToString(),
                    new ExternalException(innerMessage, innerCode)
                )
        );

        throw new ExternalException(message, code);
    }

    private void Init()
    {
        MainLoop.OnQuit += (_, _) => _quit = true;
        MainLoop.OnKeyDown += (_, eventArgs) =>
        {
            _currentTexture = eventArgs.KeySymbol.KeyCode switch
            {
                KeyCode.Escape => _textures[TextureType.Default],
                KeyCode.Left => _textures[TextureType.Left],
                KeyCode.Up => _textures[TextureType.Up],
                KeyCode.Right => _textures[TextureType.Right],
                KeyCode.Down => _textures[TextureType.Down],
                _ => _currentTexture
            };
        };
    }

    private void Load()
    {
        _textures.Add(TextureType.Default, new Texture("res/press_me.bmp"));
        _textures.Add(TextureType.Left, new Texture("res/left.bmp"));
        _textures.Add(TextureType.Up, new Texture("res/up.bmp"));
        _textures.Add(TextureType.Right, new Texture("res/right.bmp"));
        _textures.Add(TextureType.Down, new Texture("res/down.bmp"));

        _currentTexture = _textures[TextureType.Default];
    }

    private void Update()
    {
        MainLoop.PollEvents();
        _currentTexture?.Update(_screenSurface!);
        _window?.UpdateSurface(CriticalErrorWithCode);
    }

    public void Run()
    {
        Init();
        Load();
        while (!_quit)
        {
            Update();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        foreach ((TextureType _, Texture texture) in _textures)
        {
            texture.Dispose();
        }

        _screenSurface?.Dispose();
        _window?.Dispose();
        _application?.Dispose();
    }
}
