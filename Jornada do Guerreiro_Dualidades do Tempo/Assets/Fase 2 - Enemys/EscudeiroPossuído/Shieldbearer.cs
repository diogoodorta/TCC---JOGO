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
        }
        else
        {
            jogadorNaVisao = false;
        }

        if (jogadorNaVisao)
        {
            // Perseguir o jogador
            Vector3 direcao = (Player.position - transform.position).normalized;
            transform.Translate(direcao * velocidadePerseguicao * Time.deltaTime);
        }
        else
        {
            // Movimento padr�o
            MovimentoPadraoShieldbearer();
        }
    }

    private void MovimentoPadrao()
    {
        // Implemente o movimento padr�o do Shieldbearer aqui
        // Por exemplo, movimento de ida e volta entre dois pontos.
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
            }
            else
            {
                // L�gica de empurr�o
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                Vector2 direcaoEmpurrao = -(jogador.position - transform.position).normalized;
                rb.AddForce(direcaoEmpurrao * empurraoForca, ForceMode2D.Impulse);
            }
        }
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

    private void MovimentoPadraoShieldbearer()
    {
        // Movimento padr�o ou movimento de ida e volta entre dois pontos
        if (indoParaPontoDeVolta)
        {
            // Move em dire��o ao ponto de volta
            Vector3 direcaoVolta = (pontoDeVolta.position - transform.position).normalized;
            transform.Translate(direcaoVolta * velocidadePadrao * Time.deltaTime);

            // Verifica se chegou ao ponto de volta
            if (Vector2.Distance(transform.position, pontoDeVolta.position) < 0.1f)
            {
                indoParaPontoDeVolta = false;
            }
        }
        else
        {
            // Move em dire��o ao ponto de ida
            Vector3 direcaoIda = (pontoDeIda.position - transform.position).normalized;
            transform.Translate(direcaoIda * velocidadePadrao * Time.deltaTime);

            // Verifica se chegou ao ponto de ida
            if (Vector2.Distance(transform.position, pontoDeIda.position) < 0.1f)
            {
                indoParaPontoDeVolta = true;
            }
        }
    }

    private void DestruirInimigo()
    {
        // Destrua o objeto do inimigo.
        Destroy(gameObject);
    }
}

   
    

