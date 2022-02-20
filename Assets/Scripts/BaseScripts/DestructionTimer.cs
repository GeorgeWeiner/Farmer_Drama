using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTimer : MonoBehaviour
{
    [SerializeField] float destructionTimer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destructionTimer);
    }
}
