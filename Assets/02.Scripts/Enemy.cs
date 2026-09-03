using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 _moveDirection = Vector2.down;
    [SerializeField] private float _speed = 1.0f;

    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }

}