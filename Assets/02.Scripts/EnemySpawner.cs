using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //필요 속성
    // - 타이머
    [Header("Spawn Duration")]
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer = 0f;

    // - 생성할 프리펩
    [Header("Spawned Enemy Prefab")]
    [SerializeField] private Enemy[] _enemyPrefabs = new Enemy[3];    //프리펩을 연결하면, 이 컴포넌트를 가진 오브젝트를 연결 & 컴포넌트 참조 시킴.
    [SerializeField] private int[] probablityOfSpawnEnemies;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0f;
            _spawnInterval = Random.Range(1f, 3f);
        }
    }

    private void SpawnEnemy()
    {

        Enemy enemy = Instantiate(DecideSpawnEnemyPrefabs());
        enemy.transform.position = transform.position;
    }

    private Enemy DecideSpawnEnemyPrefabs()
    {
        int calculatedProb = Random.Range(0, 100);
        Enemy spawnEnemy = null;

        for (int i = 0; i < probablityOfSpawnEnemies.Length; i++)
        {
            if (calculatedProb < probablityOfSpawnEnemies[i])
            {
                spawnEnemy = _enemyPrefabs[i];
                break;
            }
        }

        if (spawnEnemy == null)
        {
            spawnEnemy = _enemyPrefabs[_enemyPrefabs.Length - 1];
        }

        return spawnEnemy;
    }
}
