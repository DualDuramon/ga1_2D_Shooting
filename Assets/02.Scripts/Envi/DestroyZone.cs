using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private Collider2D[] _zoneColliders;

    private void Awake()
    {
        _zoneColliders = transform.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in _zoneColliders)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
