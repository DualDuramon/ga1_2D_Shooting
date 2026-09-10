
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    //관리 : 특정 데이터에 대한 무결성과 읽기, 추가, 수정, 삭제 등과 관련된 로직들을 일컫는다.
    private int _bestScore = 0;
    private int _currentScore = 0;

    //UI 책임 추가(TMP 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    //저장 키
    private const string Best_Score_Save_Key = "BestScore";

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(Best_Score_Save_Key))
        {
            _bestScore = PlayerPrefs.GetInt(Best_Score_Save_Key);
        }
        else
        {
            _bestScore = PlayerPrefs.GetInt(Best_Score_Save_Key, 0);
        }

        RefreshTextPerText();
    }

    private void RefreshTextPerText()
    {
        _bestScoreText.text = $"BestScore : {_bestScore}";
        _currentScoreText.text = $"Score : {_currentScore}";
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            SaveBestScoreData();
        }

        RefreshTextPerText();
    }

    private void SaveBestScoreData()
    {
        PlayerPrefs.SetInt(Best_Score_Save_Key, _bestScore);
        PlayerPrefs.Save();
    }
}