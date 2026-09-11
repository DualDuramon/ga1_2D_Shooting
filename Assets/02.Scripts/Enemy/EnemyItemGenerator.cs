using UnityEngine;

public class EnemyItemGenerator : MonoBehaviour
{
    [SerializeField] private ItemSpawnDataTableSO _spawnTable;
    private int _maxWeight = 0;
    [SerializeField] private int _itemSpawnRate;


    private void Awake()
    {
        CalculateMaxWeight();
    }

    private void CalculateMaxWeight()
    {
        int totalWeight = 0;
        foreach (ItemSpawnData data in _spawnTable.Datas)
        {
            totalWeight = data.Weight;
        }

        _maxWeight = totalWeight;
    }

    public void SpawnItemRandomly()
    {
        if (!CanItemSpawn()) return;

        int randomWeight = Random.Range(0, _maxWeight);
        int accomulateWeight = 0;
        Item spawnItem = null;

        foreach (ItemSpawnData data in _spawnTable.Datas)
        {
            accomulateWeight += data.Weight;

            if (randomWeight < accomulateWeight)
            {
                spawnItem = data.ItemPrefab.GetComponent<Item>();
                break;
            }
        }


        SpawnItem(spawnItem);
    }

    private bool CanItemSpawn()
    {
        return Random.Range(0, 100) <= _itemSpawnRate;
    }

    private void SpawnItem(Item spawnItem)
    {
        Instantiate(spawnItem, transform.position, Quaternion.identity);
    }
}
