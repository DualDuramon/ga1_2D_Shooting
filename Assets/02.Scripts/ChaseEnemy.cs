using UnityEngine;

public class ChaseEnemy : RushEnemy
{
    [SerializeField] protected PlayerMove _targetPlayer;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
    protected override void Initialize()
    {
        _targetPlayer = FindAnyObjectByType<PlayerMove>();
        if (_targetPlayer == null)
        {
            Debug.Log("Player Not Find");
            return;
        }

        CalculateMoveDirection(_targetPlayer.transform);
        Debug.Log($"{gameObject.name} : 이니셜라이징 끝");
    }

    protected override void Update()
    {
        if (_targetPlayer != null)
        {
            base.Update();
            CalculateMoveDirection(_targetPlayer.transform);
        }
    }
}