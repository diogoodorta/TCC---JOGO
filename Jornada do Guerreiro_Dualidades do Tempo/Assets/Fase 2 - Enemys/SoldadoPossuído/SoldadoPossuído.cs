using System.Collections;
using UnityEngine;

public class SoldadoPossuido : MonoBehaviour
{

    public float vidaMaxima = 10;
    public float vida = 10;
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
    private Transform alvo;

    private SoldadoPossuido Soldade;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player")?.transform;

        // Adicione as seguintes linhas para encontrar o componente Aliado
        GameObject aliadoObject = GameObject.FindWithTag("Enemy");
        if (aliadoObject != null)
        {
            Soldade = aliadoObject.GetComponent<SoldadoPossuido>();
        }

        if (jogador != null)
        {
            animator = GetComponent<Animator>();
            jogadorLayer = LayerMask.GetMask("Player");
        }
        else
        {
            Debug.LogError("O jogador não foi encontrado. Verifique se ele tem a tag correta.");
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

                // Atualiza a escala para virar o inimigo na direção do jogador
                if (direcao.x > 0)
                    transform.localScale = new Vector3(1, 1, 1); // Virado para a direita
                else if (direcao.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1); // Virado para a esquerda

                transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);
                animator.SetTrigger("andando");

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
        animator.SetTrigger("morte");

        // Desativar outros scripts (adicione esta linha se você tiver outros scripts)
        // Exemplo: Componente de movimento
        // move movimentoScript = GetComponent<move>();
        // if (movimentoScript != null)
        // {
        //     movimentoScript.enabled = false;
        // }

        // Iniciar a rotina de cooldown
        StartCoroutine(IniciarCooldownMorte());
    }


    private void VerificarVida()
    {
        if (vida <= 0 && !estaMorto)
        {
            estaMorto = true;
            podeMover = false;
            animator.SetBool("Morte", true);

            // Iniciar a rotina de cooldown para destruição após a animação de morte
            StartCoroutine(IniciarCooldownMorteCoroutine());
        }
    }


    IEnumerator IniciarCooldownMorteCoroutine()
    {
        // Aguarde o término da animação de morte antes de iniciar o cooldown
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Autodestruir o GameObject após a animação de morte
        Destroy(gameObject);
    }

    void AtacarJogador()
    {
        if (Time.time >= tempoUltimoAtaque + tempoEntreAtaques)
        {
            animator.SetTrigger("Atacar");

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
                else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    SoldadoPossuido soldado = collider.GetComponent<SoldadoPossuido>();
                    if (soldado != null && soldado.vida < 10)
                    {
                        alvo = soldado.transform;
                        Debug.Log("Clérigo detectou SoldadoPossuído em perigo!");
                        Debug.Log("Vida do SoldadoPossuído: " + soldado.vida);

                        // Cura o SoldadoPossuído
                        Soldade.ReceberCura(4); // Ajuste a quantidade de cura conforme necessário
                        break;
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

    public void ReceberCura(float quantidadeDeCura)
    {
        vida = Mathf.Min(vida + quantidadeDeCura, vidaMaxima); // Garante que a vida não ultrapasse o máximo
        // Adicione outras lógicas relacionadas à cura, se necessário.
    }

    IEnumerator IniciarCooldownMorte()
    {
        // Aguarde o término da animação de morte antes de iniciar o cooldown
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Autodestruir o GameObject após a animação de morte
        Destroy(gameObject);
    }


}
