using System.Collections.Generic;
using UnityEngine;

//오브젝트 풀링이란 : 오브젝트 Pool(웅덩이, 창고)를 만들어두고
//그 창고 안에 게임 오브젝트를 미리 만든 후 필요할때마다 꺼내서 쓰고 반납하는 느낌의 메모리 최적화 기법이다.
//메모리 할당과 객체 생성파괴를 최소화 해서 성능UP 시킨다.
public class BulletPool : MonoBehaviour
{
    [Header("Bullet Prefab")]
    [SerializeField] private Bullet _mainBulletPrefab;
    [SerializeField] private Bullet _sideBulletPrefab;

    [Header("Pool Size")]
    [SerializeField] private int _mainBulletPoolSize = 50;
    [SerializeField] private int _sideBulletPoolSize = 50;

    private Bullet[] _mainBulletPool; //생성한 총알을 담아둘 Pool
    private Bullet[] _sideBulletPool; //생성한 사이드 총알을 담아둘 Pool

    private Dictionary<Bullet, Bullet[]> _poolDictionary = new Dictionary<Bullet, Bullet[]>(); //풀 선택용 딕셔너리

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
        _mainBulletPool = new Bullet[_mainBulletPoolSize];

        for (int i = 0; i < _mainBulletPoolSize; i++)
        {
            Bullet bullet = Instantiate(_mainBulletPrefab, gameObject.transform);
            bullet.gameObject.SetActive(false); //사용할 것 아니기에 비활성화

            _mainBulletPool[i] = bullet;  //pool 에 삽입
        }

        _sideBulletPool = new Bullet[_sideBulletPoolSize];

        for (int i = 0; i < _sideBulletPoolSize; i++)
        {
            Bullet bullet = Instantiate(_sideBulletPrefab, gameObject.transform);
            bullet.gameObject.SetActive(false);

            _sideBulletPool[i] = bullet;
        }

        _poolDictionary.Add(_mainBulletPrefab, _mainBulletPool);
        _poolDictionary.Add(_sideBulletPrefab, _sideBulletPool);
    }

    public Bullet GetBullet(Bullet neededBullet)
    {
        Bullet[] pool = _poolDictionary[neededBullet];

        foreach (Bullet bullet in pool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }

    public Bullet GetMainBullet()
    {
        foreach (Bullet bullet in _mainBulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }

    public Bullet GetSideBullet()
    {
        foreach (Bullet bullet in _sideBulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }
}
