using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : TurretBase
{
    //Stats by level
    private float[] damages = new float[3] { 5, 10, 18 };
    private float fireRates = 1;
    private float[] turretRanges = new float[3] { 3, 4, 5 };
    private float bulletDamageRadii = 0;
    private float bulletSpeeds = 20;

    public override void UpdateStats(int turretLevel)
    {
        damage = damages[turretLevel];
        fireRate = fireRates;
        range = turretRanges[turretLevel];
        bulletDamageRadius = bulletDamageRadii;
        bulletSpeed = bulletSpeeds;
    }
}
