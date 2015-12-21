using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UdpKit;

class ServerInfoToken : Bolt.IProtocolToken
{
    public string mapInfo;

    public void Read(UdpPacket packet)
    {
        mapInfo = packet.ReadString();
    }

    public void Write(UdpPacket packet)
    {
        packet.WriteString(mapInfo);
    }
}
