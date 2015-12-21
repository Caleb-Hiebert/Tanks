using UnityEngine;

class RavenData : DataBase
{
    public static RavenData data;

    public GameObject classyRaven;

    public float laserTicksPerSecond, invisibilityCooldown, invisibilityDuration, flashCooldown, flashDistance, laserDistance;
    public int laserDamagePerTick;

    void Awake()
    {
        data = this;
    }
}
