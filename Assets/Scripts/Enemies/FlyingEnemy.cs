using UnityEngine;

public class FlyingEnemy : EnemyBase
{

    public float flightHeight;

    protected new void Start()
    {
        //grab the first target to move towards
        //pointIndex = 0;
        target = Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0);

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y + flightHeight, transform.position.z);
    }

    protected override Vector3 findTarget()
    {
        return Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0);
    }
}
