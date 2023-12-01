using System.Collections;
using UnityEngine;

public class Ferrao : MonoBehaviour
{
    private Rigidbody2D rb;
    public float projectileSpeed;
    private bool hasCollided = false; // Flag para verificar se o ferr�o j� colidiu com algo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Encontrar o jogador pela tag
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Debug.LogWarning("Jogador n�o encontrado. Certifique-se de ter uma tag 'Player' no objeto do jogador.");
        }
    }

    void Update()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided) // Verifica se o ferr�o ainda n�o colidiu com nada
        {
            Debug.Log("Colis�o com: " + collision.gameObject.name);

            // Verificar se o objeto de colis�o implementa a interface IDamageable
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                // Se colidir com o jogador, cause dano e destrua o ferr�o
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(1);
                Destroy(gameObject);
            }
            else
            {
                // Se colidir com qualquer outro objeto (por exemplo, o ch�o), destrua o ferr�o
                Destroy(gameObject);
            }

            hasCollided = true; // Atualiza a flag para indicar que o ferr�o colidiu com algo
        }
    }
}
