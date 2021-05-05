using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected int coinDrop = 50;
    protected GroundEnemy()
    {
        enemyMoneyDrop = coinDrop;
        speed = 1.5f;
    }

    protected void FixedUpdate()
    {
        FindWayPoint();

        if (health <= 0)
        {
            AudioManager._instance.PlaySound("GroundEnemyDeathSound");
            Destroy(this.gameObject);
            return;
        }

        //direction vector points form this object to the target
        Vector3 dir = target - transform.position;


        //move the enemy if not in the map
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position;
    }
}
