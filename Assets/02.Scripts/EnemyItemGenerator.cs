using UnityEngine;

public class EnemyItemGenerator : MonoBehaviour
{
    [SerializeField] private int _maxItemSpawnRate;
    [SerializeField] private Item[] _itemPrefabs;

    public void SpawnItemRandomly()
    {
        int randomPercent = Random.Range(0, 100);
        if (randomPercent < _maxItemSpawnRate)
        {
            return;
        }

        int randomItemIndex = Random.Range(0, 3);
        Instantiate(_itemPrefabs[randomItemIndex], transform.position, Quaternion.identity);
    }
}
