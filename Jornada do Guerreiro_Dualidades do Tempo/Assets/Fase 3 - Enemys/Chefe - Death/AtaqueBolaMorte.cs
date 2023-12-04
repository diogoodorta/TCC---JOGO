using UnityEngine;

public class AtaqueBolaMorte : MonoBehaviour
{
    public int danoDaBola = 20;
    public float velocidade = 5f; // Ajuste conforme necessário
    private Transform jogador;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CausarDanoAoJogador", 1f, 1f); // Chama a função a cada segundo (ajuste conforme necessário)
    }

    void Update()
    {
        // Garante que o jogador ainda existe (não foi destruído)
        if (jogador != null)
        {
            // Move a bola em direção ao jogador
            transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);
        }
        else
        {
            // Se o jogador foi destruído, destrua a bola também
            Destroy(gameObject);
        }
    }

    void CausarDanoAoJogador()
    {
        if (jogador != null && Vector2.Distance(transform.position, jogador.position) < 1.5f) // Ajuste conforme necessário
        {
            Debug.Log("Bola da Morte causou dano contínuo ao jogador!");

            // Chame o método TakeDamage em vez de LevarDano
            IDamable damable = jogador.GetComponent<IDamable>();
            if (damable != null)
            {
                damable.TakeDamage(danoDaBola);
                Debug.Log("Jogador sofreu " + danoDaBola + " de dano contínuo. Vida restante: " + (damable as Playerhealth).health);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bola da Morte atingiu o jogador!");

            // Chame o método TakeDamage em vez de LevarDano
            IDamable damable = other.GetComponent<IDamable>();
            if (damable != null)
            {
                damable.TakeDamage(danoDaBola);
                Debug.Log("Jogador sofreu " + danoDaBola + " de dano. Vida restante: " + (damable as Playerhealth).health);
            }
            else
            {
                Debug.Log("Componente IDamable não encontrado no jogador.");
            }

            // Destruir a bola após causar dano
            Debug.Log("Destruindo a Bola da Morte.");
            Destroy(gameObject);
        }
    }
}