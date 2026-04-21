using System;

namespace Win_Viewer.backend.Models;


public class DeviceInfo
{
    public string DeviceId { get; set; }
    public string Name { get; set; }
    public string Platform { get; set; }
    public string Tag { get; set; }
    public string LastSeenIP { get; set; }
    public DateTime LastSeenUtc { get; set; }
}

public class SessionUser
{
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
    public string DeviceId { get; set; }
}

