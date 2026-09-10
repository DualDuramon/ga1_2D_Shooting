using UnityEngine;

// 역할 : 키보드 입력에 따라서 플레이어 입력 처리.
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerEffect _effect;
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private PlayerAutoMove _autoMove;

    private const string Dir_X = "dirX";

    private PlayerCommandInvoker _invoker;

    private Vector2 _boundSize = Vector2.zero;

    public bool CanReadInput
    {
        get
        {
            return _invoker.CanReadInput;
        }
    }

    private void Awake()
    {
        _boundSize.y = Screen.height;
        _boundSize.x = Screen.width;
        _invoker = GetComponent<PlayerCommandInvoker>();
        _status = GetComponent<PlayerStatus>();
        _animator = GetComponent<Animator>();
        _effect = GetComponent<PlayerEffect>();
        _autoMove = GetComponent<PlayerAutoMove>();
    }

    // 매 프레임마다 호출되는 함수
    // 컴퓨터마다 뽑히는 프레임이 다름을 유의해라.
    private void Update()
    {
        if (!CanReadInput) return;

        Move();
    }

    private void Move()
    {
        Vector2 dir = Vector2.zero;

        if (_autoMove.IsAutoMove)
        {
            dir = _autoMove.GetMoveDirection();
        }
        else
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            dir = new Vector2(h, v);
        }

        ExecutePlayerMove(dir);
        _animator.SetInteger(Dir_X, (int)dir.x);
    }

    private void LimitPlayerTransform()
    {
        Vector2 boundSize = new Vector2(_boundSize.x, _boundSize.y / 2);

        Vector2 bottomLimit = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 ceilLimit = Camera.main.ScreenToWorldPoint(boundSize);

        Vector2 newPosition = transform.position;

        if (transform.position.x > ceilLimit.x)
        {
            newPosition.x = ceilLimit.x;
        }
        else if (transform.position.x < bottomLimit.x)
        {
            newPosition.x = bottomLimit.x;
        }

        if (transform.position.y > ceilLimit.y)
        {
            newPosition.y = ceilLimit.y;
        }
        else if (transform.position.y < bottomLimit.y)
        {
            newPosition.y = bottomLimit.y;
        }

        transform.position = newPosition;
    }

    public void ExecutePlayerMove(Vector2 direction)
    {
        Vector2 normalizedDirection = direction.normalized;
        transform.Translate(normalizedDirection * _status.MoveSpeed * Time.deltaTime);
        _effect.SetTrailEffect(Vector2.Dot(normalizedDirection, transform.up) > 0);
        LimitPlayerTransform();
    }

    public void ResetPlayerLocation()
    {
        transform.position = Vector2.zero;
    }
}