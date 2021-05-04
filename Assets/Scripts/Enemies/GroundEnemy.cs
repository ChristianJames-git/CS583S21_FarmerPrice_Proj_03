using UnityEngine;

public class GroundEnemy : EnemyBase
{
    protected int coinDrop = 50;

    protected new void Start()
    {
        //grab the first target to move towards
        //pointIndex = 0;
        target = Waypoints.points[pointIndex].position + new Vector3(0, -0.3f, 0);

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);

        enemyMoneyDrop = coinDrop;
        speed = 1.5f;
        damage = 1;
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
        return Waypoints.points[pointIndex].position + new Vector3(0, -0.3f, 0);
    }

    protected override void RotateToFace()
    {
        //find the direction
        Vector3 dir = transform.position - target;
        Quaternion rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        transform.rotation *= Quaternion.Euler(0, 180, 0); // this adds a 180 degrees Y rotation
        transform.Rotate(dir);
    }
}
