using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private bool _isAutoMove = false;
    [SerializeField] private float _deadZoneX = 0.1f;
    private Vector2 _defaultPos;


    public bool IsAutoMove => _isAutoMove;

    private Transform _targetEnemy;

    private void Awake()
    {
        _defaultPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleAutoMove();
        }

        if (_isAutoMove && _targetEnemy == null)
        {
            SetNextEnemy();
        }
    }


    private void ToggleAutoMove()
    {
        _isAutoMove = !_isAutoMove;
    }

    private void SetNextEnemy() //TODO : 오브젝트 풀링이용
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemyList)
        {
            float dist = Vector2.SqrMagnitude(enemy.transform.position - transform.position);
            if (dist < minDist * minDist)
            {
                minDist = dist;
                _targetEnemy = enemy.transform;
            }
        }

    }

    public Vector2 GetMoveDirection()
    {
        if (_targetEnemy == null)
        {
            SetNextEnemy();
        }

        Vector2 dir = _defaultPos - (Vector2)transform.position;
        if (_targetEnemy != null)
        {
            dir = _targetEnemy.position - transform.position;

            if (Mathf.Abs(dir.x) < _deadZoneX)
            {
                dir.x = 0f;
            }
            dir.y = 0f;
        }

        dir = dir == Vector2.zero ? Vector2.zero : dir.normalized;

        return dir;
    }
}
