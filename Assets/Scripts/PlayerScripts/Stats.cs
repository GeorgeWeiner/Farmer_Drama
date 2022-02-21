using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wenn du die Upgrade Fuktion verwenden willst, solltest du direkt darunter die Methode "ResetTempStats()" machen, damit sie
// die Stats�berschreiben (ohne dem Reset k�nnte es sonst zur Stacking-Problemen kommen (TempBuffs mit UpgradeStats ->
// TempBuffs w�rden als Fixwert �berschrieben)), bei Fragen melden
public class Stats : MonoBehaviour,IDamageable
{
    [SerializeField] ScriptableStats statsOfThisObject;
    [SerializeField] float health;
    float currentHealth;
    public float Health { get { return health; } set { health = value; } }

    [SerializeField] float attackSpeed;
    float currentAttackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set {attackSpeed = value; } }

    [SerializeField] float speed;
    float currentSpeed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] float dmg;
    float currentdmg;
    public float Dmg { get { return dmg; } set { dmg = value; } }

    [Header("Buff States")]
    [SerializeField] private bool isOnCocain;
    public bool IsOnCocain { get { return isOnCocain; } set { isOnCocain = value; } }
    [SerializeField] private bool isOnBoosterVaccination;
    public bool IsOnBoosterVaccination { get { return isOnBoosterVaccination; } set { isOnBoosterVaccination = value; } }

    private void Awake()
    {
        CopyStatsFromScriptable();
        SaveCurrentStats();
    }
    void CopyStatsFromScriptable()
    {
        health = statsOfThisObject.health;
        dmg = statsOfThisObject.attackDamage;
        attackSpeed = statsOfThisObject.attackSpeed;
        speed = statsOfThisObject.movementSpeed;
        //Debug.Log(speed);
    }
    /// <summary>
    /// Player takes damage
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDmg(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            GetComponent<IOnDie>().OnDie();
        }
    }

    /// <summary>
    /// Saves the stats without temporary buffs
    /// </summary>
    public void SaveCurrentStats()
    {
        currentHealth = health;
        currentdmg = dmg;
        currentAttackSpeed = attackSpeed;
        currentSpeed = speed;
    }
    
    /// <summary>
    /// Removes all temporary stats
    /// </summary>
    public void ResetTempStats()
    {
        health = currentHealth;
        dmg = currentdmg;
        attackSpeed = currentAttackSpeed;
        speed = currentSpeed;
    }

    #region UpgradeStats
    //Upgrade system for player
    public void UpgradeHealth(float _currentHealth)
    {
        currentHealth += _currentHealth;
    }
    public void UpgradeDmg(float _currenttdmg)
    {
        currentdmg += _currenttdmg;
    }
    public void UpgradeAttackSpeed(float _currentAttackSpeed)
    {
        currentAttackSpeed += _currentAttackSpeed;
    }
    public void UpgradeSpeed(float _currentSpeed)
    {
        currentSpeed += _currentSpeed;
    }
    #endregion UpgradeStats

    #region AddTempStats
    //Temporary stats buffs from items
    public void AddHealth(float _tempHealth)
    {
        health += _tempHealth;
        if (health > currentHealth)
            health = currentHealth;
    }
    public void AddTempDmg(float _tempdmg) 
    {
        dmg += _tempdmg;
    }
    public void AddTempAttackSpeed(float _tempAttackSpeed) 
    {
        attackSpeed += _tempAttackSpeed;
    }
    public void AddTempSpeed(float _tempSpeed) 
    {
        speed += _tempSpeed;
    }
    #endregion AddTempStats

    #region ResetTempStats
    //Temporary stats buffs from items
    public void ResetTempHealth()
    {
        health = currentHealth;
    }
    public void ResetTempDmg()
    {
        dmg = currentdmg;
    }
    public void ResetTempAttackSpeed()
    {
        attackSpeed = currentAttackSpeed;
    }
    public void ResetTempSpeed()
    {
        speed = currentSpeed;
    }
    #endregion ResetTempStats
}
