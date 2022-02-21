using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Upgrade : ScriptableObject
{
    [SerializeField] Sprite upgradeImg;
    public Sprite UpgradeImage => upgradeImg;

    [SerializeField] [TextArea(10, 10)] protected string description;
    public string Description => description;
    public abstract void UpgradeFunction(GameObject button);
   
}
