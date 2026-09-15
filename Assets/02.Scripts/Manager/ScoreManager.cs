
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private int _bestScore = 0;
    private int _currentScore = 0;

    public int CurrentScore => _currentScore;
    public int BestScore => _bestScore;

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

        RefreshText();
    }

    private void RefreshText()
    {
        _bestScoreText.text = $"BestScore : {_bestScore:N0}";
        _currentScoreText.text = $"Score : {_currentScore:N0}";
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

        RefreshText();
    }

    private void SaveBestScoreData()
    {
        PlayerPrefs.SetInt(Best_Score_Save_Key, _bestScore);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 점수 소비 함수. amount만큼의 점수를 차감할 수 있는지 확인하고 소비가 가능하면 소비 로직을 진행한다.
    /// </summary>
    /// <param name="amount">차감할 점수의 양</param>
    /// <returns>점수 소비를 성공했는지 여부</returns>
    public bool TrySpendScore(int amount)
    {
        if (amount < 0)
        {
            return false;
        }

        int calculatedScore = _currentScore - amount;
        if (calculatedScore < 0)
        {
            return false;
        }
        else
        {
            _currentScore = calculatedScore;
            RefreshText();
            return true;
        }
    }
}