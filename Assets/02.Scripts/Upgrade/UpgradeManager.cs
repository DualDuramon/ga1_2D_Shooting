using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private const string Upgrade_Save_Data_Key = "UpgradeSaveData";

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
        Load();
        RefreshUI();
    }

    public void LevelUp(int index)
    {
        Upgrade upgrade = _upgrades[index];
        if (!ScoreManager.Instance.TrySpendScore(upgrade.Cost))
        {
            return;
        }

        _upgrades[index].LevelUp();
        Save();
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_Upgrade ui in _upgradeUIs)
        {
            ui.Refresh();
        }
    }

    private void Save()
    {
        UpgradeSaveData saveData = new UpgradeSaveData(_upgrades.Length);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }

        //Json 포멧으로 저장.
        string json = JsonUtility.ToJson(saveData);
        string encryptedJson = Security.Encrypt(json);

        PlayerPrefs.SetString(Upgrade_Save_Data_Key, encryptedJson);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(Upgrade_Save_Data_Key))
        {
            Debug.LogWarning("데이터 로딩 실패) 세이브데이터 키가 존재하지 않습니다.");
            return;
        }

        string loadedData = PlayerPrefs.GetString(Upgrade_Save_Data_Key);
        string decryptedData = Security.Decrypt(loadedData);

        UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(decryptedData);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            Debug.Log($"{_upgrades[i].Name} 로드 완료!");
            _upgrades[i].SetLevel(saveData.Level[i]);
        }

        RefreshUI();
    }
}