using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStatus _status;

    [Header("Particle References")]
    [SerializeField] private GameObject _deathEffect;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
    }
    private void OnEnable()
    {
        _status.OnDeath += SpawnDeathParticle;
    }

    private void OnDisable()
    {
        _status.OnDeath -= SpawnDeathParticle;
    }

    private void SpawnDeathParticle()
    {
        if (_status == null)
        {
            Debug.LogWarning($"{gameObject.name} : Can't Generate DeathParticle. Need PlayerStatus Component");
            return;
        }
        if (_deathEffect == null)
        {
            Debug.LogWarning($"{gameObject.name} : Can't Generate DeathParticle. Need particle prefab");
            return;
        }
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
}
