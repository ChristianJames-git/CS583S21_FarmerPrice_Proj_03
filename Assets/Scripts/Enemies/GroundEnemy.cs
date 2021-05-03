using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected int coinDrop = 50;
    protected GroundEnemy()
    {
        enemyMoneyDrop = coinDrop;
        speed = 1.5f;
    }

    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position;
    }
}
