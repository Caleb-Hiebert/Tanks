using UnityEngine;
using System.Collections;

public class HammerheadData : DataBase {

    public static HammerheadData data;

    public GameObject minerHammerhead;

    public float grappleCooldown, grappleBreakDistance, reelSpeed, reelBreakTime, explosivePrimingTime, earthquakeRadius, earthquakeCooldown, earthquakeWindupTime;
    public int grappleDamage, explosiveDamage, earthquakeDamage, explosiveCarryingCapacity;

    void Awake()
    {
        data = this;
    }
}
