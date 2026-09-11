using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;
    // 역할 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알프리팹
    public Bullet BulletPrefab;
    public Bullet SideBulletPrefab;

    // - 생성위치(총구)
    public Transform[] MuzzleLocation;
    public Transform[] SideMuzzleLocation;

    private float _lastFireTime = 0f;
    public bool AutomaticFire = false;

    private PlayerCommandInvoker _invoker;
    public bool IsReplaying => !_invoker.CanReadInput;

    public event Action OnFire;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
        _invoker = GetComponent<PlayerCommandInvoker>();
    }

    private void Update()
    {
        if (IsReplaying) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleFireMode();
        }

        if (CanFire())
        {
            if (AutomaticFire || Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteFire();
            }
        }
    }

    private bool CanFire()
    {
        return Time.time - _lastFireTime > _status.FireRate;
    }

    public void ExecuteFire()
    {
        FireMainBullet();
        FireSideBullet();

        OnFire?.Invoke();
        _lastFireTime = Time.time;
    }

    private void FireMainBullet()
    {
        //SpawnMainBullet(MuzzleLocation);
        SpawnBullet(BulletPrefab, MuzzleLocation);
    }

    private void FireSideBullet()
    {
        //SpawnSideBullet(SideMuzzleLocation);
        SpawnBullet(SideBulletPrefab, SideMuzzleLocation);
    }

    private void SpawnBullet(Bullet neededBullet, Transform[] locations)
    {
        foreach (Transform muzzleTf in locations)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(neededBullet);
            bullet.transform.position = muzzleTf.position;
        }
    }

    private void ToggleFireMode()
    {
        AutomaticFire = !AutomaticFire;
    }
}