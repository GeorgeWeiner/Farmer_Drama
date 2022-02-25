using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoDeath : Death
{
    public override void OnDie()
    {
        Debug.Log("DOGODIED NOOO");
        Destroy(gameObject);
    }

}
