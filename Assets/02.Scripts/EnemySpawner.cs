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
    [SerializeField] private int[] _enemySpawnProbabilities;
    private int _maxProbablity;

    private void Awake()
    {
        CalculateMaxProbablity();
    }

    private void CalculateMaxProbablity()
    {
        for (int i = 0; i < _enemySpawnProbabilities.Length; i++)
        {
            _maxProbablity += _enemySpawnProbabilities[i];
        }
    }

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
        int calculatedProb = Random.Range(0, _maxProbablity);
        Enemy spawnEnemy = null;

        //TODO : ScriptableObject를 사용해서 리펙토링
        //이유 : 배열을 사용했지만 각 아이템이 어떤 프리펩인지 알 수가 없다.
        // 각 에너미 스폰 확률이랑 Enemy가 분리되어 있어서 유지보수가 어렵다.
        for (int i = 0; i < _enemySpawnProbabilities.Length; i++)
        {
            if (calculatedProb < _enemySpawnProbabilities[i])
            {
                spawnEnemy = _enemyPrefabs[i];
                break;
            }
            calculatedProb -= _enemySpawnProbabilities[i];
        }

        if (spawnEnemy == null)
        {
            spawnEnemy = _enemyPrefabs[_enemyPrefabs.Length - 1];
        }

        return spawnEnemy;
    }
}
