using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableStats/Stats", fileName = "Stats")]
public class ScriptableStats : ScriptableObject
{
    public new string name;
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;

}
