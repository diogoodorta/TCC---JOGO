using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldbearer : MonoBehaviour
{
    public float velocidadePadrao = 2.0f;
    public float velocidadePerseguicao = 4.0f;
    public float empurraoForca = 10.0f;
    public float alcanceDeVisao = 5.0f;
    public float distanciaDeIda = 5.0f; // Dist�ncia entre os pontos de ida e volta
    public PlayerHealth playerHealth;
    public Transform pontoDeEmpurrao; // Adicione esta linha para referenciar a Empty

    public int vida = 3;
    public float atrasoAntesDeDestruir = 2.0f;
    public Animator animador; // Refer�ncia ao componente Animator
    public Transform jogador;
    public Transform pontoDeIda;
    public Transform pontoDeVolta;
    public Transform Player;

    private bool jogadorNaVisao = false;
    private bool derrotado = false;
    private bool indoParaPontoDeVolta = false; // Adicione esta linha para declarar a vari�vel
    private bool podeAndar = true; // Adicione esta linha para controlar se o inimigo pode andar

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanciaParaJogador = Vector2.Distance(transform.position, Player.position);

        if (distanciaParaJogador <= alcanceDeVisao)
        {
            jogadorNaVisao = true;

            // Adicione a lógica de flip aqui
            FlipSprite(Player.position.x - transform.position.x);
        }
        else
        {
            jogadorNaVisao = false;
        }

        if (podeAndar)
        {
            animador.SetTrigger("andar");

            if (jogadorNaVisao)
            {
                // Perseguir o jogador
                Vector3 direcao = (Player.position - transform.position).normalized;
                transform.Translate(direcao * velocidadePerseguicao * Time.deltaTime);

                // Adicione a lógica de flip aqui também
                FlipSprite(direcao.x);
            }
            else
            {
                // Movimento padrão
                MovimentoPadraoShieldbearer();
            }
        }
    }

    private void MovimentoPadraoShieldbearer()
    {
        // Movimento padrão ou movimento de ida e volta entre dois pontos
        if (indoParaPontoDeVolta)
        {
            // Move em direção ao ponto de volta
            Vector3 direcaoVolta = (pontoDeVolta.position - transform.position).normalized;
            transform.Translate(direcaoVolta * velocidadePadrao * Time.deltaTime);

            // Verifica se chegou ao ponto de volta e flipa se necessário
            if (Vector2.Distance(transform.position, pontoDeVolta.position) < 0.1f)
            {
                indoParaPontoDeVolta = false;
                FlipSprite(pontoDeIda.position.x - pontoDeVolta.position.x);
            }
        }
        else
        {
            // Move em direção ao ponto de ida
            Vector3 direcaoIda = (pontoDeIda.position - transform.position).normalized;
            transform.Translate(direcaoIda * velocidadePadrao * Time.deltaTime);

            // Verifica se chegou ao ponto de ida e flipa se necessário
            if (Vector2.Distance(transform.position, pontoDeIda.position) < 0.1f)
            {
                indoParaPontoDeVolta = true;
                FlipSprite(pontoDeVolta.position.x - pontoDeIda.position.x);
            }
        }
    }

    public void SofrerDano(bool nasCostas)
    {
        if (!derrotado)
        {
            if (nasCostas)
            {
                vida--;
                if (vida <= 0)
                {
                    Derrotar();
                }
                else
                {
                    // Inicie a animação de dano
                    animador.SetTrigger("Dano");

                    // Impede o inimigo de andar temporariamente
                    StartCoroutine(PararAndarTemporariamente());
                }
            }
            else
            {
                // Lógica de empurrão
                Vector2 direcaoEmpurrao = ((Vector2)Player.position - (Vector2)transform.position).normalized;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero; // Pare o movimento atual do inimigo

                // Ajuste da posição alvo usando a Empty
                Vector2 pontoAlvo = pontoDeEmpurrao.position;
                Vector2 posicaoAtual = rb.position;

                // Adicione uma força em direção à posição alvo
                rb.AddForce((pontoAlvo - posicaoAtual).normalized * empurraoForca, ForceMode2D.Impulse);

                // Inicie a animação de empurrão
                animador.SetTrigger("Empurrao");

                // Impede o inimigo de andar temporariamente
                StartCoroutine(PararAndarTemporariamente());
            }
        }
    }

    IEnumerator PararAndarTemporariamente()
    {
        podeAndar = false;
        yield return new WaitForSeconds(0.5f); // Aguarde 0.5 segundos
        podeAndar = true;
    }

    private void Derrotar()
    {
        if (!derrotado)
        {
            //Desative o Collider para que o inimigo n�o possa mais ser atingido.
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Reproduza a anima��o de morte, se houver um Animator.
            if (animador != null)
            {
                animador.SetTrigger("Morte");
            }

            derrotado = true;

            // Agende a destrui��o do objeto do inimigo ap�s um atraso.
            Invoke("DestruirInimigo", atrasoAntesDeDestruir);
        }
    }

    private void FlipSprite(float directionX)
    {
        if ((directionX > 0 && transform.localScale.x < 0) || (directionX < 0 && transform.localScale.x > 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void DestruirInimigo()
    {
        // Destrua o objeto do inimigo.
        Destroy(gameObject);
    }
}