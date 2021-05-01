using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : TurretBase
{
    //Stats by level
    private float[] damages = new float[3] { 3, 4.5f, 7 };
    private float[] fireRates = new float[3] { 3, 5, 8 };
    private float[] turretRanges = new float[3] { 4, 4, 5 };
    //private float bulletDamageRadii = 0;
    private float bulletSpeeds = 40;

    public override void UpdateStats(int turretLevel)
    {
        base.UpdateStats(turretLevel);
        damage = damages[turretLevel];
        fireRate = fireRates[turretLevel];
        range = turretRanges[turretLevel];
        bulletSpeed = bulletSpeeds;
    }
}
