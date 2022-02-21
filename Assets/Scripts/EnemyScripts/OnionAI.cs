using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionAI : EnemyAI
{
    [SerializeField] ParticleSystem gasParticles;
    protected override void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Awake();
    }
    protected override void AttackTarget()
    {
        if(CheckIfIsInAttackRange())
        {
            GetComponentInChildren<SphereCollider>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDmg(stats.Dmg);
        }
    }
}
