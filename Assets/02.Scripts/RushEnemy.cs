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
            return;
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
        Vector2 calculatedDirection = targetTransform.position - transform.position;
        _moveDirection = calculatedDirection.normalized;

        RotateToward(_moveDirection);
    }

    private void RotateToward(Vector2 to)
    {
        float rotateAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle + 90f);
    }
}
