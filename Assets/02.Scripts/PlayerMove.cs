using UnityEditor.XR;
using UnityEngine;

//역할 : 키보드 입력에 따라서 플레이어 입력 처리.
public class PlayerMove : MonoBehaviour
{
    public float Speed = 0.05f;

    private Vector2 _boundSize = Vector2.zero;
    
    private void Awake()
    {
        _boundSize.y = Screen.height;
        _boundSize.x = Screen.width;

    }

    //매 프레임마다 호출되는 함수
    //컴퓨터마다 뽑히는 프레임이 다름을 유의해라.
    private void Update()
    {
        ////입력처리 방식 1
        ////1. 키보드 입력을 받는다
        //if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //2. 키보드 입력에 따라 방향을 구한다
        //    //게임에는 벡터라는 타입이 있고, 벡터는 (크기와 방향)을 의미한다.
        //    Vector2 dir = new Vector2(-1, 0); //Vector2.left 와 같음


        //    //3. 방향과 속도에 따라 이동한다.
        //    transform.Translate(dir * Speed * Time.deltaTime); //매직넘버 : 보는 사람에 다라 의미가 달라질 수 있는 헷갈리는 숫자
        //}


        //입력 처리 방식2
        float h = Input.GetAxisRaw("Horizontal"); //키보드 입력 상태에 따라 -1f ~ 0 ~ 1f를 반환
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(h, v);
        Vector2 normalizedSpeed = dir.normalized;
        transform.Translate(normalizedSpeed * Speed * Time.deltaTime);



        //입력처리방식3 : 다음위치 = 현재위치 + 속도 * 시간
        //transform.position = (Vector2)transform.position + dir * Speed * Time.deltaTime;
        LimitPlayerTransform();
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
}
