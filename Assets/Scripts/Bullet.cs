using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float speed = 20f;
    private float damage;
    public void Follow (Transform newTarget, float newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distPerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distPerFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distPerFrame, Space.World);
    }

    private void HitTarget()
    {
        EnemyBase enemy = target.gameObject.GetComponent<EnemyBase>();
        enemy.health -= damage;
        Destroy(gameObject);
    }
}
