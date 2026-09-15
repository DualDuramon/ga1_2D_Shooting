using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] protected Vector2 _moveDirection = Vector2.down;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private int _baseHealth = 70; // enemy의 기준 체력
    [SerializeField] private int _health = 70;

    [Header("Combat Variables")]
    [SerializeField] private float _attackDamage = 300f;
    [SerializeField] private LayerMask _damagableLayers;

    [Header("ItemGenerate")]
    [SerializeField] private EnemyItemGenerator _generator;

    [Header("Animator Parameters")]
    [SerializeField] private Animator _animator;
    private const string HitTrigger = "HitTrigger";

    [Header("Effect References")]
    [SerializeField] private GameObject _deathFxPrefab;
    [SerializeField] private float _effectScale = 1f;

    [Header("Audio References")]
    [SerializeField] private AudioSource _damagedSoundSource;

    protected virtual void Awake()
    {
        _generator = GetComponent<EnemyItemGenerator>();
        _animator = GetComponent<Animator>();
        _damagedSoundSource = GetComponent<AudioSource>();
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

    public void SetHealthBalance(float multiplier)
    {
        Debug.Log($"{gameObject.name} : 체력 수정 -> {multiplier}");
        _health = (int)(_baseHealth * multiplier);
    }

    private void Move()
    {
        transform.position += (Vector3)_moveDirection.normalized * _speed * Time.deltaTime;
    }

    public void TakeDamage(int damageAmount)
    {
        _health = _health - (int)damageAmount;

        if (_health < 0f)
        {
            Die();
        }
        else
        {
            TriggerHitAnimation();
            PlayHitSound();
        }
    }

    private void TriggerHitAnimation()
    {
        if (_animator == null)
        {
            Debug.LogWarning($"{gameObject.name} : don't have animator component!");
            return;
        }
        _animator.SetTrigger(HitTrigger);
    }

    private void PlayHitSound()
    {
        if (_damagedSoundSource == null || _damagedSoundSource.clip == null)
        {
            Debug.LogWarning($"{gameObject.name} : Can't Play Hit Sound. Check AudioClip Componenet !");
            return;
        }
        _damagedSoundSource.Play();
    }

    private void Die()
    {
        GenerateItemRandomly();
        SpawnDeathEffect();

        ScoreManager.Instance.AddScore(100); //점수 매니저 싱글톤

        Destroy(gameObject);
    }

    private void SpawnDeathEffect()
    {
        GameObject effect = Instantiate(_deathFxPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = new Vector3(_effectScale, _effectScale, _effectScale);
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