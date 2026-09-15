using UnityEngine;

[System.Serializable]
public class EnemyBalanceData
{
    [SerializeField] private int _requiredScore = 0;
    [SerializeField] private float _healthMultiplier = 1;

    public int RequiredScore => _requiredScore;
    public float HealthMultiplier => _healthMultiplier;
}
