using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> allEnemies;
    [SerializeField] List<GameObject> currentlyAliveEnemies;
    [SerializeField] List<GameObject> allSpawnPoints;
    [SerializeField] float amountOfEnemiesToSpawn;
    [SerializeField] float delayBeetweenWaves;
    static public SpawnManager instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null && instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    void Update()
    {
        checkIfAllEnemiesAreDead();   
    }
    IEnumerator SpawnEnemies()
    {
        float delayBetweenEnemySpawn = 1;
        for (int i = 0; i < amountOfEnemiesToSpawn ; i++)
        {
            int randomSpawnPoint = Random.Range(0, allSpawnPoints.Count);
            int randomEnemy = Random.Range(0,allEnemies.Count);
            var tempEnemy = Instantiate(allEnemies[randomEnemy], allSpawnPoints[randomSpawnPoint].transform.position, transform.rotation);
            currentlyAliveEnemies.Add(tempEnemy);
            yield return new WaitForSeconds(delayBetweenEnemySpawn);
            Debug.Log(i);
        }
        yield return new WaitUntil(checkIfAllEnemiesAreDead);
        amountOfEnemiesToSpawn += 4;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(delayBeetweenWaves);
        StartCoroutine(SpawnEnemies());
    }
    bool checkIfAllEnemiesAreDead()
    {
        if(currentlyAliveEnemies.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public void RemoveKilledEnemies(GameObject enemyToRemove)
    {         
        currentlyAliveEnemies.Remove(enemyToRemove);
    }
}
