using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction = Vector2.up;
    public float Speed = 0f;
    public float LifeTime = 1f;

    private float _generatedTime = 0f;

    private void Awake()
    {
        _generatedTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - _generatedTime >= LifeTime)
        {
            Destroy(this);
            return;
        }
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    public void SetUp(Vector2 dir, float speed, float lifeTime)
    {
        Direction = dir;
        Speed = speed;
        LifeTime = lifeTime;
    }
}
