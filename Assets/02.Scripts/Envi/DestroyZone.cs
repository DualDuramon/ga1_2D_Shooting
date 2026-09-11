using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private Collider2D[] _zoneColliders;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(collision.gameObject);
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
