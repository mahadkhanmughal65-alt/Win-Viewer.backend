using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Win_Viewer.backend.Services;
public class WakeOnLan
{
    public void Wake(string macAddress, string broadcastIp)
    {
        var client = new UdpClient();
        client.Connect(IPAddress.Parse(broadcastIp), 9);
        // WoL magic packet
        var wol = new byte[102];
        for (int i = 0; i < 6; i++) wol[i] = 0xFF;
        for (int i = 1; i <= 16; i++)
            Array.Copy(PhysicalAddress.Parse(macAddress).GetAddressBytes(), 0, wol, i * 6, 6);
        client.Send(wol, wol.Length);
    }
}


