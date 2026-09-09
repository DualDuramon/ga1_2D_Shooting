using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Bomb Renferences")]
    [SerializeField] private int _maxBombCount = 3;
    private int _currentBombCount = 1;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombPosition;

    [SerializeField] private float _bombCoolTimeRate = 10f;
    private float _currentBombTime = 0f;
    private const string Bomb_Location_Tag_Name = "BombLocation";

    private void Awake()
    {
        _currentBombTime = Time.time;
        if (_bombPosition == null)
        {
            _bombPosition = GameObject.FindWithTag(Bomb_Location_Tag_Name).transform;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && CanUseBomb())
        {
            UseBomb();
            ResetBombTimer();
        }
    }

    public void AddBombCount(int amount)
    {
        _currentBombCount += amount;
        _currentBombCount = Mathf.Clamp(_currentBombCount, 0, _maxBombCount);
    }

    public bool CanUseBomb()
    {
        return Time.time - _currentBombTime >= _bombCoolTimeRate && _currentBombCount > 0;
    }

    private void UseBomb()
    {
        if (_bombPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name} : _bombPrefab is Null! Can't Use Bomb");
            return;
        }

        Instantiate(_bombPrefab, _bombPosition.position, Quaternion.identity);
    }

    private void ResetBombTimer()
    {
        _currentBombTime = Time.time;
    }
}
