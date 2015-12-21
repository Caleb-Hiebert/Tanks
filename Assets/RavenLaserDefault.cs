using UnityEngine;
using System.Collections;
using System;

public class RavenLaserDefault : RavenLaser {

    public HDLine laserLine;
    public ParticleSystem laserParticles;

    public float emissionAmount;

    public override void LaserOff()
    {
        laserLine.lr.enabled = false;

        laserParticles.emissionRate = 0;
    }

    void LateUpdate()
    {
        laserLine.origin = laserOrigin;
        laserLine.endPoint = laserEnd;
    }

    public override void LaserOn()
    {
        laserLine.lr.enabled = true;

        laserParticles.emissionRate = emissionAmount;
    }
}
