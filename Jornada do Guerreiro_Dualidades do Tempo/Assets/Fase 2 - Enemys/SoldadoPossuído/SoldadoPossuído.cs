using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float vida = 20;
    public int danoDaEspada = 1;
    public float velocidadeMovimento = 5.0f;
    public float alcanceDeDetecção = 10.0f;
    public float alcanceDoAtaque = 1.5f;
    public float tempoEntreAtaques = 2.0f;
    public LayerMask jogadorLayer;
    public Transform espadaTransform;

    private float tempoUltimoAtaque = -10.0f;
    private bool estaMorto = false;
    private float cooldownMorte = 2f;
    private float cooldownMorteTimer = 0f;
    private Transform jogador;

    private Animator animator;
    private bool podeMover = true;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
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

            if (estaMorto)
            {
                cooldownMorteTimer -= Time.deltaTime;

                if (cooldownMorteTimer <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void AtacarJogador()
    {
        if (Time.time - tempoUltimoAtaque >= tempoEntreAtaques)
        {
            animator.SetTrigger("Atacar");

            Debug.Log("Ataque do inimigo iniciado");

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(espadaTransform.position, alcanceDoAtaque, jogadorLayer);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.gameObject.layer == jogadorLayer)
                {
                    Debug.Log("Ataque acertou o jogador");
                    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(danoDaEspada);
                        Debug.Log("Dano causado ao jogador");
                    }
                    else
                    {
                        Debug.LogWarning("O componente Playerhealth não foi encontrado no jogador.");
                    }
                }
            }

            tempoUltimoAtaque = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            estaMorto = true;
            podeMover = false;
            animator.SetBool("Morto", true);
            cooldownMorteTimer = cooldownMorte;
        }
    }
}
