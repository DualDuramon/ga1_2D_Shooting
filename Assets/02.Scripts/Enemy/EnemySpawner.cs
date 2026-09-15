using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Spawn Duration")]
    [SerializeField] private float _maxSpawnInterval = 2f;
    [SerializeField] private float _minSpawnInterval = 1f;
    private float _spawnInterval = 3f;
    private float _timer = 0f;

    [Header("Enemy Spawn Setting")]
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;
    [SerializeField] private EnemyBalanceDataTableSO _balanceDataTable;

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
        enemy.SetHealthBalance(GetHealthMultiplier());
        enemy.transform.position = spawnTf.position;
    }

    private Enemy DecideSpawnEnemyPrefabs()
    {
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

    private float GetHealthMultiplier()
    {
        if (_balanceDataTable == null)
        {
            Debug.LogWarning($"{gameObject.name} : _balanceDataTable is Null. Check it out");
            return 1.0f;
        }

        int bestScore = ScoreManager.Instance.BestScore;
        float multiplier = 1f;

        foreach (EnemyBalanceData data in _balanceDataTable.Datas)
        {
            if (data.RequiredScore < bestScore)
            {
                multiplier = data.HealthMultiplier;
            }
            else
            {
                break;
            }
        }

        return multiplier;
    }
}
