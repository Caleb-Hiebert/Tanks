using UnityEngine;
using System.Collections;

public class KennethData : DataBase {

    public static KennethData data;

    public GameObject redDragonKenneth;

    public float rocketCooldown, bombGenerationTime, rocketLifeTime, rocketTurnSpeed, bombLifeTime, bombRadius, bombSetupTime;
    public int carryingCapacity, rocketDamage, bombDamage;

	void Awake () {
        data = this;
	}
}
