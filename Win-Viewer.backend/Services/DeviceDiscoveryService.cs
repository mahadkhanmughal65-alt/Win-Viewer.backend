using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Win_Viewer.backend.Models;
namespace Win_Viewer.backend.Services;

public class DeviceDiscoveryService
{
    // thread-safe device registry
    private readonly ConcurrentDictionary<string, DeviceInfo> _devices = new();

    public void RegisterOrUpdate(DeviceInfo device)
    {
        device.LastSeenUtc = DateTime.UtcNow;
        _devices[device.DeviceId] = device;
    }

    public IEnumerable<DeviceInfo> ListAll() => _devices.Values;

    public DeviceInfo? GetDevice(string deviceId)
        => _devices.TryGetValue(deviceId, out var dev) ? dev : null;
}
