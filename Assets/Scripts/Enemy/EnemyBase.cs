using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 5f;

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
        dir.Normalize();

        //move the enemy
        transform.Translate(dir * speed * Time.deltaTime);

        //check if this game object has reached the end
        if (pointIndex >= Waypoints.points.Length)
            Destroy(this.gameObject);

        //check if this enemy has reached the target
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            //find the next target
            pointIndex++;
            target = Waypoints.points[pointIndex];
        }
    }
}
