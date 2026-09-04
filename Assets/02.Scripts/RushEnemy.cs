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

        CalculateMoveDirection(_targetPlayer.transform);
        Debug.Log($"{gameObject.name} : ¿Ã¥œº»∂Û¿Ã¬° ≥°");
    }



    protected void CalculateMoveDirection(Transform targetTransform)
    {
        Vector2 CalculatedDirection = targetTransform.position - transform.position;
        _moveDirection = CalculatedDirection.normalized;
    }

}
