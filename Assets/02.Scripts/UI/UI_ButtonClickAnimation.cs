using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClickAnimation : MonoBehaviour
{
    [Header("Click Feedback")]
    [SerializeField] private AnimationCurve _bumpCurve;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Button _button;

    private bool _isBumping = false;
    private float _elapsedTime = 0f;
    private const float BumpScale = 1.1f;
    private const float BumpDuration = 0.3f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(StartBumpAnimation);
    }

    private void Update()
    {
        HandleBumpAnimation();
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

        float time = _elapsedTime / BumpDuration;   //얼마나 지났는지 퍼센트(0~1);
        float curveValue = _bumpCurve.Evaluate(time);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * BumpScale, curveValue);
    }

    public void StartBumpAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0f;
        _audioSource.Play();
    }
}
