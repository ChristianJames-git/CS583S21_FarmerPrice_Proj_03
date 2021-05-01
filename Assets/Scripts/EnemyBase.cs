using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float health = 200;

    protected Vector3 target;
    protected int pointIndex;

    protected void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex].position;
    }

    protected void FixedUpdate()
    {
        FindWayPoint();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        //direction vector points form this object to the target
        Vector3 dir = target - transform.position;


        //move the enemy if not in the map
        if (GameObject.Find("MapManager").GetComponent<MapManager>().inMap == false)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    protected void FindWayPoint()
    {
        // check if this enemy has reached the target
        if (Vector3.Distance(transform.position, target) <= 0.2f)//enemy is flying so it will always be above the waypoints
        {
            //find the next target
            pointIndex++;
            if (Waypoints.points.Length > pointIndex)
            {
                target = Waypoints.points[pointIndex].position;
            }

            else
                Destroy(this.gameObject);
        }
    }

    //method to deal with the enemy taking damage
    public void Hit(float damage)
    {
        //subtract the amount of damage form the enemy health
        health -= damage;

        //check if the enemy has died
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
