using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using TMPro;

public class SpawnManager : MonoBehaviour
{   [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> allEnemies;
    [SerializeField] List<GameObject> currentlyAliveEnemies;
    [SerializeField] List<GameObject> allSpawnPoints;
    [SerializeField] List<Upgrade> upgradeList;
    [SerializeField] List<GameObject> upgradeButtons;
    [SerializeField] TextMeshProUGUI [] upgradeText;
    [SerializeField] float amountOfEnemiesToSpawn;
    [SerializeField] float delayBeetweenWaves;
    [SerializeField] float timeTillUpgradeUIActivates;
    static public SpawnManager instance;

    public event Action OnWaveBegin;

    private bool upgradeSelected = false;
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
    }

    private void Update()
    {
        CheckIfAllEnemiesAreDead();
    }

    private IEnumerator SpawnEnemies()
    {
        OnWaveBegin?.Invoke();
        SoundManager.instance.PlayAudioClip(ESoundType.OnWaveBegin, GetComponent<AudioSource>());
        yield return new WaitForSeconds(3);
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
        yield return new WaitForSeconds(timeTillUpgradeUIActivates);
        ActivateUpgradeUI();
        GetRandomUpgrade();
        yield return new WaitUntil(DeactivateUpgradeUI);
        yield return new WaitForSeconds(delayBeetweenWaves);
        StartCoroutine(SpawnEnemies());
    }

    private bool CheckIfAllEnemiesAreDead()
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
    void GetRandomUpgrade()
    {
        
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int randomUpgrade = Random.Range(0, upgradeList.Count);
            var tempImage = upgradeButtons[i].AddComponent<Image>();
            tempImage.sprite = upgradeList[randomUpgrade].UpgradeImage;
            upgradeText[i].text = upgradeList[randomUpgrade].Description;
            AddEvent(EventTriggerType.PointerClick, delegate { upgradeList[randomUpgrade].UpgradeFunction(player); } ,upgradeButtons[i]);  
        }    
    }
    private void ResetUpgrades()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            Component[] components = upgradeButtons[i].GetComponents<Component>();
            foreach (var componentsOnThisObject in components)
            {
                if (!Types.Equals(componentsOnThisObject, upgradeButtons[i].GetComponent<RectTransform>()) && !Types.Equals(componentsOnThisObject, upgradeButtons[i].GetComponent<CanvasRenderer>()))
                {
                    Destroy(componentsOnThisObject);
                }   
            }
        }
    }
    public void RemoveKilledEnemies(GameObject enemyToRemove)
    {
        currentlyAliveEnemies.Remove(enemyToRemove);
    }
    void ActivateUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0;
    }
    public bool DeactivateUpgradeUI()
    {
        if (Time.timeScale == 0 && upgradeSelected)
        {
            ResetUpgrades();
            Time.timeScale = 1;
            upgradeUI.gameObject.SetActive(false);
            upgradeSelected = false;
            player.GetComponent<Player>().enabled = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowUpgradeText(Upgrade upgradeText)
    {

    }
    protected void AddEvent(EventTriggerType type, UnityAction<BaseEventData> action, GameObject button)
    {
        EventTrigger trigger = button.AddComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
