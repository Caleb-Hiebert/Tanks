using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TankData
{
    public enum Tanks
    {
        Gary, FrostGary, Hammerhead, MinerHammerhead, Kenneth, RedDragonKenneth, Raven, ClassyRaven
    }

    public struct StateData
    {
        public int state, skin;
        
        public StateData(int state, int skin)
        {
            this.state = state;
            this.skin = skin;
        }
    }

    public static DataBase GetTankData(Tanks tank)
    {
        if(tank == Tanks.Gary || tank == Tanks.FrostGary)
        {
            return GaryData.data;
        } else if (tank == Tanks.Hammerhead || tank == Tanks.MinerHammerhead)
        {
            return HammerheadData.data;
        } else if (tank == Tanks.Kenneth || tank == Tanks.RedDragonKenneth)
        {
            return KennethData.data;
        } else if (tank == Tanks.Raven || tank == Tanks.ClassyRaven)
        {
            return RavenData.data;
        }

        return null;
    }

    public static StateData GetState(Tanks t)
    {
        switch(t)
        {
            case Tanks.Gary: return new StateData(2, 0);
            case Tanks.FrostGary: return new StateData(2, 1);
            case Tanks.Hammerhead: return new StateData(3, 0);
            case Tanks.MinerHammerhead: return new StateData(3, 1);
            case Tanks.Kenneth: return new StateData(4, 0);
            case Tanks.RedDragonKenneth: return new StateData(4, 1);
            case Tanks.Raven: return new StateData(5, 0);
            case Tanks.ClassyRaven: return new StateData(5, 1);
        }

        return new StateData(0, 0);
    }

    public static Tanks Parse(string tank)
    {
        return (Tanks)Enum.Parse(typeof(Tanks), tank);
    }
}
