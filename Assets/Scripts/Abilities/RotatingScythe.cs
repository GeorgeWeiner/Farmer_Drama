using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Upgrades/Scythe", order = 1)]
public class RotatingScythe : Upgrade
{
    [SerializeField] GameObject scythe;
    
    public override void UpgradeFunction(GameObject button)
    {
        throw new System.NotImplementedException();
    }
}
