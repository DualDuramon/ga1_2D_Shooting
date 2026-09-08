using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Bomb Setting")]
    [SerializeField] private float _maxLifeTime = 10f;
    private float _lifeTime = 0f;
    [SerializeField] private LayerMask _damageableMask;
    [SerializeField] private float _damage;

    [Header("Effect Setting")]
    [SerializeField] private Transform _trailTransform;
    [SerializeField] private float _minDistance = -2f;
    [SerializeField] private float _maxDistance = -1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _damageableMask.value) != 0)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void Update()
    {
        if (_maxLifeTime <= _lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            _lifeTime += Time.deltaTime;
            CalculateRandomTrailPosition();
        }
    }

    private void CalculateRandomTrailPosition()
    {
        if (_trailTransform == null) return;

        float randomDistance = Random.Range(_minDistance, _maxDistance);
        _trailTransform.localPosition = new Vector2(randomDistance, 0f);
    }
}
