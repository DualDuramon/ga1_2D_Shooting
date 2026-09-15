using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBalanceDataTableSO", menuName = "Scriptable Objects/EnemyBalanceDataTableSO")]
public class EnemyBalanceDataTableSO : ScriptableObject
{
    [SerializeField] private EnemyBalanceData[] _datas;
    public EnemyBalanceData[] Datas => _datas;
}
