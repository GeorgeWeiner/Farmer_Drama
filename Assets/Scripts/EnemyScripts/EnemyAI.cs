using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof(Stats))]
public abstract class EnemyAI : MonoBehaviour
{   
    public enum AIStates
    {
        chasing,
        attacking
    }
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float attackCd;
    [SerializeField] protected float attackRange;
    protected AIStates currentState;
    public AIStates CurrentState { set { currentState = value; } }
    protected Transform target;
    protected NavMeshAgent agent;
    protected Stats stats;
    protected bool canAttack = true;
    protected virtual void Awake()
    {
        stats = GetComponent<Stats>();
        agent = GetComponentInParent<NavMeshAgent>();
        
    }
    void Start()
    {
        agent.speed = stats.Speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!CheckIfIsInAttackRange())
        {
            currentState = AIStates.chasing;
            ChaseTarget();
        }
        else
        {
            currentState = AIStates.attacking;
            AttackTarget();
        }
    }
    protected virtual void ChaseTarget()
    {
        if(currentState == AIStates.chasing && target != null)
        {     
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }
    protected abstract void AttackTarget();
    protected bool CheckIfIsInAttackRange()
    {
        return Physics.CheckSphere(transform.position, attackRange,targetLayer);
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
