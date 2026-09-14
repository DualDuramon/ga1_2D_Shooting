using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //관리 : 업그레이드들에 대한 무결성과 생성,조회, 수정, 삭제 등의 게임 관련 로직
    private static UpgradeManager _instance;
    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<UpgradeManager>();
            }

            return _instance;
        }
    }


    [SerializeField] private Upgrade[] _upgrades;

    public Upgrade[] Upgrades => _upgrades;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void LevelUp(int index)
    {
        _upgrades[index].LevelUp();
    }

}
