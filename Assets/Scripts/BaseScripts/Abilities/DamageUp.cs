using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Upgrade
{
    protected override void UpgradeFunction(GameObject upgradeImage)
    {
        Debug.Log("HEllo");
        base.UpgradeFunction(upgradeImage);
    }
}
