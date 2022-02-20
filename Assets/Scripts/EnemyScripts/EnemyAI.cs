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
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] float attackCd;
    [SerializeField] float attackRange;
    AIStates currentState;
    public AIStates CurrentState { set { currentState = value; } }
    protected Transform player;
    protected NavMeshAgent agent;
    protected Stats stats;
    protected bool canAttack = true;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stats = GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();
        
    }
    private void Start()
    {
        agent.speed = stats.Speed;
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
