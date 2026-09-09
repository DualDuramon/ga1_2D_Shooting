using UnityEngine;


public class PlayerSound : MonoBehaviour
{
    [Header("Sound Setting")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _hitSoundClip;
    [SerializeField] private AudioClip _fireSoundClip;
    [Header("Component References")]
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private PlayerFire _attack;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<PlayerFire>();
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
        _attack.OnFire += PlayerFireSound;
    }

    private void UnsubscribeEvents()
    {
        _status.OnHit -= PlayHitSound;
        _attack.OnFire -= PlayerFireSound;
    }

    public void PlayHitSound()
    {
        PlaySoundOneShot(_hitSoundClip);
    }

    public void PlayerFireSound()
    {
        PlaySoundOneShot(_fireSoundClip);
    }

    private void PlaySoundOneShot(AudioClip clip)
    {
        _source.PlayOneShot(_hitSoundClip);
    }
}