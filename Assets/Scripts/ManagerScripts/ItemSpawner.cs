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
    [SerializeField] private List<GameObject> Items;
    [SerializeField] private int maxItemOnField = 1;
    [SerializeField] private float itemDropDespawnTimer = 10f;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        SpawnItemOnMap(Items, maxItemOnField);
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

            GameObject item = Instantiate(tempItemPrefab, newPos, Quaternion.identity) as GameObject;
            item.transform.parent = transform; //So that the item is instantiated in the GameObject "ItemSpawner".
        }
    }
    /// <summary>
    /// Drops a random item on the dead enemy position (Drop Chance 10%)
    /// </summary>
    /// <param name="_prefablist">List of GameObjects to be instantiated.</param>
    /// <param name="_enemyPos">Vector position of enemy</param>
    public void MobDrop(List<GameObject> _prefablist, Vector3 _enemyPos)
    {
        int tempZuf = Random.Range(0, 10);
        if(tempZuf == 0)
        {
            GameObject tempItemPrefab = _prefablist[Random.Range(0, _prefablist.Count - 1)];
            Instantiate(tempItemPrefab, _enemyPos, Quaternion.identity);

            //StartCoroutine(DespawnItem(tempItemPrefab, itemDropDespawnTimer));
        }
    }
    private IEnumerator DespawnItem(GameObject _droppedItem, float _itemDropDespawnTimer)
    {
        yield return new WaitForSeconds(_itemDropDespawnTimer);

        if(_droppedItem != null)
        Destroy(_droppedItem);
    }
}
