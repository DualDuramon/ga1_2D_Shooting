using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //역할 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    //필요 속성
    //  - 총알프리펩
    public GameObject BulletPrefab;
    public GameObject SideBulletPrefab;
    //  - 생성위치(총구)
    public Transform[] MuzzleLocation;
    public Transform[] SideMuzzleLocation;
    public float FireCoolTime = 0.3f;

    private float _lastFireTime = 0f;
    public bool AutometicFire = false;

    private PlayerCommandInvoker _invoker;
    public bool IsReplaying => !_invoker.CanReadInput;


    private void Awake()
    {
        _invoker = GetComponent<PlayerCommandInvoker>();
    }

    private void Update()
    {
        if (IsReplaying) return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleFireMode();
        }

        if(Time.time - _lastFireTime > FireCoolTime)
        {
            if(AutometicFire || Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteFire();
            }
        }
    }

    public void ExecuteFire()
    {
        FireMainBullet();
        FireSideBullet();
        _lastFireTime = Time.time;
    }    

    private void FireMainBullet()
    {
        GenerateBullet(BulletPrefab, MuzzleLocation);
    }
    private void FireSideBullet()
    {
        GenerateBullet(SideBulletPrefab, SideMuzzleLocation);
    }

    private void GenerateBullet(GameObject bulletPrefab, Transform[] locations)
    {
        foreach (Transform muzzleTf in locations)
        {
            Instantiate(bulletPrefab, muzzleTf.position, Quaternion.identity);
        }
    }

    private void ToggleFireMode()
    {
        AutometicFire = !AutometicFire;
    }
}
