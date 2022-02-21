using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/DmgUp", order = 0)]
public class DamageUp : Upgrade
{
    [SerializeField] ScriptableStats playerStats;
    public override void UpgradeFunction(GameObject button)
    {
        Debug.Log("HEHEHEHEHH");
        playerStats.attackDamage += 20;
        SpawnManager.instance.UpgradSelected = true;
    }
}
