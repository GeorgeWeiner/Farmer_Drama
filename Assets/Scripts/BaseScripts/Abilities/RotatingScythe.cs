using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/Scythe", order = 1)]
public class RotatingScythe : Upgrade
{
    [SerializeField] GameObject scythe;
    [SerializeField] int dmgUpOnMaxScythes;
    [SerializeField] int maxSizeCount;
    public override void UpgradeFunction(GameObject player)
    {
        SpawnManager.instance.UpgradSelected = true;
        if (SpawnManager.instance.ScytheCount < 4)
        {   
            var tempObj = Instantiate(scythe, SpawnManager.instance.SpawnPoints.position, Quaternion.identity);
            tempObj.transform.SetParent(SpawnManager.instance.SpawnPoints, true);
            SpawnManager.instance.ScytheCount += 1;
            tempObj.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z));
        }
        else
        {
            GameObject[] scyythesSpawned = GameObject.FindGameObjectsWithTag("Scythe");
            foreach (var scythe in scyythesSpawned)
            {
                scythe.GetComponent<Scythe>().ScytheDmg += dmgUpOnMaxScythes;
            }
        }
   
    }
}
