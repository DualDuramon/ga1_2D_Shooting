using UnityEngine;


public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _hitSoundClip;
    [Header("References")]
    [SerializeField] private PlayerStatus _status;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _status = GetComponent<PlayerStatus>();
    }
    private void OnEnable()
    {
        _status.OnHit += PlayHitSound;
    }
    private void OnDisable()
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
