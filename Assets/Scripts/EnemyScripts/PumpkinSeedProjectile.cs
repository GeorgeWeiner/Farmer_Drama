using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PumpkinSeedProjectile : MonoBehaviour
{
    [SerializeField] float projectileForce;
    float dmg;
    public float Dmg { set { dmg = value; } }
    Rigidbody projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        InitProjectile();
        Destroy(gameObject, 2);
    }
    void InitProjectile()
    {
        projectileRb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && !other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>().TakeDmg(dmg);
            Destroy(gameObject);
        }    
    }
}
