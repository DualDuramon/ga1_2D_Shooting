using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Vector2 _moveDirection = Vector2.down;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _health = 100f;

    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        _health = _health - damageAmount;

        if (_health < 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}