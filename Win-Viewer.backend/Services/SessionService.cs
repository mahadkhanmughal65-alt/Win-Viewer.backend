using System;
using System.Collections.Concurrent;
namespace Win_Viewer.backend.Services;

public enum SessionRole { Owner, Controller, Viewer }

public class SessionUser
{
    public string ConnectionId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public SessionRole Role { get; set; }
    public string DeviceId { get; set; } = string.Empty;
}

public class SessionInfo
{
    public string SessionId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ConcurrentDictionary<string, SessionUser> Users { get; set; } = new();
    public bool IsRecording { get; set; } = false;
    // Add more: session settings, host device, etc.
}

public class SessionService
{
    // sessionId -> SessionInfo
    private readonly ConcurrentDictionary<string, SessionInfo> _sessions = new();

    public SessionInfo GetOrCreate(string sessionId)
        => _sessions.GetOrAdd(sessionId, id => new SessionInfo { SessionId = id });

    public void AddUser(string sessionId, SessionUser user)
    {
        var session = GetOrCreate(sessionId);
        session.Users[user.ConnectionId] = user;
    }

    public void RemoveUser(string sessionId, string connectionId)
    {
        if (_sessions.TryGetValue(sessionId, out var session))
            session.Users.TryRemove(connectionId, out var _);
    }

    public SessionInfo? GetSession(string sessionId)
        => _sessions.TryGetValue(sessionId, out var session) ? session : null;

    public IEnumerable<SessionUser> GetUsers(string sessionId)
        => GetSession(sessionId)?.Users.Values ?? [];

    public IEnumerable<string> AllSessionIds() => _sessions.Keys;
}
