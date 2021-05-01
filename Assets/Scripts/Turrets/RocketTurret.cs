public class RocketTurret : TurretBase
{
    //Stats by level
    private float[] damages = new float[3] { 24, 40, 48 };
    private float[] fireRates = new float[3] { 0.25f, 0.4f, 0.75f };
    private float[] turretRanges = new float[3] { 3, 5, 8 };
    private float[] bulletDamageRadii = new float[3] { 1, 1.5f, 2 };
    private float[] bulletSpeeds = new float[3] { 5, 8, 10 };

    public override void UpdateStats(int turretLevel)
    {
        damage = damages[turretLevel];
        fireRate = fireRates[turretLevel];
        range = turretRanges[turretLevel];
        bulletDamageRadius = bulletDamageRadii[turretLevel];
        bulletSpeed = bulletSpeeds[turretLevel];
    }
}
