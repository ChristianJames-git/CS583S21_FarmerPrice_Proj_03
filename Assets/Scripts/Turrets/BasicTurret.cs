public class BasicTurret : TurretBase
{
    //Stats by level
    private float[] damages = new float[3] { 10, 25, 65 };
    //private float fireRates = 1;
    private float[] turretRanges = new float[3] { 3, 4, 5 };
    //private float bulletDamageRadii = 0;
    private float bulletSpeeds = 20;

    public override void UpdateStats(int turretLevel)
    {
        base.UpdateStats(turretLevel);
        damage = damages[turretLevel];
        range = turretRanges[turretLevel];
        bulletSpeed = bulletSpeeds;
    }
}
