using UnityEngine;

public class RushEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    protected virtual void Initialize()
    {
        PlayerMove _targetPlayer = FindAnyObjectByType<PlayerMove>();
        if (_targetPlayer == null)
        {
            Debug.Log($"{gameObject.name} : 플레이어를 찾을 수 없음");
        }

        CalculateMoveDirection(_targetPlayer.transform);
    }

    protected void CalculateMoveDirection(Transform targetTransform)
    {
        if (targetTransform == null)
        {
            Debug.LogError($"{transform.name} : target이 사라졌습니다.");
            return;
        }
        Vector2 CalculatedDirection = targetTransform.position - transform.position;
        _moveDirection = CalculatedDirection.normalized;
    }

}
