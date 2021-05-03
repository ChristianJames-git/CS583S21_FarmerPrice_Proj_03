using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected int coinDrop = 100;
    protected GroundEnemy()
    {
        enemyMoneyDrop = coinDrop;
    }

    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position;
    }
}
