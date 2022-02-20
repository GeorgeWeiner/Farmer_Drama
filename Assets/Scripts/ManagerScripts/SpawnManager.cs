using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> allEnemies;
    [SerializeField] List<GameObject> currentlyAliveEnemies;
    [SerializeField] List<GameObject> allSpawnPoints;
    [SerializeField] float amountToSpawn;
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
        StartCoroutine(SpawnEnemies(amountToSpawn));
    }
    void Update()
    {
        checkIfAllEnemiesAreDead();   
    }
    IEnumerator SpawnEnemies(float amountToSpawn)
    {
        float delayBetweenEnemySpawn = 1;
        for (int i = 0; i < amountToSpawn ; i++)
        {
            int randomSpawnPoint = Random.Range(0, allSpawnPoints.Count);
            int randomEnemy = Random.Range(0,allEnemies.Count);
            var tempEnemy = Instantiate(allEnemies[randomEnemy], allSpawnPoints[randomSpawnPoint].transform.position, transform.rotation);
            currentlyAliveEnemies.Add(tempEnemy);
            yield return new WaitForSeconds(delayBetweenEnemySpawn);
        }
        yield return new WaitUntil(checkIfAllEnemiesAreDead);
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
    public void RemoveKilledEnemies()
    {
        for (int i = 0; i < currentlyAliveEnemies.Count; i++)
        {
            if(currentlyAliveEnemies[i] == null)
            {
                currentlyAliveEnemies.Remove(currentlyAliveEnemies[i]);
                return;
            }
        }
    }
}
