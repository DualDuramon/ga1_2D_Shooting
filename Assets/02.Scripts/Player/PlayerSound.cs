using UnityEngine;


public class PlayerSound : MonoBehaviour
{
    [Header("Sound Setting")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _hitSoundClip;
    [Header("Component References")]
    [SerializeField] private PlayerStatus _status;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _status = GetComponent<PlayerStatus>();
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _status.OnHit += PlayHitSound;
    }

    private void UnsubscribeEvents()
    {
        _status.OnHit -= PlayHitSound;
    }

    public void PlayHitSound()
    {
        PlaySoundOneShot(_hitSoundClip);
    }

    private void PlaySoundOneShot(AudioClip clip)
    {
        _source.PlayOneShot(_hitSoundClip);
    }
}