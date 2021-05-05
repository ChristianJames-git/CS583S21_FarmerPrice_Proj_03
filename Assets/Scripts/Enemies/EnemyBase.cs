using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed;
    public float health;
    public int damage;

    protected Vector3 target;
    protected int pointIndex;

    protected int enemyMoneyDrop;

    protected void Start()
    {
        //grab the first target to move towards
        pointIndex = 0;
        target = Waypoints.points[pointIndex].position;
        damage = 1;
    }

    /**
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
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }*/

    protected void FindWayPoint()
    {
        // check if this enemy has reached the target
        if (Vector3.Distance(transform.position, target) <= 0.2f)
        {
            //find the next target
            pointIndex++;
            if (Waypoints.points.Length > pointIndex)
            {
                target = findTarget();
                RotateToFace();
            }

            else
            {
                PlayerInfo.instance.DamageCore(damage);
                Destroy(this.gameObject);
            }
        }
    }

    protected abstract Vector3 findTarget();

    //method to deal with the enemy taking damage
    public void Hit(float damage)
    {
        //subtract the amount of damage form the enemy health
        health -= damage;

        //check if the enemy has died
        if (health <= 0)
        {
            //line below adds the enemyMoneyDrop value to the players balance when the enemy dies
            CurrencyManager.instance.inputMoney(enemyMoneyDrop);
            Destroy(this.gameObject);
        }

    }
}
