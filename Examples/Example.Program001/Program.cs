using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using Vmr.Sdl2.Net;
using Vmr.Sdl2.Net.Graphics;
using Vmr.Sdl2.Net.Utilities;
using Vmr.Sdl2.Net.Video;
using Vmr.Sdl2.Net.Video.Messages;

try
{
    using Application app =
        new(
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

    using Window window =
        new(
            "SDL Tests",
            new Point(WindowPredefinedPosition.Centered),
            new Size(640, 480),
            WindowOptions.Shown,
            CriticalError
        );

    using Surface? screenSurface = window.GetSurface(Error.DefaultHandler);
    screenSurface?.Fill(
        Rectangle.Empty,
        screenSurface.PixelFormat?.MapRgb(Color.DarkOrange) ?? 0U,
        CriticalErrorWithCode
    );

    window.UpdateSurface(Error.DefaultHandlerWithCode);

    Thread.Sleep(TimeSpan.FromSeconds(3));
}
catch (Exception e)
{
    MessageBox.Show(
        MessageBoxOptions.Error,
        "Unmanaged Exception",
        e.ToString(),
        null,
        (_, _) => Console.Error.WriteLine(e)
    );
}

return;

static string GenerateMismatchInfo(Version expectedVersion, Version version)
{
    return
        $"Expected SDL2 v{expectedVersion}, but SDL2 v{version} was found. Some unexpected behaviors or errors may be encountered, reinstall the game or update your SDL2 version to match v{expectedVersion}";
}

static void CriticalError(string? message)
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

static void CriticalErrorWithCode(string? message, int code)
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
