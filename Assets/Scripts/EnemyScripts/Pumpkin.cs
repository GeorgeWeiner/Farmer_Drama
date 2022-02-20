using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : EnemyAI
{
    [SerializeField] GameObject pumpkinSeedProjectile;
    [SerializeField] Transform SpawnPoint;
    protected override void AttackPlayer()
    {
        agent.isStopped = true;
        if (canAttack)
        {
            StartCoroutine(AttackCd());
            transform.LookAt(player.position);
            var tempProjectile = Instantiate(pumpkinSeedProjectile, SpawnPoint.position, transform.rotation);
            tempProjectile.GetComponent<PumpkinSeedProjectile>().Dmg = stats.Dmg;
        }
    }
}
