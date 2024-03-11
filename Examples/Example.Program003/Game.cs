// Copyright (c) 2024 Victor Matia <vmatir@gmail.com>

using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using Example.Program002.Settings;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Input.KeyboardUtilities;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video.Messages;
using Vmr.Sdl2.Net.Video.Windowing;

namespace Example.Program003;

public class Game : IDisposable
{
    private readonly Application? _application;
    private readonly Surface? _screenSurface;
    private readonly Window? _window;
    private bool _quit;
    private Surface? _xOut;
    private Rectangle _xOutDstRect = Rectangle.Empty;
    private FileStream? _xOutFileStream;
    private RwOps? _xOutRwOps;

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
            if (eventArgs.KeySymbol.KeyCode == KeyCode.Escape)
            {
                _quit = true;
            }
        };
    }

    private void Load()
    {
        _xOutFileStream = new FileStream("res/x_out.bmp", FileMode.Open, FileAccess.Read);
        _xOutRwOps = new RwOps(_xOutFileStream, CriticalError);
        _xOut = Surface.LoadBmp(_xOutRwOps, CriticalError);
    }

    private void Update()
    {
        MainLoop.PollEvents();
        _xOut?.Blit(
            Rectangle.Empty,
            _screenSurface!,
            ref _xOutDstRect,
            false,
            CriticalErrorWithCode
        );

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

        _xOut?.Dispose();
        _xOutRwOps?.Dispose();
        _xOutFileStream?.Dispose();
        _screenSurface?.Dispose();
        _window?.Dispose();
        _application?.Dispose();
    }
}
