using System;

namespace Win_Viewer.backend.Services;

public interface IPrinterRelayService
{
    Task RequestPrintAsync(string sessionId, string fileName, byte[] documentData);
}

public class WindowsPrinterRelayService : IPrinterRelayService
{
    public async Task RequestPrintAsync(string sessionId, string fileName, byte[] documentData)
    {
        // Save the incoming file to a temp path
        var tempPath = Path.Combine(Path.GetTempPath(), fileName);
        await File.WriteAllBytesAsync(tempPath, documentData);

        // Windows: Use System.Drawing.Printing
        // Linux: Use lpr
        throw new NotImplementedException("Print implementation required (platform-specific).");
    }
}
