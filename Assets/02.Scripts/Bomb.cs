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

    [SerializeField] private float _minDistance = 1f;
    [SerializeField] private float _maxDistance = 2f;

    [SerializeField] private float _rotateSpeed = 180f;
    [SerializeField] private float _radiusChangeSpeed = 2f;

    private float _currentAngle;

    private void Update()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime >= _maxLifeTime)
        {
            Destroy(gameObject);
            return;
        }

        CalculateTrailPosition();
    }

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

    private void CalculateTrailPosition()
    {
        if (_trailTransform == null)
        {
            return;
        }

        _currentAngle += _rotateSpeed * Time.deltaTime;

        float radian = _currentAngle * Mathf.Deg2Rad;
        float radiusT = (Mathf.Sin(Time.time * _radiusChangeSpeed) + 1f) * 0.5f;
        float radius = Mathf.Lerp(_minDistance, _maxDistance, radiusT);

        Vector2 targetPosition = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * radius;

        _trailTransform.localPosition = targetPosition;
    }
}
