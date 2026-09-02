using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //역할 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    
    //필요 속성
    //  - 총알프리펩
    public GameObject BulletPrefab;
    //  - 생성위치(총구)
    public Transform MuzzleLocation;

    private void Update()
    {
        //1. 스페이스바를 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //2. 총알 발사
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab);

        bullet.transform.position = MuzzleLocation.position;
    }
}
