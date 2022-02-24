using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] LayerMask explosionTargets;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float explosionRange;
    [SerializeField] float explosionDmg;
    [SerializeField] int hitsToExplode;
    public void TakeDmg(float dmg)
    {
        hitsToExplode -= 1;
        CheckIfHasToExplode();
    }
    void Explode()
    {
        var tempParticles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        SoundManager.instance.PlayAudioClip(ESoundType.TomatoExplosion, GetComponent<AudioSource>(), true);
        Collider[] enemiesInExplosionRange = Physics.OverlapSphere(transform.position, explosionRange, explosionTargets);
        foreach (var enemyInRange in enemiesInExplosionRange)
        {
            if(enemyInRange.GetComponent<IDamageable>() != null)
            {
                enemyInRange.GetComponent<IDamageable>().TakeDmg(explosionDmg);
            }  
        }
    }
    void CheckIfHasToExplode()
    {
        if(hitsToExplode <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
      
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
