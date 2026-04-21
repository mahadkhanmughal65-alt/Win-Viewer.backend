using System;
using Microsoft.AspNetCore.SignalR;
using Win_Viewer.backend.Services;
using Win_Viewer.backend.Models;
using SIPSorcery.Net;
using System.Threading.Tasks;
namespace Win_Viewer.backend.Hubs;

public class SessionHub : Hub
{


    private readonly WebRtcService _rtcService;

    public SessionHub(WebRtcService rtcService)
        => _rtcService = rtcService;

    public async Task SendOffer(string sessionId, string offerSdp)
    {
        var session = _rtcService.CreateOrGetSession(sessionId);
        var desc = SDP.ParseSDPDescription(offerSdp);
        await session.Peer.SetRemoteDescription(RTCSdpType.offer, desc.ToString());

        var answerSdp = session.Peer.createAnswer(null);
        await session.Peer.setLocalDescription(answerSdp);

        await Clients.Caller.SendAsync("ReceiveAnswer", answerSdp.ToString());
    }

    public async Task SendIce(string sessionId, string candidateSdp)
    {
        var session = _rtcService.GetSession(sessionId);
        if (session != null)
        {
            var ice = RTCIceCandidateInit.Parse(candidateSdp);
            session.Peer.addIceCandidate(ice);
        }
    }
}

