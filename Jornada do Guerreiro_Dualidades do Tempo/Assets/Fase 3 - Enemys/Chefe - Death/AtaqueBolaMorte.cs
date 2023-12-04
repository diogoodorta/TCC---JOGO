using UnityEngine;

public class AtaqueBolaMorte : MonoBehaviour
{
    public int danoDaBola = 20;
    public float velocidade = 5f; // Ajuste conforme necess�rio
    private Transform jogador;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CausarDanoAoJogador", 1f, 1f); // Chama a fun��o a cada segundo (ajuste conforme necess�rio)
    }

    void Update()
    {
        // Garante que o jogador ainda existe (n�o foi destru�do)
        if (jogador != null)
        {
            // Move a bola em dire��o ao jogador
            transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);
        }
        else
        {
            // Se o jogador foi destru�do, destrua a bola tamb�m
            Destroy(gameObject);
        }
    }

    void CausarDanoAoJogador()
    {
        if (jogador != null && Vector2.Distance(transform.position, jogador.position) < 1.5f) // Ajuste conforme necess�rio
        {
            Debug.Log("Bola da Morte causou dano cont�nuo ao jogador!");

            // Chame o m�todo TakeDamage em vez de LevarDano
            IDamable damable = jogador.GetComponent<IDamable>();
            if (damable != null)
            {
                damable.TakeDamage(danoDaBola);
                Debug.Log("Jogador sofreu " + danoDaBola + " de dano cont�nuo. Vida restante: " + (damable as Playerhealth).health);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bola da Morte atingiu o jogador!");

            // Chame o m�todo TakeDamage em vez de LevarDano
            IDamable damable = other.GetComponent<IDamable>();
            if (damable != null)
            {
                damable.TakeDamage(danoDaBola);
                Debug.Log("Jogador sofreu " + danoDaBola + " de dano. Vida restante: " + (damable as Playerhealth).health);
            }
            else
            {
                Debug.Log("Componente IDamable n�o encontrado no jogador.");
            }

            // Destruir a bola ap�s causar dano
            Debug.Log("Destruindo a Bola da Morte.");
            Destroy(gameObject);
        }
    }
}