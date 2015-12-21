using UnityEngine;
using System.Collections;

public class TerrainType : MonoBehaviour {

    public TerrainTypes type;
    public float angular, velocity;
    public Color color = Color.white;

    public struct TerrainData
    {
        public TerrainTypes type;
        public float drag;
        public float angularDrag;
        public Color color;

        public TerrainData(TerrainTypes type, float ang, float vel, Color color)
        {
            this.type = type;
            angularDrag = ang;
            drag = vel;
            this.color = color;
        }
    }

    public enum TerrainTypes
    {
        Default, Water, Ice, Grass, Dirt, Snow, Sand, Stone
    }

    public TerrainData Data
    {
        get { return new TerrainData(type, angular, velocity, color); }
    }
}
