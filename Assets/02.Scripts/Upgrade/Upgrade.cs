using UnityEngine;

[System.Serializable]
public class Upgrade
{
    // 기획자가 채우는 속성
    [SerializeField] UpgradeBaseDataSO _baseData;

    // 실행중에 동적으로 바뀌 속성
    private int _level = 1;
    private float _currentValue;
    private float _nextValue;
    private int _cost;

    public string Name => _baseData.Name;
    public int Level => _level;
    public float CurrentValue => _currentValue;
    public float NextValue => _nextValue;
    public int Cost => _cost;

    public Upgrade(int level, string name, float defaultValue, float increaseValue, float increaseCost)
    {
        //_level = level;
        //_name = name;
        //_defaultValue = defaultValue;
        //_increaseValue = increaseValue;
        //_increaseCost = increaseCost;

        Calculate();
    }

    public void LevelUp()
    {
        _level += 1;
        Calculate();
    }

    private void Calculate()
    {
        // Todo: 공식에 따라 변화
        // Value : 기본 벨류 + 레벨 * 증가량 밸류
        // Cost  : 기본 점수 * 증가량 점수 ^ 레벨

        _currentValue = _baseData.DefaultValue + _level * _baseData.IncreaseValue;
        _nextValue = _baseData.DefaultValue + (_level + 1) * _baseData.IncreaseValue;
        _cost = (int)(_baseData.DefaultCost + Mathf.Pow(_baseData.IncreaseCost, _level));
    }
}