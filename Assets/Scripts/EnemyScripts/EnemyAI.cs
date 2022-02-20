using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public abstract class EnemyAI : MonoBehaviour
{   
    public enum AIStates
    {
        chasing,
        attacking
    }
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] float attackCd;
    [SerializeField] float attackRange;
    AIStates currentState;
    public AIStates CurrentState { set { currentState = value; } }
    protected Transform player;
    protected NavMeshAgent agent;
    protected bool canAttack = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckIfIsInAttackRange())
        {
            currentState = AIStates.chasing;
            ChasePlayer();
        }
        else
        {
            currentState = AIStates.attacking;
            AttackPlayer();
        }
    }
    void ChasePlayer()
    {
        if(currentState == AIStates.chasing)
        {     
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }
    protected abstract void AttackPlayer();
    bool CheckIfIsInAttackRange()
    {
        return Physics.CheckSphere(transform.position, attackRange,playerLayer);
    }
    protected IEnumerator AttackCd()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCd);
        canAttack = true;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
