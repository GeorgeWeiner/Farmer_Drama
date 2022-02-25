using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : Death
{
    [SerializeField] ParticleSystem deathParticles;
    public override void OnDie()
    {
        var tempParticles = Instantiate(deathParticles, transform.position + Vector3.up, Quaternion.identity);
        SpawnManager.instance.RemoveKilledEnemies(this.gameObject);
        Destroy(gameObject);
    }
}
