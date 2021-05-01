using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{

    public float flightHeight;

    private void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex];

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y + flightHeight, transform.position.z);
    }

    protected void FindWaypoint()
    {
        // check if this enemy has reached the target
        if (Vector3.Distance(transform.position - new Vector3(0, -flightHeight, 0), target.position) <= 0.2f)//enemy is flying so i will always be above the waypoints
        {
            //find the next target
            pointIndex++;
            if (Waypoints.points.Length > pointIndex)
                target = Waypoints.points[pointIndex];
            else
                Destroy(this.gameObject);
        }
    }
}
