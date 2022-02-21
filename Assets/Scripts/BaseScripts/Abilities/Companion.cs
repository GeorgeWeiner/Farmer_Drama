using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/Companion", order = 3)]
public class Companion : Upgrade
{
    [SerializeField] GameObject companion;
    public override void UpgradeFunction(GameObject player)
    {
        SpawnManager.instance.UpgradSelected = true;
        var tempDoggo = Instantiate(companion, player.transform.position + Vector3.forward * 3, Quaternion.identity);
    }
}
