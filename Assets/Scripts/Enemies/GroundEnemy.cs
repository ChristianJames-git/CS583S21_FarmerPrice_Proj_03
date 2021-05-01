using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position;
    }
}
