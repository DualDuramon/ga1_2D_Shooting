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
    [SerializeField] private Enemy _enemyprefab;    //프리펩을 연결하면, 이 컴포넌트를 가진 오브젝트를 연결 & 컴포넌트 참조 시킴.

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnInterval)
        {
            SpawnEnemy(_enemyprefab);
            _timer = 0f;
            Debug.Log("에네미 스폰");
        }
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.transform.position = transform.position;
    }

}
