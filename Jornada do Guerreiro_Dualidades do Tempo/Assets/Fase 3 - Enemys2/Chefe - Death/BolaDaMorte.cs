using UnityEngine;

public class BolaDaMorte : MonoBehaviour
{
    public int danoAoPlayer = 3;
    public float velocidade = 5f; // Adicione uma variável de velocidade.
    private Transform jogador; // Referência ao transform do jogador.



    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcule a direção para o jogador no início.
        Vector2 direcao = (jogador.position - transform.position).normalized;

        // Adicione uma força ao Rigidbody2D para lançar a bola na direção do jogador.
        GetComponent<Rigidbody2D>().AddForce(direcao * velocidade, ForceMode2D.Impulse);

        // Desabilite o Rigidbody2D para que a bola pare de se mover.
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("Player does not implement IDamageable interface.");
            }

            // Destrua a bola quando atingir o jogador.
            Destroy(gameObject);
        }
        
    }
}
