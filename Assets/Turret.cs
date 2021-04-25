using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range;
    public float turnSpeed;
    public Transform PartToRotate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //Rotate to Target
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
    }

    private void FindTarget ()
    {
        target = GameManager.Instance.FindNearestEnemy(range, transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
