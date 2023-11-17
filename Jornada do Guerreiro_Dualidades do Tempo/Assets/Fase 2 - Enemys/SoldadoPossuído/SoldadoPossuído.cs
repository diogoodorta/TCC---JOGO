using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float vida = 20; // Defina o valor máximo de vida do inimigo
    public int danoDaEspada = 1;
    public float velocidadeMovimento = 5.0f;
    public float alcanceDeDetecção = 10.0f;
    public float alcanceDoAtaque = 1.5f;
    public float tempoEntreAtaques = 2.0f; // Tempo em segundos entre os ataques
    public LayerMask jogadorLayer; // Layer do jogador
    public Transform espadaTransform; // Transform da espada

    private float tempoUltimoAtaque = -10.0f; // Inicialize com um valor negativo para evitar um ataque imediato
    private bool estaMorto = false;
    private float cooldownMorte = 2f;  // Cooldown para destruir o objeto após a animação de morte
    private float cooldownMorteTimer = 0f;
    private Transform jogador;
    private Animator animator;
    private bool podeMover = true; // Adicione esta linha para controlar se o inimigo pode se mover

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        if (jogador != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("O jogador não foi encontrado. Verifique se ele tem a tag correta.");
            // Lide com a situação de o jogador não ser encontrado, se necessário.
        }
    }

    void Update()
    {
        if (!estaMorto && podeMover)
        {
            float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

            if (distanciaParaJogador <= alcanceDeDetecção)
            {
                Vector3 direcao = (jogador.position - transform.position).normalized;
                transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);

                if (distanciaParaJogador <= alcanceDoAtaque)
                {
                    AtacarJogador();
                }
            }

            cooldownMorteTimer -= Time.deltaTime;

            if (cooldownMorteTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void AtacarJogador()
    {
        if (Time.time - tempoUltimoAtaque >= tempoEntreAtaques)
        {
            animator.SetTrigger("Atacar");

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(espadaTransform.position, alcanceDoAtaque, jogadorLayer);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.layer == jogadorLayer)
                {
                    Playerhealth playerHealth = collider.GetComponent<Playerhealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(danoDaEspada);
                    }
                }
            }

            tempoUltimoAtaque = Time.time; // Registre o tempo do último ataque
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            estaMorto = true;
            podeMover = false; // Impede o inimigo de se mover temporariamente
            animator.SetBool("Morto", true);
            cooldownMorteTimer = cooldownMorte;
        }
    }
}
