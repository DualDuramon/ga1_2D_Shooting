using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction = Vector2.up;
    public float MoveSpeed = 0f;
    public float LifeTime = 1f;
    public float Damage = 40f;

    private float _generatedTime = 0f;

    private void Awake()
    {
        _generatedTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _generatedTime >= LifeTime)
        {
            Destroy(gameObject);
            return;
        }
        transform.Translate(Direction * MoveSpeed * Time.deltaTime);
    }

    public void SetUp(Vector2 dir, float speed, float lifeTime)
    {
        Direction = dir;
        MoveSpeed = speed;
        LifeTime = lifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //GetComponent<T>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
    }

}