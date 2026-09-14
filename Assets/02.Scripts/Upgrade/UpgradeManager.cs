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

    //업그레이드 도메인 클래스
    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    //업그레이드 UI
    [SerializeField] private UI_Upgrade[] _upgradeUIs;


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

    private void Start()
    {
        RefreshUI();
    }

    public void LevelUp(int index)
    {
        Upgrade upgrade = _upgrades[index];
        if (ScoreManager.Instance.CurrentScore < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.SpendScore(upgrade.Cost);
        _upgrades[index].LevelUp();
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_Upgrade ui in _upgradeUIs)
        {
            ui.Refresh();
        }
    }
}
