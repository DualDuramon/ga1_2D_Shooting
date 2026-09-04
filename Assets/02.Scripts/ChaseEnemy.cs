using UnityEngine;

public class ChaseEnemy : RushEnemy
{
    [SerializeField] protected PlayerMove _targetPlayer;

    protected override void Initialize()
    {
        _targetPlayer = FindAnyObjectByType<PlayerMove>();

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