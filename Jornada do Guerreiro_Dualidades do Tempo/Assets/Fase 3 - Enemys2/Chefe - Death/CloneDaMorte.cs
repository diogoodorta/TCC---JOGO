using UnityEngine;

public class CloneDaMorte : MonoBehaviour
{
    private Vector3 posicaoInicial;
    private Vector3 posicaoFinal;
    public float velocidadeDoClone = 5f;

    public void Inicializar(Vector3 posicaoInicial, Vector3 posicaoFinal)
    {
        this.posicaoInicial = posicaoInicial;
        this.posicaoFinal = posicaoFinal;
    }

    void Start()
    {
        // Define a posição inicial do clone
        transform.position = posicaoInicial;
    }

    void Update()
    {
        // Move o clone em direção ao jogador
        transform.position = Vector3.MoveTowards(transform.position, posicaoFinal, velocidadeDoClone * Time.deltaTime);
        Debug.Log("Posição atual do clone: " + transform.position);

        // Verifica se o clone atingiu a posição final
        if (transform.position == posicaoFinal)
        {
            Debug.Log("Clone atingiu a posição final");
            // Execute a lógica de dano ou destruição, se necessário
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se colidiu com o jogador
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1); // Ajuste o valor conforme necessário
            }

            // Execute outras ações de colisão, se necessário

            // Destrua o clone após causar dano
            Destroy(gameObject);
        }
    }
}
