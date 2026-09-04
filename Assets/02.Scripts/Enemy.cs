using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] protected Vector2 _moveDirection = Vector2.down;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _health = 100f;

    [Header("Combat Variables")]
    [SerializeField] private float _attackDamage = 300f;
    [SerializeField] private LayerMask _damagableLayers;

    [Header("ItemGenerate")]
    [SerializeField] private EnemyItemGenerator _generator;

    protected virtual void Awake()
    {
        _generator = GetComponent<EnemyItemGenerator>();
        _damagableLayers = LayerMask.GetMask("Player");
    }

    protected virtual void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _damagableLayers) != 0)
        {
            if (other.gameObject.TryGetComponent(out PlayerStatus player))
            {
                player.TakeDamage(_attackDamage);
            }
            Die();
        }
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
        GenerateItemRandomly();
        Destroy(gameObject);
    }

    private void GenerateItemRandomly()
    {
        if (_generator == null)
        {
            Debug.Log($"{gameObject.name} : 아이템 생성 컴포넌트가 없습니다.");
            return;
        }

        _generator.SpawnItemRandomly();
    }
}