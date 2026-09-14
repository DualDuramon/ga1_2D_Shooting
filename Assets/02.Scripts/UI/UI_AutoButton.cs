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

    [Header("Button Click Feedback")]
    [SerializeField] private AudioSource _audioSource;
    private const float ButtonClickInterval = 0.3f;
    private float _lastButtonClickTime = 0f;

    [Header("Click Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationCurve _bumpCurve;

    private Image _myImage;
    private bool _autoMode = false;
    private PlayerAutoMove _playerAutoMove;

    private bool _isBumping = false;
    private float _elapsedTime = 0f;
    private const float BumpScale = 1.1f;
    private const float BumpDuration = 0.3f;


    private void Awake()
    {
        _myImage = GetComponent<Image>(); //button의 이미지를 가져오게함
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        UpdateButtonImage();
    }

    private void Start()
    {
        _playerAutoMove = FindAnyObjectByType<PlayerAutoMove>();
    }

    private void Update()
    {
        HandleBumpAnimation();
    }

    public void AutoToggle()
    {
        if (!CanActivateButton()) return;

        _autoMode = !_autoMode;
        _playerAutoMove.SetAutoMove(_autoMode);

        UpdateButtonImage();
        PlayButtonFeedback();
        _lastButtonClickTime = Time.time;
    }

    private void UpdateButtonImage()
    {
        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }

    public void PlayButtonFeedback()
    {
        StartBumpAnimation();
        _audioSource.Play();
    }

    private bool CanActivateButton()
    {
        return ButtonClickInterval < Time.time - _lastButtonClickTime;
    }

    private void HandleBumpAnimation()
    {
        if (!_isBumping) return;

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        float time = _elapsedTime / BumpDuration; //얼마나 지났는지 퍼센트(0~1);
        float curveValue = _bumpCurve.Evaluate(time);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * BumpScale, curveValue);
    }

    private void StartBumpAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0f;
    }
}
