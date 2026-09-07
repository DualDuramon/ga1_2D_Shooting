using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Item Basic Setting")]
    [SerializeField] private float _timeToActivate = 3f;

    [Header("Absorb Setting")]
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private float _curveAmount = 2f;

    [SerializeField] private PlayerStatus _targetStatus;

    private float _activateTimer;
    private float _moveTimer;

    private bool _isMoving;

    private Vector2 _startPosition;
    private Vector2 _controlPoint;


    private void Start()
    {
        InitializeParameters();
    }

    private void Update()
    {
        if (!_isMoving)
        {
            WaitForActivation();
            return;
        }

        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.TryGetComponent(out PlayerStatus status))
        {
            ApplyEffect(status);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} : Player doesn't have PlayerStatus!");
        }

        Destroy(gameObject);
    }

    private void InitializeParameters()
    {
        _targetStatus = FindFirstObjectByType<PlayerStatus>();

        if (_targetStatus == null)
        {
            Debug.LogWarning($"{gameObject.name} : Initialize Fail, Can't Find Player!");
        }
    }

    private void WaitForActivation()
    {
        _activateTimer += Time.deltaTime;

        if (_activateTimer < _timeToActivate)
        {
            return;
        }

        StartMoving();
    }

    private void StartMoving()
    {
        if (_targetStatus == null)
        {
            return;
        }

        _isMoving = true;
        _moveTimer = 0f;

        _startPosition = transform.position;

        Vector2 targetPosition = _targetStatus.transform.position;
        Vector2 direction = (targetPosition - _startPosition).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        Vector2 center = (_startPosition + targetPosition) * 0.5f;
        float curveDirection = Random.value < 0.5f ? -1f : 1f;

        _controlPoint = center + perpendicular * _curveAmount * curveDirection;
    }

    private void MoveToPlayer()
    {
        if (_targetStatus == null)
        {
            return;
        }

        _moveTimer += Time.deltaTime;

        float t = Mathf.Clamp01(_moveTimer / _moveDuration);

        float easedT = t * t; // 빨려 들어가는 느낌을 위해 후반부로 갈수록 빠르게 이동


        Vector2 targetPosition = _targetStatus.transform.position;

        transform.position = CalculateBezier(_startPosition, _controlPoint, targetPosition, easedT);
    }

    private Vector2 CalculateBezier(Vector2 start, Vector2 control, Vector2 end, float t)
    {
        Vector2 a = Vector2.Lerp(start, control, t);
        Vector2 b = Vector2.Lerp(control, end, t);

        return Vector2.Lerp(a, b, t);
    }

    protected abstract void ApplyEffect(PlayerStatus player);
}