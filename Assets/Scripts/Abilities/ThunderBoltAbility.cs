using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "NewUpgrade/ThunderBolt", order = 3)]
public class ThunderBoltAbility : Upgrade
{
    [SerializeField] GameObject thunderboltSpawner;

    public override void UpgradeFunction(GameObject player)
    {
        if (GameObject.FindGameObjectWithTag("ThunderBoltSpawner") == null)
        {
            SpawnManager.instance.UpgradSelected = true;
            var thunderBoltSpawner = Instantiate(thunderboltSpawner, player.transform.position, Quaternion.identity);
        }
        else if (GameObject.FindGameObjectWithTag("ThunderBoltSpawner") != null)
        {
            SpawnManager.instance.UpgradSelected = true;
            GameObject.FindGameObjectWithTag("ThunderBoltSpawner").GetComponent<ThunderBoltSpawner>().AmoountOfThunderbolts += 1; 
        }  
    }
}
