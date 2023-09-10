namespace Novorender.Api.Tests;

using Novorender.API;
using System.IO.Compression;


[TestFixture]
public class ZipTests
{
    [Test]
    public async Task WriteAsync_CreatesZipFileWithRandomContent()
    {
        // Arrange
        var stream = new MemoryStream();
        var numberOfFiles = 3;
        var cancellationToken = CancellationToken.None;


        // Act
        await Zip.WriteAsync(stream, numberOfFiles, cancellationToken);

        // Assert
        // Reset the stream position to the beginning for reading
        stream.Seek(0, SeekOrigin.Begin);

        // Create a ZipArchive from the stream to inspect its content
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Assert that the archive contains the expected number of entries
        Assert.That(archive.Entries, Has.Count.EqualTo(numberOfFiles));
    }

    [Test]
    public void WriteAsync_CancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        var stream = new MemoryStream();
        var numberOfFiles = 3;
        var cancellationToken = new CancellationToken(canceled: true);

        // Act and Assert
        Assert.ThrowsAsync<OperationCanceledException>(
            async () => await Zip.WriteAsync(stream, numberOfFiles, cancellationToken)
        );
    }

    [Test]
    public async Task WriteAsync_ZeroFiles_NoEntriesCreated()
    {
        // Arrange
        var stream = new MemoryStream();
        var numberOfFiles = 0;
        var cancellationToken = CancellationToken.None;

        // Act
        await Zip.WriteAsync(stream, numberOfFiles, cancellationToken);

        // Assert
        stream.Seek(0, SeekOrigin.Begin);
        using var zipStream = new ZipArchive(stream, ZipArchiveMode.Read);
        Assert.That(zipStream.Entries, Is.Empty);
    }
}