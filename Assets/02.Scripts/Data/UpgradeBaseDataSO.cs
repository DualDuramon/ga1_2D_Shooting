using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeBaseDataSO", menuName = "Scriptable Objects/UpgradeBaseDataSO")]
[Serializable]
public class UpgradeBaseDataSO : ScriptableObject
{
    // 기획자가 채우는 속성
    [SerializeField] private string _name;

    [SerializeField] private float _defaultValue;
    [SerializeField] private float _increaseValue;
    [SerializeField] private float _defaultCost;
    [SerializeField] private float _increaseCost;

    public string Name => _name;
    public float DefaultValue => _defaultValue;
    public float IncreaseValue => _increaseValue;
    public float DefaultCost => _defaultCost;
    public float IncreaseCost => _increaseCost;
}
