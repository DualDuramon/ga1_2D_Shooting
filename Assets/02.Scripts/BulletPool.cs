using UnityEngine;

//오브젝트 풀링이란 : 오브젝트 Pool(웅덩이, 창고)를 만들어두고
//그 창고 안에 게임 오브젝트를 미리 만든 후 필요할때마다 꺼내서 쓰고 반납하는 느낌의 메모리 최적화 기법이다.
//메모리 할당과 객체 생성파괴를 최소화 해서 성능UP 시킨다.
public class BulletPool : MonoBehaviour
{
    [Header("Bullet Pool Settings")]
    [SerializeField] private Bullet[] _bulletPrefabs;
    [SerializeField] private int _poolSize = 50;

    private Bullet[,] _pools; //풀 선택용 2차원 배열

    #region 싱글톤
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;
    #endregion

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        GenerateBulletInstances();
    }

    private void GenerateBulletInstances()
    {
        _pools = new Bullet[_bulletPrefabs.GetLength(0), _poolSize];

        for (int i = 0; i < _bulletPrefabs.GetLength(0); i++)
        {
            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = Instantiate(_bulletPrefabs[i], gameObject.transform);
                bullet.gameObject.SetActive(false); //사용할 것 아니기에 비활성화

                _pools[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType neededBulletType)
    {
        for (int i = 0; i < _pools.GetLength(0); i++)
        {
            if (_pools[i, 0].Type != neededBulletType)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = _pools[i, j];

                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}
