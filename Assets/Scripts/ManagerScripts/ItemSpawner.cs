using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawn Area")]
    [SerializeField] private float spawnXMin;
    [SerializeField] private float spawnXMax;
    [SerializeField] private float spawnZMin;
    [SerializeField] private float spawnZMax;
    [SerializeField] private float spawnY;

    [Header("Item Prefab & Settings")]
    [SerializeField] private int maxItemOnField;
    [SerializeField] private float itemDropDespawnTimer;
    [SerializeField] private int itemDropChance;
    [SerializeField] float spawnDelay;
    [SerializeField] private List<GameObject> Items;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        SpawnItemOnMap(Items, maxItemOnField);
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    /// <summary>
    /// The GameObject is instantiated in the Area and this is repeated _maxValue.
    /// </summary>
    /// <param name="_prefablist">List of GameObjects to be instantiated.</param>
    /// <param name="_maxValue">How often the GameObject should be instantiated.</param>
    private void SpawnItemOnMap(List<GameObject> _prefablist, int _maxValue)
    {
        for (int i = 0; i < _maxValue; i++)
        {
            GameObject tempItemPrefab = _prefablist[Random.Range(0,_prefablist.Count)];

            float xPos = Random.Range(spawnXMin, spawnXMax);
            float zPos = Random.Range(spawnZMin, spawnZMax);

            Vector3 newPos = new Vector3(xPos, spawnY, zPos);

            GameObject item = Instantiate(tempItemPrefab, newPos + Vector3.up , Quaternion.identity) as GameObject;
            item.transform.parent = transform; //So that the item is instantiated in the GameObject "ItemSpawner".
            StartCoroutine(DespawnItem(item, itemDropDespawnTimer));
        }
    }
    /// <summary>
    /// Drops a random item on the dead enemy position (Drop Chance itemDropChance)
    /// </summary>
    /// <param name="_prefablist">List of GameObjects to be instantiated.</param>
    /// <param name="_enemyPos">Vector position of enemy</param>
    public void MobDrop(Vector3 _enemyPos)
    {
        int tempZuf = Random.Range(0, itemDropChance);
        if(tempZuf == 0)
        {
            GameObject tempItemPrefab = Items[Random.Range(0, Items.Count - 1)];
            GameObject tempItem = Instantiate(tempItemPrefab, _enemyPos, Quaternion.identity);

            StartCoroutine(DespawnItem(tempItem, itemDropDespawnTimer));
        }
    }
    /// <summary>
    /// After how many seconds the dropped item should despawn.
    /// </summary>
    /// <param name="_droppedItem">Item to despawn.</param>
    /// <param name="_itemDropDespawnTimer">After how many seconds the dropped item should despawn.</param>
    /// <returns></returns>
    private IEnumerator DespawnItem(GameObject _droppedItem, float _itemDropDespawnTimer)
    {
        yield return new WaitForSeconds(_itemDropDespawnTimer);

        if(_droppedItem != null)
        Destroy(_droppedItem);
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnItemOnMap(Items, maxItemOnField);
        StartCoroutine(SpawnRoutine());
    }

}
