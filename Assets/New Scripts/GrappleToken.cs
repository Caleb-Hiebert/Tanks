using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UdpKit;
using UnityEngine;

class GrappleToken : Bolt.IProtocolToken
{
    public BoltEntity entityHit;
    public Vector2 entityLocalPosition;

    public BoltEntity owner;

    //if we didnt hit an entity we need a static point
    public Vector2 worldHitPoint;

    public void Read(UdpPacket packet)
    {
        entityHit = packet.ReadBoltEntity();
        entityLocalPosition = packet.ReadVector2();
        owner = packet.ReadBoltEntity();
        worldHitPoint = packet.ReadVector2();
    }

    public void Write(UdpPacket packet)
    {
        packet.WriteBoltEntity(entityHit);
        packet.WriteVector2(entityLocalPosition);
        packet.WriteBoltEntity(owner);
        packet.WriteVector2(worldHitPoint);
    }
}
