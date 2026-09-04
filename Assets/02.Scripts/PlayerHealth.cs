using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public Action OnDeath;

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
        OnDeath?.Invoke();
        Debug.Log("플레이어가 죽었슴!");
        Destroy(gameObject);
    }

}
