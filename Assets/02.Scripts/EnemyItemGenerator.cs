using UnityEngine;

public class EnemyItemGenerator : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;

    public void GenerateRandomItem()
    {
        int randomIndex = Random.Range(0, 3);

        Instantiate(_itemPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
