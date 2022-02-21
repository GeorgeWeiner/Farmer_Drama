using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/DmgUp", order = 0)]
public class DamageUp : Upgrade
{
    public override void UpgradeFunction(GameObject player)
    {
        player.GetComponent<Stats>().Dmg += 20;
        SpawnManager.instance.UpgradSelected = true;
    }
}
