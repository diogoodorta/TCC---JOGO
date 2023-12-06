using System.Collections;
using UnityEngine;

    public class Ferrao : MonoBehaviour
{
    private Rigidbody2D rb;
    public float projectileSpeed;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado. Certifique-se de ter uma tag 'Player' no objeto do jogador.");
        }
    }

    void Update()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            Debug.Log("Colisão com: " + collision.gameObject.name);

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                IDamageable playerDamageable = collision.gameObject.GetComponent<IDamageable>();

                if (playerDamageable != null)
                {
                    playerDamageable.TakeDamage(1);
                }

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            hasCollided = true;
        }
    }
}

