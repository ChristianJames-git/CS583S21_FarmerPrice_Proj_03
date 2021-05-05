using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    public float flightHeight;
    protected int coinDrop = 75;

    protected new void Start()
    {
        //grab the first target to move towards
        //pointIndex = 0;
        target = Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0);

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y + flightHeight, transform.position.z);

        enemyMoneyDrop = coinDrop;
        speed = 2;
        damage = 1;
    }

    protected void FixedUpdate()
    {
        FindWayPoint();

        if (health <= 0)
        {
            AudioManager._instance.PlaySound("FlyingEnemyDeathSound");
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
        return Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0);
    }

    //rotates the object to be facing the waypoint(The direction it is moving)
    protected override void RotateToFace()
    {
        //find the direction
        Vector3 dir = transform.position - target;
        Quaternion rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        transform.rotation *= Quaternion.Euler(0, 180, 0); // this adds a 180 degrees Y rotation
        //transform.Rotate(dir);
    }
}
