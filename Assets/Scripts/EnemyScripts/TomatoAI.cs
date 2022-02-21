using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoAI : EnemyAI
{
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] float explosionBlinkSpeed;
    protected override void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Awake();
        startColor = GetComponentInChildren<Renderer>().material.color;
    }
    protected override void AttackTarget()
    {
        agent.isStopped = true;
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(Explode());
        }   
    }
    IEnumerator Explode()
    {
        float explosionTimer = 0;
        Vector3 tomatoSizeToReach = transform.localScale + Vector3.one / 2;
        while (attackCd >= 0)
        {
            attackCd -= Time.deltaTime;
            explosionTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale,tomatoSizeToReach , explosionTimer * Time.deltaTime);
            GetComponentInChildren<Renderer>().material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * explosionBlinkSpeed,1));
            yield return new WaitForEndOfFrame();
        }
        DealExplosionDmg();   
    }
    void DealExplosionDmg()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        foreach (var hitables in player )
        {
            if(hitables.GetComponent<IDamageable>() != null)
            {
                hitables.GetComponent<IDamageable>().TakeDmg(GetComponent<Stats>().Dmg);
            }
        }
        var tempParticles = Instantiate(explosionParticles, transform.position, transform.rotation);
        GetComponent<IOnDie>().OnDie();
        Destroy(gameObject);
    }
}
