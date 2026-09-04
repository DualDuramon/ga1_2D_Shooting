using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Item Basic Setting")]
    [SerializeField] private float _timeToActivate = 3f;
    [SerializeField] private float _timer = 0f;
    [SerializeField] private Vector2 _moveDirection = Vector2.down;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private PlayerStatus _targetStatus;


    private void Start()
    {
        InitializeParameters();
        _timer = 0f;
    }

    private void Update()
    {
        if (_timer > _timeToActivate)
        {
            Move();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //적용요청하기
            if (collision.TryGetComponent(out PlayerStatus status))
            {
                ApplyEffect(status);
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} : player dosen't have Status!!");
            }

            Destroy(gameObject);
        }
    }

    private void InitializeParameters()
    {
        _targetStatus = FindFirstObjectByType<PlayerStatus>();

        if (_targetStatus == null)
        {
            Debug.LogWarning($"{gameObject.name} : InitializeFail, Can't Find Player!");
        }
    }

    private void CalculateMoveDirection()
    {
        if (_targetStatus == null)
        {
            Debug.LogWarning($"{gameObject.name} : Can't Find Player!");
            return;
        }

        _moveDirection = (_targetStatus != null) ? (_targetStatus.transform.position - transform.position).normalized : _moveDirection;
    }

    private void Move()
    {
        CalculateMoveDirection();
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    protected abstract void ApplyEffect(PlayerStatus player);

}
