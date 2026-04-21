using System;
using SIPSorcery.Net;
using System.Collections.Concurrent;
namespace Win_Viewer.backend.Services;

public class WebRtcSession
{
    public RTCPeerConnection Peer { get; set; }
    public string SessionId { get; set; }
}
public class WebRtcService
{
    private readonly ConcurrentDictionary<string, WebRtcSession> _sessions = new();

    public WebRtcSession CreateOrGetSession(string sessionId)
    {
        return _sessions.GetOrAdd(sessionId, sid =>
        {
            var pc = new RTCPeerConnection(null);
            // Add local video/audio tracks if providing media from server
            pc.OnICECandidate += (candidate) => {
                // send ICE to peer via SignalR!
            };
            return new WebRtcSession
            {
                Peer = pc,
                SessionId = sessionId
            };
        });
    }

    public WebRtcSession? GetSession(string sessionId)
        => _sessions.TryGetValue(sessionId, out var ws) ? ws : null;

    public void RemoveSession(string sessionId)
        => _sessions.TryRemove(sessionId, out var _);
}

