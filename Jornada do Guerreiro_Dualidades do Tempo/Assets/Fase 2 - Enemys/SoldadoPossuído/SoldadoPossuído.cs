using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float velocidadeMovimento = 5.0f;
    public float alcanceDeDetecção = 10.0f;
    public int vida = 3;
    public Playerhealth playerHealth;
    public int danoDaEspada = 1;
    public Transform espadaTransform; // Transform da espada
    public float alcanceDoAtaque = 1.5f;
    public LayerMask jogadorLayer; // Layer do jogador
    public float tempoEntreAtaques = 2.0f; // Tempo em segundos entre os ataques

    private float tempoUltimoAtaque = 0.0f; // Mantém o registro do último ataque
    private Transform jogador;
    private Animator animator;
    private bool estaMorto = false;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform; // Encontre o jogador pelo nome da tag
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!estaMorto)
        {
            float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

            if (distanciaParaJogador <= alcanceDeDetecção)
            {
                // O jogador está no alcance de detecção, siga o jogador
                Vector3 direcao = (jogador.position - transform.position).normalized;
                transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);

                // Verifique se o jogador está no alcance de ataque
                if (distanciaParaJogador <= alcanceDoAtaque)
                {
                    AtacarJogador();
                }
            }
        }
    }

    void AtacarJogador()
    {
        // Verifique o cooldown do ataque
        if (Time.time - tempoUltimoAtaque >= tempoEntreAtaques)
        {
            // Inicie a animação de ataque
            animator.SetTrigger("Atacar");

            // Verifique se o jogador está à frente do inimigo
            if (Vector2.Dot(espadaTransform.right, jogador.position - espadaTransform.position) > 0)
            {
                // O jogador está à frente do inimigo, aplique dano ao jogador
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(espadaTransform.position, alcanceDoAtaque, jogadorLayer);

                foreach (Collider2D collider in hitColliders)
                {
                    Playerhealth playerHealth = collider.GetComponent<Playerhealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(danoDaEspada);
                    }
                }
            }

            // Defina o tempo do último ataque para o momento atual
            tempoUltimoAtaque = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            estaMorto = true;
            animator.SetBool("Morto", true);
            // Adicione qualquer lógica adicional de morte aqui
        }
    }
}
