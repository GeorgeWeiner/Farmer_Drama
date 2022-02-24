using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBoltSpawner : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform player;
    [SerializeField] GameObject thunderBolt;
    [SerializeField] float amountOfThunderbolts;
    [SerializeField] float spawnRadius;
    public float AmoountOfThunderbolts { get { return amountOfThunderbolts;} set { amountOfThunderbolts = value; } }
    [SerializeField] float delayBetweenThunderBolts;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        StartCoroutine(SpawnThunderBolts());
    }
    private void Update()
    {
        transform.position = player.position;
    }
    IEnumerator SpawnThunderBolts()
    {
        
        for (int i = 0; i < amountOfThunderbolts; i++)
        {
            if(CheckIfEnemiesAreInRange())
            {
                var tempObj = Instantiate(thunderBolt, transform.position, Quaternion.identity);
                SearchTarget(tempObj.GetComponent<ThunderBoltProjectile>());
                
            }
            yield return new WaitForSeconds(delayBetweenThunderBolts);
        }
        StartCoroutine(SpawnThunderBolts());  
    }
    void SearchTarget(ThunderBoltProjectile thunder)
    {   
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spawnRadius, enemyLayer);
        thunder.Target = enemiesInRange[0].transform;
        
    }
    bool CheckIfEnemiesAreInRange()
    {
        return Physics.CheckSphere(transform.position, spawnRadius, enemyLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
