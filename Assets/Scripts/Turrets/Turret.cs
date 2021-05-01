using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float damage = 5;
    public float range = 4f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public int targetType = 0; //0:Nearest ; 1:Oldest ; 2:Strongest
    
    public float bulletDamageRadius;
    public float bulletSpeed;

    [Header("References")]
    public Transform PartToRotate;
    public Transform firePoint;
    private float turnSpeed = 10f;
    public GameObject bulletPrefab;
    


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.paused == false)
        {
            if (target == null)
                return;

            //Rotate to Target
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            //FireRate
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void FindTarget ()
    {
        switch (targetType)
        {
            case 1:
                target = GameManager.Instance.FindOldestEnemy(range, transform);
                break;
            case 2:
                target = GameManager.Instance.FindStrongestEnemy(range, transform);
                break;
            default:
                target = GameManager.Instance.FindNearestEnemy(range, transform);
                break;
        }
    }

    //Range bubble draw
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, BuildManager.instance.Bullets);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Follow(target, damage, bulletDamageRadius, bulletSpeed);
    }
}
