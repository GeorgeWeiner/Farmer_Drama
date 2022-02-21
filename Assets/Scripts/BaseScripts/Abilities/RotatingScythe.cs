using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/Scythe", order = 1)]
public class RotatingScythe : Upgrade
{
    [SerializeField] GameObject scythe;
    public override void UpgradeFunction(GameObject player)
    {
        SpawnManager.instance.UpgradSelected = true;
        var tempObj = Instantiate(scythe, player.transform.position , Quaternion.identity);
        tempObj.GetComponent<Scythe>().Player = player.transform;
        tempObj.transform.SetParent(player.transform, false);
    }

}
