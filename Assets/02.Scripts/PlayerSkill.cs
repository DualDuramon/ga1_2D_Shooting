using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Bomb Renferences")]
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform _bombPosition;

    [SerializeField] private float _bombCoolTimeRate = 10f;
    private float _currentUseBomb = 0f;
    private const string Bomb_Location_Tag_Name = "BombLocation";

    private void Awake()
    {
        _currentUseBomb = Time.time;
        if (_bombPosition == null)
        {
            _bombPosition = GameObject.FindWithTag(Bomb_Location_Tag_Name).transform;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && Time.time - _currentUseBomb >= _bombCoolTimeRate)
        {
            UseBomb();
            ResetBombTimer();
        }
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
        _currentUseBomb = Time.time;
    }
}
