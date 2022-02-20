using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : EnemyAI
{
    [SerializeField] GameObject pumpkinSeedProjectile;
    protected override void AttackPlayer()
    {
        agent.isStopped = true;
        if (canAttack)
        {
            StartCoroutine(AttackCd());
            transform.LookAt(player.position);
            var tempProjectile = Instantiate(pumpkinSeedProjectile, transform.position, transform.rotation);
            tempProjectile.GetComponent<PumpkinSeedProjectile>().Dmg = stats.Dmg;
        }
    }
}
