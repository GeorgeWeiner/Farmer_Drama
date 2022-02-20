using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Death : MonoBehaviour, IOnDie
{
    public abstract void OnDie();
   
}
