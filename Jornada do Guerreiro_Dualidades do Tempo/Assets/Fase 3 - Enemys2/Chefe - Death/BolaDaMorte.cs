using UnityEngine;

public class BolaDaMorte : MonoBehaviour
{
    public int danoAoPlayer = 3;
    public float velocidade = 5f; // Adicione uma vari�vel de velocidade.
    private Transform jogador; // Refer�ncia ao transform do jogador.



    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcule a dire��o para o jogador no in�cio.
        Vector2 direcao = (jogador.position - transform.position).normalized;

        // Adicione uma for�a ao Rigidbody2D para lan�ar a bola na dire��o do jogador.
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
