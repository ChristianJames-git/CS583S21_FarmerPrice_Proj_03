using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float explosionRadius;
    private float damage;
    public GameObject impactEffect;

    public void Follow(Transform newTarget, float newDamage, float damageRadius, float bulletSpeed)
    {
        target = newTarget;
        damage = newDamage;
        explosionRadius = damageRadius;
        speed = bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!GameManager.Instance.paused)
        {
            Vector3 dir = target.position - transform.position;
            float distPerFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distPerFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distPerFrame, Space.World);
            transform.LookAt(target);
        }
    }

    private void HitTarget()
    {
        Destroy(this.gameObject);
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        if (explosionRadius != 0)
            Explode();
        else
            Damage(target.gameObject.GetComponent<EnemyBase>());
    }
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
            if (collider.tag == "Enemy")
                Damage(collider.gameObject.GetComponent<EnemyBase>());
    }
    private void Damage(EnemyBase enemy)
    {
        enemy.health -= damage;
    }
}
