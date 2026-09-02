using UnityEngine;

//역할 : 키보드 입력에 따라서 플레이어 입력 처리.
public class PlayerMove : MonoBehaviour
{
    private PlayerCommandInvoker _invoker;
    public float Speed = 0.05f;
    public float IncreaseSpeedAmount = 1.0f;
    public float DecreaseSpeedAmount = -1.0f;

    private Vector2 _boundSize = Vector2.zero;

    public bool CanReadInput { 
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
    }

    //매 프레임마다 호출되는 함수
    //컴퓨터마다 뽑히는 프레임이 다름을 유의해라.
    private void Update()
    {
        if (!CanReadInput) return;
        
        Move();
        SpeedChange();
    }

    private void Move()
    {
        //입력 처리 방식2
        float h = Input.GetAxisRaw("Horizontal"); //키보드 입력 상태에 따라 -1f ~ 0 ~ 1f를 반환
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(h, v);
        ExecutePlayerMove(dir);
    }

    private void SpeedChange()
    {
        if (Input.GetKey(KeyCode.E))
        {
            AdjustSpeed(IncreaseSpeedAmount);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            AdjustSpeed(DecreaseSpeedAmount);
        }
    }

    private void AdjustSpeed(float addedSpeed)
    {
        Speed += addedSpeed;
        Speed = 0 <= Speed ? Speed : 0; 
    }

    private void LimitPlayerTransform()
    {
        Vector2 boundSize = new Vector2(_boundSize.x, _boundSize.y / 2);
        
        Vector2 bottomLimit = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 ceilLimit = Camera.main.ScreenToWorldPoint(boundSize);

        Vector2 newPosition = transform.position;

        if(transform.position.x > ceilLimit.x)
        {
            newPosition.x = bottomLimit.x;
        }
        else if(transform.position.x < bottomLimit.x)
        {
            newPosition.x = ceilLimit.x;
        }

        if (transform.position.y > ceilLimit.y)
        {
            newPosition.y = bottomLimit.y;
        }
        else if (transform.position.y < bottomLimit.y)
        {
            newPosition.y = ceilLimit.y;
        }

        transform.position = newPosition;
    }

    public void ExecutePlayerMove(Vector2 direction)
    {
        Vector2 normalizedSpeed = direction.normalized;
        transform.Translate(normalizedSpeed * Speed * Time.deltaTime);
        LimitPlayerTransform();
    }

    public void ResetPlayerLocation()
    {
        transform.position = Vector2.zero;
    }
}
