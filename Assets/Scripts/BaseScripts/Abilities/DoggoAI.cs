using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoAI : EnemyAI
{
    [SerializeField] int amountOfTargetsThatCanBeHit;
    [SerializeField] float sightRange;
    GameObject player;
    public GameObject Player { get { return player; } set { player = value; } }
    protected override void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void AttackTarget()
    {
        if (canAttack)
        {
            int counter = 0;
            SoundManager.instance.PlayAudioClip(ESoundType.DoggoAttacks, GetComponent<AudioSource>(), false);
            StartCoroutine(AttackCd());
            Collider[] attackableTargets = Physics.OverlapSphere(transform.position + Vector3.forward, attackRange, targetLayer);
            for (int i = 0; i < attackableTargets.Length; i++)
            {
                if (counter <= amountOfTargetsThatCanBeHit)
                {
                    attackableTargets[i].GetComponent<IDamageable>().TakeDmg(GetComponent<Stats>().Dmg);
                }
            }
        }
    }
    protected override void ChaseTarget()
    {
        if (player != null)
        {
            if (CheckIfEnemiesAreInSight() && currentState == AIStates.chasing)
            {
                agent.isStopped = false;
                target = GameObject.FindGameObjectWithTag("Enemy").transform;
                agent.SetDestination(target.position);
            }
            else if (CalculateDistanceToPlayer() >= 4f)
            {
                agent.isStopped = false;
                target = player.transform;
                agent.SetDestination(target.position);

            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
    bool CheckIfEnemiesAreInSight()
    {
        return Physics.CheckSphere(transform.position, sightRange, targetLayer);
    }
    float CalculateDistanceToPlayer()
    {
        return Mathf.Sqrt((transform.position - player.transform.position).sqrMagnitude);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
