using System;
using System.Threading.Tasks;
using SIPSorcery.Net;
namespace Win_Viewer.backend.Services;


public interface IAudioRelayService
{
    // Called to start streaming audio to a WebRTC track or DataChannel
    Task StartStreamAsync(string sessionId, RTCPeerConnection peerConnection);
    Task StopStreamAsync(string sessionId);
}

public class NAudioRelayService : IAudioRelayService
{
    // Inject logger etc.
    public async Task StartStreamAsync(string sessionId, RTCPeerConnection peerConnection)
    {
        // Use NAudio to capture microphone/speaker
        // Encode to PCM/Opus
        // Pipe to WebRTC peerConnection.AddTrack(audioTrack)
        throw new NotImplementedException("Windows audio relay: implement with NAudio + WebRTC audio track.");
    }

    public async Task StopStreamAsync(string sessionId)
    {
        // Teardown streams
    }
}

