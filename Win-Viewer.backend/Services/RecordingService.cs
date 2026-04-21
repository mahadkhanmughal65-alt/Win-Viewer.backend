using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SIPSorcery.Net;
namespace Win_Viewer.backend.Services;

public interface IRecordingService
{
    Task StartRecordingAsync(string sessionId, RTCPeerConnection peerConnection);
    Task StopRecordingAsync(string sessionId);
    string? GetRecordingPath(string sessionId);
}

public class FFmpegRecordingService : IRecordingService
{
    private readonly Dictionary<string, Process> _ffmpegProcesses = new();

    public async Task StartRecordingAsync(string sessionId, RTCPeerConnection peerConnection)
    {
        // 1. Connect peer media to ffmpeg stdin or a temp file
        // 2. Run ffmpeg process with audio+video input and output file
        // 3. Save PID to _ffmpegProcesses[sessionId]
        throw new NotImplementedException("Recording not yet implemented. Connect peer media to FFmpeg.");
    }

    public async Task StopRecordingAsync(string sessionId)
    {
        if (_ffmpegProcesses.TryGetValue(sessionId, out var proc))
        {
            proc.Kill();
            _ffmpegProcesses.Remove(sessionId);
        }
    }

    public string? GetRecordingPath(string sessionId)
    {
        // Return absolute path of recorded file
        return $"/recordings/{sessionId}.mp4";
    }
}
