using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/HealthUp", order = 2)]
public class HealthUp : Upgrade
{
    [SerializeField] float healthUpValue;
    public override void UpgradeFunction(GameObject player)
    {
        SpawnManager.instance.UpgradSelected = true;
        player.GetComponent<Stats>().Health += healthUpValue;
    }
}
