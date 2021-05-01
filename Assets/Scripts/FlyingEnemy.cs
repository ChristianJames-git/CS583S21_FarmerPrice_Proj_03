using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{

    public float flightHeight;

    protected new void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0);

        //set the gameobject to be at the flying height
        transform.position = new Vector3(transform.position.x, transform.position.y + flightHeight, transform.position.z);
    }

    protected new void FixedUpdate()
    {
        FindWayPoint();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        //direction vector points form this object to the target
        Vector3 dir = target - transform.position;
        print(target.ToString());


        //move the enemy if not in the map
        if (GameObject.Find("MapManager").GetComponent<MapManager>().inMap == false)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    protected new void FindWayPoint()
    {
        // check if this enemy has reached the target
        if (Vector3.Distance(transform.position + (Vector3.down * flightHeight), target) <= 0.2f)//enemy is flying so it will always be above the waypoints
        {
            print("hello");
            //find the next target
            pointIndex++;
            if (Waypoints.points.Length > pointIndex)
            {
                target = Waypoints.points[pointIndex].position + new Vector3(0, flightHeight, 0); ;
            }
                
            else
                Destroy(this.gameObject);
        }
    }
}
