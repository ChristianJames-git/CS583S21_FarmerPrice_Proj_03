using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    protected Transform target;

    [Header("Attributes")]
    protected float damage;
    protected float range;
    protected float fireRate;
    private float fireCountdown = 0;
    protected float bulletDamageRadius;
    protected float bulletSpeed;

    protected int targetType; //0:Nearest ; 1:Oldest ; 2:Strongest ; 3:RandomInRange

    [Header("References")]
    public Transform PartToRotate;
    public Transform firePoint;
    protected float turnSpeed = 10f;
    public GameObject bulletPrefab;

    public LineRenderer laserRend;
    protected bool useLaser;



    // Start is called before the first frame update
    protected void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (GameManager.Instance.paused == false)
        {
            if (target == null)
            {
                if (useLaser && laserRend.enabled)
                    laserRend.enabled = false;
                return;
            }

            RotateToTarget();

            if (useLaser)
                Laser();
            else
            {
                if (fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }
    }

    private void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected void FindTarget ()
    {
        switch (targetType)
        {
            case 1:
                target = GameManager.Instance.FindOldestEnemy(range, transform);
                break;
            case 2:
                target = GameManager.Instance.FindStrongestEnemy(range, transform);
                break;
            case 3:
                target = GameManager.Instance.FindRandomEnemyInRange(range, transform);
                break;
            default:
                target = GameManager.Instance.FindNearestEnemy(range, transform);
                break;
        }
    }

    //Range bubble draw
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, BuildManager.instance.Bullets);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Follow(target, damage, bulletDamageRadius, bulletSpeed);
    }

    private void Laser()
    {
        if (!laserRend.enabled)
            laserRend.enabled = true;
        laserRend.SetPosition(0, firePoint.position);
        laserRend.SetPosition(1, target.position);
        if (fireCountdown <= 0)
        {
            target.gameObject.GetComponent<EnemyBase>().Hit(damage);
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    public abstract void UpdateStats(int turretLevel);
}
