using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private bool _isAutoPlay = false;
    [SerializeField] private float _ignoreTrackingY = 2f;
    private Vector2 _defaultPos;

    public bool IsAutoPlay => _isAutoPlay;

    private Transform _targetEnemy;

    private void Awake()
    {
        _defaultPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAutoMove(!_isAutoPlay);
        }

        if (_isAutoPlay && _targetEnemy == null)
        {
            SetNextEnemy();
        }
    }


    public void SetAutoMove(bool auto)
    {
        _isAutoPlay = auto;
    }

    private void SetNextEnemy() //TODO : 오브젝트 풀링이용
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemyList)
        {
            if (enemy.transform.position.y < _ignoreTrackingY) continue;

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


        Vector2 diff = (_defaultPos - (Vector2)transform.position);
        Vector2 dir = diff;

        if (_targetEnemy != null)
        {
            dir = (_targetEnemy.position - transform.position);
            if (diff.y >= 3)
            {
                dir.y = 1f;

            }
            else
            {
                diff.y = -1f;

            }
        }

        return dir.normalized;
    }
}
