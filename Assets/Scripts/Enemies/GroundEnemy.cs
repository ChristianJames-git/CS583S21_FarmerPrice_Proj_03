using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected int coinDrop = 50;
    protected GroundEnemy()
    {
        enemyMoneyDrop = coinDrop;
        speed = 1.5f;
    }

    protected new void Start()
    {
        //grab the first target to move towards
        //pointIndex = 0;
        target = Waypoints.points[pointIndex].position + new Vector3(0, -0.5f, 0);

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position + new Vector3(0, -0.5f, 0);
    }
}
