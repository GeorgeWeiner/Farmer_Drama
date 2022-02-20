using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour,IDamageable
{
    [SerializeField] ScriptableStats statsOfThisObject;
    [SerializeField] float health;
    public float Health { get { return health; } set { health = value; } }
    float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set {attackSpeed = value; } }
    float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    float dmg;
    public float Dmg { get { return dmg; } set { dmg = value; } }
    private void Awake()
    {
        CopyStatsFromScriptable();
    }
    void CopyStatsFromScriptable()
    {
        health = statsOfThisObject.health;
        dmg = statsOfThisObject.attackDamage;
        attackSpeed = statsOfThisObject.attackSpeed;
        speed = statsOfThisObject.movementSpeed;
        Debug.Log(speed);
    }
    public void TakeDmg(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            GetComponent<IOnDie>().OnDie();
        }
    }
}
