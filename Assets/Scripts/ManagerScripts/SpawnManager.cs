using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject upgradeUI;
    [SerializeField] List<GameObject> allEnemies;
    [SerializeField] List<GameObject> currentlyAliveEnemies;
    [SerializeField] List<GameObject> allSpawnPoints;
    [SerializeField] float amountOfEnemiesToSpawn;
    [SerializeField] float delayBeetweenWaves;
    static public SpawnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
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
        CheckIfAllEnemiesAreDead();
    }
    IEnumerator SpawnEnemies()
    {
        float delayBetweenEnemySpawn = 1;
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            int randomSpawnPoint = Random.Range(0, allSpawnPoints.Count);
            int randomEnemy = Random.Range(0, allEnemies.Count);
            var tempEnemy = Instantiate(allEnemies[randomEnemy], allSpawnPoints[randomSpawnPoint].transform.position, transform.rotation);
            currentlyAliveEnemies.Add(tempEnemy);
            yield return new WaitForSeconds(delayBetweenEnemySpawn);
            Debug.Log(i);
        }
        yield return new WaitUntil(CheckIfAllEnemiesAreDead);
        amountOfEnemiesToSpawn += 4;
        Debug.Log("Waiting");
        ActivateUpgradeUI();
        yield return new WaitUntil(DeactivateUpgradeUI);
        StartCoroutine(SpawnEnemies());
    }
    bool CheckIfAllEnemiesAreDead()
    {
        if (currentlyAliveEnemies.Count <= 0)
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

    void ActivateUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public bool DeactivateUpgradeUI()
    {
        if (Time.timeScale == 0)
        {
            return false;
        }
        else
        {
            upgradeUI.gameObject.SetActive(false);
            Time.timeScale = 1;
            return true;
        }
    }
}
