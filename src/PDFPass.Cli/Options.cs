using CommandLine;

namespace PDFPass.Cli;

internal abstract class FileOptions
{
    [Option('i', "input", Required = true)]
    public string Input { get; set; } = string.Empty;

    [Option('o', "output", Required = true)]
    public string Output { get; set; } = string.Empty;

    [Option("force")]
    public bool Force { get; set; }
}

[Verb("encrypt")]
internal sealed class EncryptOptions : FileOptions
{
    [Option("user-password")]
    public string? UserPassword { get; set; }

    [Option("owner-password")]
    public string? OwnerPassword { get; set; }

    [Option("watermark")]
    public string? Watermark { get; set; }
}

[Verb("decrypt")]
internal sealed class DecryptOptions : FileOptions
{
    [Option("password")]
    public string? Password { get; set; }
}

[Verb("watermark")]
internal sealed class WatermarkOptions : FileOptions
{
    [Option("text", Required = true)]
    public string Text { get; set; } = string.Empty;
}
