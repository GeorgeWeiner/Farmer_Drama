using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Items
{
    protected override void OnTriggerEnter(Collider _other)
    {
        Debug.Log("Medkit" + _other.gameObject.name);
        base.OnTriggerEnter(_other);
    }

    protected override void SetNewTempStats(Collider _other)
    {
        Debug.Log("Kommt in SetNewTempStats rein ");
        _other.gameObject.GetComponent<Stats>().AddHealth(50f);
        Destroy(gameObject);
    }
}
