using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionAI : EnemyAI
{
    [SerializeField] ParticleSystem gasParticles;
    protected override void AttackPlayer()
    {
        if(CheckIfIsInAttackRange())
        {
            Debug.Log("HEHEHEH");
            GetComponent<SphereCollider>().enabled = true;
            gasParticles.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDmg(stats.Dmg);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gasParticles.Stop();
        }
    }
}
