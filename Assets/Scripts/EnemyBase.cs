using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float health = 200;

    protected Transform target;
    protected int pointIndex;

    protected void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex];
    }

    protected void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        //direction vector points form this object to the target
        Vector3 dir = target.position - transform.position;


        //move the enemy if not in the map
        if (GameObject.Find("MapManager").GetComponent<MapManager>().inMap == false)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        FindWayPoint();
    }
    protected void FindWayPoint()
    {
        //check if this enemy has reached the target
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            //find the next target
            pointIndex++;
            if (Waypoints.points.Length > pointIndex)
                target = Waypoints.points[pointIndex];
            else
                Destroy(this.gameObject);
        }
    }

    //method to deal with the enemy taking damage
    public void Hit(int damage)
    {
        //subtract the amount of damage form the enemy health
        health -= damage;

        //check if the enemy has died
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
