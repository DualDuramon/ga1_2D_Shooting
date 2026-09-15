using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private PlayerAutoMove _autoMove;


    public Transform[] MuzzleLocation;
    public Transform[] SideMuzzleLocation;

    private float _lastFireTime = 0f;

    private PlayerCommandInvoker _invoker;
    public bool IsReplaying => !_invoker.CanReadInput;

    public event Action OnFire;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
        _invoker = GetComponent<PlayerCommandInvoker>();
        _autoMove = GetComponent<PlayerAutoMove>();
    }

    private void Update()
    {
        if (IsReplaying) return;


        if (CanFire())
        {
            //if (_autoMove.IsAutoPlay || _automaticFire || Input.GetKeyDown(KeyCode.Space))
            //{
            //    ExecuteFire();
            //}
            ExecuteFire();
        }
    }

    private bool CanFire()
    {
        return Time.time - _lastFireTime > _status.FireRate - UpgradeManager.Instance.Upgrades[1].CurrentValue;
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
        SpawnBullet(BulletType.Main, MuzzleLocation);
    }

    private void FireSideBullet()
    {
        SpawnBullet(BulletType.Sub, SideMuzzleLocation);
    }

    private void SpawnBullet(BulletType neededBulletType, Transform[] locations)
    {
        foreach (Transform muzzleTf in locations)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(neededBulletType);
            bullet.transform.position = muzzleTf.position;
        }
    }
}