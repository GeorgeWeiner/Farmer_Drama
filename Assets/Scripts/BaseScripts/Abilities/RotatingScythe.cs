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
        var tempObj = Instantiate(scythe, new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z + scythe.GetComponent<Scythe>().MaxDistance) , Quaternion.identity);
        tempObj.GetComponent<Scythe>().Player = player.transform;
        tempObj.GetComponent<Scythe>().RotationPoint = GameObject.FindGameObjectWithTag("RotationPoint").transform;
    }
}
