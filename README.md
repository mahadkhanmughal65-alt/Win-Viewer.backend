# Win-Viewer

Win-Viewer is an ASP.NET Core backend for a remote device viewing and session-signaling workflow. The current codebase focuses on device discovery, in-memory session management, and WebRTC signaling, with service scaffolding in place for audio relay, recording, Wake-on-LAN, and printer relay.

## Status

This project is an early-stage backend foundation. Core API and hub plumbing are present, while some platform-specific media and printing features are still stubs.

## Tech Stack

- .NET 9 / ASP.NET Core Web API
- SignalR
- Microsoft OpenAPI support for development-time API docs
- SIPSorcery WebRTC

## Repository Layout

```text
.
|-- Win-Viewer.slnx
|-- README.md
`-- Win-Viewer.backend/
    |-- Controllers/
    |-- Hubs/
    |-- Models/
    |-- Services/
    |-- Program.cs
    `-- Win-Viewer.backend.csproj
```

## What It Currently Does

- Registers devices with metadata such as name, platform, tag, and last-seen IP.
- Lists all registered devices from an in-memory registry.
- Hosts a SignalR hub for WebRTC offer/answer and ICE exchange.
- Tracks sessions in memory.
- Exposes OpenAPI metadata when the app runs in the Development environment.

## Prerequisites

- .NET 9 SDK

## Running Locally

From the solution root:

```bash
dotnet restore Win-Viewer.slnx
dotnet build Win-Viewer.slnx
dotnet run --project Win-Viewer.backend/Win-Viewer.backend.csproj --launch-profile http
```

Default development URLs from `launchSettings.json`:

- `http://localhost:5266`
- `https://localhost:7153`

## API Surface

### REST API

`POST /api/device/register`

Registers or updates a device in the in-memory registry.

Example request body:

```json
{
  "deviceId": "pc-01",
  "name": "Office Workstation",
  "platform": "Windows",
  "tag": "desktop",
}
```

`GET /api/device`

Returns the list of currently registered devices.

### SignalR Hub

Hub route: `/hubs/session`

Client-to-server methods:

- `SendOffer(sessionId, offerSdp)` creates or reuses a WebRTC session and replies to the caller with `ReceiveAnswer`.
- `SendIce(sessionId, candidateSdp)` adds an ICE candidate to an existing session.

## Current Limitations

- Device and session data are stored in memory only, so app restarts clear all state.
- No authentication or authorization is implemented yet.
- `NAudioRelayService` is a placeholder and currently throws `NotImplementedException`.
- `FFmpegRecordingService` is scaffolded but recording is not implemented yet.
- `WindowsPrinterRelayService` is scaffolded but printing is not implemented yet.
- Wake-on-LAN logic exists as a service, but no controller endpoint currently exposes it.

## Development Notes

- OpenAPI is mapped only in the Development environment.
- The current backend is a good foundation for pairing with a desktop or web client that handles screen capture, control input, and session orchestration.
