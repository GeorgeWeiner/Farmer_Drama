using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : EnemyAI
{
    [SerializeField] GameObject pumpkinSeedProjectile;
    [SerializeField] Transform SpawnPoint;
    protected override void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Awake();
    }
    protected override void AttackTarget()
    {
        agent.isStopped = true;
        if (target != null && canAttack)
        {
            StartCoroutine(AttackCd());
            transform.LookAt(target.position);
            var tempProjectile = Instantiate(pumpkinSeedProjectile, SpawnPoint.position, transform.rotation);
            tempProjectile.GetComponent<PumpkinSeedProjectile>().Dmg = stats.Dmg;
        }
    }
}
