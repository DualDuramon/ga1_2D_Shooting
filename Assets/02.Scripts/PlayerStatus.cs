using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _fireCoolTime = 0.3f;
    private const float _fireMinCoolTime = 0.1f;


    public float MoveSpeed { get { return _moveSpeed; } }
    public float FireCoolTime { get { return _fireCoolTime; } }

    public Action OnDeath;

    public void TakeDamage(float damageAmount)
    {
        _health = _health - damageAmount;


        if (_health < 0f)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        _health = _health + healAmount;
    }

    public void AdjustSpeed(float addedSpeed)
    {
        _moveSpeed += addedSpeed;
        _moveSpeed = 0 <= _moveSpeed ? _moveSpeed : 0;
    }

    public void AdjustFireDuration(float addedDuration)
    {
        _fireCoolTime += addedDuration;
        _fireCoolTime = (_fireMinCoolTime <= _fireCoolTime) ? _fireCoolTime : _fireMinCoolTime;
    }


    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

}
