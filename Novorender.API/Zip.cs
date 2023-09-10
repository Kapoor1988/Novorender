using System.IO.Compression;

namespace Novorender.API;
public static class Zip
{
    // Write a zip file with specified number of files of 1 MiB each with random content.
    public static async Task WriteAsync(Stream stream, int files, CancellationToken cancellationToken = default)
    {
        var seed = 123;
        var rnd = new Random(seed);
        using ZipArchive archive = new(stream, ZipArchiveMode.Create, true);
        byte[] buffer = new byte[0x100000];
        for (int i = 0; i < files; i++)
        {
            cancellationToken.ThrowIfCancellationRequested(); // Check for cancellation
            rnd.NextBytes(buffer);
            var name = i.ToString();
            var entry = archive.CreateEntry(name, CompressionLevel.Optimal);
            using var entryStream = entry.Open();
            await entryStream.WriteAsync(buffer, cancellationToken);
        }
    }
}
