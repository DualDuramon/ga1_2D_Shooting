using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //역할 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    
    //필요 속성
    //  - 총알프리펩
    public GameObject BulletPrefab;
    //  - 생성위치(총구)
    public Transform[] MuzzleLocation;
    public float FireCoolTime = 0.3f;

    private float _lastFireTime = 0f;
    public bool AutometicFire = false;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleFireMode();
        }

        if(Time.time - _lastFireTime > FireCoolTime)
        {
            if(AutometicFire || Input.GetKeyDown(KeyCode.Space))
            {
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        foreach(Transform muzzleTf in MuzzleLocation)
        {
            Instantiate(BulletPrefab, muzzleTf.position, Quaternion.identity);
        }

        _lastFireTime = Time.time;
    }

    private void ToggleFireMode()
    {
        AutometicFire = !AutometicFire;
    }
}
