using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : Death
{
    public override void OnDie()
    {
        SpawnManager.instance.RemoveKilledEnemies(this.gameObject);
        Destroy(gameObject);
    }
}
