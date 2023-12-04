using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private void Update()
    {
        // Movimento da faca.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a faca colidiu com algo.
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            // Causa dano ao objeto que implementa a interface IDamageable.
            damageable.TakeDamage(damage);
        }

        // Destroi a faca após atingir algo.
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        // Destroi a faca se ela sair da tela.
        Destroy(gameObject);
    }
}
