using UnityEngine;

public class RushEnemy : Enemy
{
    [SerializeField] protected PlayerMove _targetPlayer;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _targetPlayer = FindAnyObjectByType<PlayerMove>();

        CalculateMoveDirection(_targetPlayer.transform);
        Debug.Log($"{gameObject.name} : ¿Ã¥œº»∂Û¿Ã¬° ≥°");
    }

    protected void CalculateMoveDirection(Transform targetTransform)
    {
        Vector2 CalculatedDirection = targetTransform.position - transform.position;
        _moveDirection = CalculatedDirection.normalized;
    }

}
