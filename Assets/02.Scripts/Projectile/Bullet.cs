using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] public BulletType Type;

    public Vector2 Direction = Vector2.up;
    public float MoveSpeed = 0f;
    public float LifeTime = 1f;
    public float Damage = 40f;

    private float _generatedTime = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time - _generatedTime >= LifeTime)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
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

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void OnSpawn()
    {
        //프리펩이 활성화 될 때마다 pool에 의해서 초기화 하는 코드들이 들어간다
        _generatedTime = Time.time;
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }
}