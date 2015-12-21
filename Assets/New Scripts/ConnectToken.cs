using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UdpKit;

class ConnectToken : Bolt.IProtocolToken
{
    public string name;

    public void Read(UdpPacket packet)
    {
        name = packet.ReadString();
    }

    public void Write(UdpPacket packet)
    {
        packet.WriteString(name);
    }
}
