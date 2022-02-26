using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
   
    [SerializeField] float dmg;
    public float ScytheDmg { get { return dmg; } set { dmg = value; } }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            SoundManager.instance.PlayAudioClip(ESoundType.ScytheHitSound, GetComponent<AudioSource>(), false);
            other.GetComponent<IDamageable>().TakeDmg(dmg);
            StartCoroutine(GetComponent<DmgPopUp>().ActivatePopUp(dmg, other.gameObject));
        }
    }
}
