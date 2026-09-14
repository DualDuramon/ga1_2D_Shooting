using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    //버튼을 클릭하면 Toggle 하고 싶음.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격

    [Header("On/Off Sprites")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;


    private Image _myImage;
    private bool _autoMode = false;
    private PlayerAutoMove _playerAutoMove;

    private void Awake()
    {
        _myImage = GetComponent<Image>(); //button의 이미지를 가져오게함
        UpdateButtonImage();
    }

    private void Start()
    {
        _playerAutoMove = FindAnyObjectByType<PlayerAutoMove>();
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;
        _playerAutoMove.SetAutoMove(_autoMode);

        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}
