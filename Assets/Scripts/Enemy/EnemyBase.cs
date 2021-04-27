using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public int health;

    private Transform target;
    private int pointIndex;

    private void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex];
    }

    private void FixedUpdate()
    {
        //direction vector points form this object to the target
        Vector3 dir = target.position - transform.position;

        //move the enemy
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            

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
}
