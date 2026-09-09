using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _fireRate = 0.3f;
    private const float Fire_Min_CoolTime = 0.1f;


    //캡슐화 : 데이터 은닉(health를 private 처리) + 행위를 통한 상태 변경(TakeDamage()와 Heal())
    //getter or setter : 특정 데이터를 get or set 해주는 메서드
    public float MoveSpeed { get { return _moveSpeed; } }
    public float FireRate { get { return _fireRate; } }
    public float CurrentHealth => _health;
    //{
    //    get
    //    { 
    //        return _health; //get만 있으면 읽기전용 , set만 있으면 쓰기전용 프로퍼티. 둘다 있으면 프로퍼티 라 부른다. 단순데이터의 +set아니면 웬만하면 쓰지마라
    //    }
    //}
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

    public void AdjustMoveSpeed(float addedSpeed)
    {
        _moveSpeed += addedSpeed;
        _moveSpeed = 0 <= _moveSpeed ? _moveSpeed : 0;
    }

    public void AdjustFireDuration(float addedDuration)
    {
        _fireRate += addedDuration;
        _fireRate = (Fire_Min_CoolTime <= _fireRate) ? _fireRate : Fire_Min_CoolTime;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

}
