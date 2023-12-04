using UnityEngine;
using System.Collections;

public class AtaqueBolaMorte : MonoBehaviour
{
    public int danoDaBola = 20;
    public float velocidade = 5f;
    public float tempoVida = 4f; // Tempo de vida em segundos
    public float cooldown = 4f; // Cooldown em segundos
    private Transform jogador;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CausarDanoAoJogador", 1f, 1f);
        StartCoroutine(AutodestruirAposTempoVida());
    }

    void Update()
    {
        if (jogador != null)
        {
            Vector2 direcao = (jogador.position - transform.position).normalized;
            transform.Translate(direcao * velocidade * Time.deltaTime);
        }
    }

    void CausarDanoAoJogador()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Bola da Morte causou dano ao jogador!");

                Playerhealth playerHealth = collider.GetComponent<Playerhealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(danoDaBola);
                    Debug.Log("Jogador sofreu " + danoDaBola + " de dano. Vida restante: " + playerHealth.health);
                }

                Debug.Log("Destruindo a Bola da Morte.");
                Destroy(gameObject);
            }
        }
    }

    // Coroutine para autodestruir após o tempo de vida
    IEnumerator AutodestruirAposTempoVida()
    {
        yield return new WaitForSeconds(tempoVida);
        Destroy(gameObject);
    }
}
