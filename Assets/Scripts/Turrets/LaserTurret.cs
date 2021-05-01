public class LaserTurret : TurretBase
{
    //Stats by level
    private float[] damages = new float[3] { 4, 5.5f, 9 };
    private float[] fireRates = new float[3] { 3, 5, 8 };
    private float[] turretRanges = new float[3] { 4, 4, 5 };
    private float bulletDamageRadii = 0;
    private float bulletSpeeds = 40;

    public LaserTurret()
    {
        targetType = 3;
        useLaser = true;
        bulletSpeed = bulletSpeeds;
        bulletDamageRadius = bulletDamageRadii;
    }
    public override void UpdateStats(int turretLevel)
    {
        damage = damages[turretLevel];
        fireRate = fireRates[turretLevel];
        range = turretRanges[turretLevel];
    }
}
