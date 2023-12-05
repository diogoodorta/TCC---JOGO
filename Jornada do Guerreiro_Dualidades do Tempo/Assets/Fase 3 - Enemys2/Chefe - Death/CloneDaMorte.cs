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
        // Define a posi��o inicial do clone
        transform.position = posicaoInicial;
    }

    void Update()
    {
        // Move o clone em dire��o ao jogador
        transform.position = Vector3.MoveTowards(transform.position, posicaoFinal, velocidadeDoClone * Time.deltaTime);
        Debug.Log("Posi��o atual do clone: " + transform.position);

        // Verifica se o clone atingiu a posi��o final
        if (transform.position == posicaoFinal)
        {
            Debug.Log("Clone atingiu a posi��o final");
            // Execute a l�gica de dano ou destrui��o, se necess�rio
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
                damageable.TakeDamage(1); // Ajuste o valor conforme necess�rio
            }

            // Execute outras a��es de colis�o, se necess�rio

            // Destrua o clone ap�s causar dano
            Destroy(gameObject);
        }
    }
}
