using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private Collider2D[] zoneColliders;

    private void Awake()
    {
        zoneColliders = transform.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in zoneColliders)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
