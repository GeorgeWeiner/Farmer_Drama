using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocaine : Items
{
    protected override void OnTriggerEnter(Collider _other)
    {
        Debug.Log("Cocaine" + _other.gameObject.name);
        base.OnTriggerEnter(_other);
    }

    protected override void SetNewTempStats(Collider _other)
    {
        Debug.Log("Kommt in SetNewTempStats rein ");
        _other.gameObject.GetComponent<Stats>().AddTempAttackSpeed(2f);
        _other.gameObject.GetComponent<Stats>().AddTempSpeed(2f);
        _other.gameObject.GetComponent<Stats>().IsOnCocain = true;
        StartCoroutine(BuffDuration(10f, _other)); //Buff currently lasts 10 seconds
    }

    protected override IEnumerator BuffDuration(float _timer, Collider _other)
    {
        yield return base.BuffDuration(_timer, _other);

        _other.gameObject.GetComponent<Stats>().ResetTempAttackSpeed();
        _other.gameObject.GetComponent<Stats>().ResetTempSpeed();
        _other.gameObject.GetComponent<Stats>().IsOnCocain = false;
        Debug.Log("Buff ist verschwunden");

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
