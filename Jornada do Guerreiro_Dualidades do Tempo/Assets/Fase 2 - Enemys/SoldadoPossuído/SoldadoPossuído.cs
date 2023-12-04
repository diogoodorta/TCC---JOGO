using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoPossuido : MonoBehaviour, IDamageable
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
        jogador = GameObject.FindWithTag("Player")?.transform;

        if (jogador != null)
        {
            animator = GetComponent<Animator>();
            jogadorLayer = LayerMask.GetMask("Player"); // Defina o LayerMask aqui
        }
        else
        {
            Debug.LogError("O jogador não foi encontrado. Verifique se ele tem a tag correta.");
            // Lide com a situação de o jogador não ser encontrado, se necessário.
        }
    }

    void Update()
    {
        if (!estaMorto && podeMover && jogador != null)
        {
            float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

            if (distanciaParaJogador <= alcanceDeDetecção)
            {
                Vector3 direcao = (jogador.position - transform.position).normalized;
                transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);
                animator.SetTrigger("andar");

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
                    EncerrarAtividades();
                }
            }
        }
    }

    void EncerrarAtividades()
    {
        // Desativar controles de movimento (se houver)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Desligar colisores
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Iniciar animação de morte (se houver um componente Animator)
        if (animator != null)
        {
            animator.SetTrigger("morte");
        }

        // Desativar outros scripts (adicione esta linha se você tiver outros scripts)
        // Exemplo: Componente de movimento
        // move movimentoScript = GetComponent<move>();
        // if (movimentoScript != null)
        // {
        //     movimentoScript.enabled = false;
        // }

        // Iniciar a rotina de cooldown
        StartCoroutine(IniciarCooldown());
    }

    IEnumerator IniciarCooldown()
    {
        yield return new WaitForSeconds(4f); // Tempo de cooldown

        // Autodestruir o GameObject
        Destroy(gameObject);
    }

    void AtacarJogador()
    {
        if (Time.time >= tempoUltimoAtaque + tempoEntreAtaques)
        {
            animator.SetTrigger("ataque");  // Inicie a animação aqui

            Debug.Log("Ataque do SoldadoPossuído iniciado");

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(espadaTransform.position, alcanceDoAtaque, jogadorLayer);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("Ataque acertou o jogador");
                    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamageEspada(danoDaEspada); // Corrija esta linha
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

    public void ReceberDano(int amount)
    {
        vida -= amount;
        VerificarVida();
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        VerificarVida();
    }

    private void VerificarVida()
    {
        if (vida <= 0 && !estaMorto)
        {
            estaMorto = true;
            podeMover = false;
            animator.SetBool("morrer", true);
            cooldownMorteTimer = cooldownMorte;
            EncerrarAtividades();
        }
    }
}
