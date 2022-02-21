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
    [SerializeField] List<GameObject> upgradeList;
    [SerializeField] List<GameObject> upgradePosition;
    static public SpawnManager instance;
    bool upgradeSelected = false;
    public bool UpgradSelected { get { return upgradeSelected; } set { upgradeSelected = value; } }

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
        //upgradePosition[0] = Instantiate(upgradeList[0]);
        
        upgradePosition[0].AddComponent<Upgrade>();
        //upgradeList[0].GetComponent<Upgrade>();
    }
    void Update()
    {
        CheckIfAllEnemiesAreDead();
    }
    IEnumerator SpawnEnemies()
    {
        float delayBetweenEnemySpawn = 0.5f;
        for (int i = 0; i < amountOfEnemiesToSpawn ; i++)
        {
            int randomSpawnPoint = Random.Range(0, allSpawnPoints.Count);
            int randomEnemy = Random.Range(0, allEnemies.Count);
            var tempEnemy = Instantiate(allEnemies[randomEnemy], allSpawnPoints[randomSpawnPoint].transform.position, transform.rotation);
            currentlyAliveEnemies.Add(tempEnemy);
            yield return new WaitForSeconds(delayBetweenEnemySpawn);
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
        if (Time.timeScale == 0 && upgradeSelected)
        {
            Time.timeScale = 1;
            upgradeUI.gameObject.SetActive(false);
            upgradeSelected = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
