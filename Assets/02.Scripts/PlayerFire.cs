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

    private void Update()
    {
        //1. 스페이스바를 누르면
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - _lastFireTime > FireCoolTime)
        {
            //2. 총알 발사
            FireBullet();
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
}
