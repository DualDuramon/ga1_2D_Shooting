using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //필요 속성
    // - 타이머
    [Header("Spawn Duration")]
    [SerializeField] private float _maxSpawnInterval = 2f;
    [SerializeField] private float _minSpawnInterval = 1f;
    private float _spawnInterval = 3f;
    private float _timer = 0f;

    // - 생성할 프리펩
    [Header("Spawned Enemy Prefab")]
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;

    private int _maxWeight;

    [Header("Spawn Point")]
    [SerializeField] private Transform[] _spawnPoint = new Transform[3];

    private void Awake()
    {
        CalculateMaxProbablity();
    }

    private void CalculateMaxProbablity()
    {
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            _maxWeight += data.Weight;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0f;
            _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnPoint == null)
        {
            Debug.LogWarning("Can't Spawn Enemy! Check Spawn Point");
            return;
        }

        Transform spawnTf = _spawnPoint[(Random.Range(0, _spawnPoint.Length))];

        Enemy enemy = Instantiate(DecideSpawnEnemyPrefabs());
        enemy.transform.position = spawnTf.position;
    }

    private Enemy DecideSpawnEnemyPrefabs()
    {
        //int calculatedProb = Random.Range(0, _maxProbablity);
        //Enemy spawnEnemy = null;

        //TODO : ScriptableObject를 사용해서 리펙토링
        //이유 : 배열을 사용했지만 각 아이템이 어떤 프리펩인지 알 수가 없다.
        // 각 에너미 스폰 확률이랑 Enemy가 분리되어 있어서 유지보수가 어렵다

        //for (int i = 0; i < _enemySpawnProbabilities.Length; i++)
        //{
        //    if (calculatedProb < _enemySpawnProbabilities[i])
        //    {
        //        spawnEnemy = _enemyPrefabs[i];
        //        break;
        //    }
        //    calculatedProb -= _enemySpawnProbabilities[i];
        //}


        //가중치 랜덤 선택
        Enemy spawnEnemy = null;

        int randomWeight = Random.Range(0, _maxWeight);
        int accomulateWeight = 0;

        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            accomulateWeight += data.Weight;

            if (randomWeight < accomulateWeight)
            {
                spawnEnemy = data.EnemyPreafb.GetComponent<Enemy>();
                break;
            }
        }

        return spawnEnemy;
    }
}
