using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBoltProjectile : MonoBehaviour
{
    [SerializeField] float chaseSpeed;
    [SerializeField] float dmg;
    Transform target;
    public Transform Target { get { return target; } set { target = value; } }

    void Update()
    {
        MoveTowardsTarget();
    }
    void MoveTowardsTarget()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + Vector3.up * 6, Time.deltaTime * chaseSpeed);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>().TakeDmg(dmg);
        }
    }
}
