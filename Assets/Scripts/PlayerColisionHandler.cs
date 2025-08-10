using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerScore score;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            score.AddScore(10);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = GetComponent<IDamageable>();
            damageable?.TakeDamage(10f);
            Destroy(collision.gameObject);
        }
    }
}