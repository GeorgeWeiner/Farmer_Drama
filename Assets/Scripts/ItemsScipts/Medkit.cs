using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Items
{
    protected override void OnTriggerEnter(Collider _other)
    {
        base.OnTriggerEnter(_other);
    }

    protected override void SetNewTempStats(Collider _other)
    {
        _other.gameObject.GetComponent<Stats>().AddHealth(50f);
        Destroy(gameObject);
    }
}
