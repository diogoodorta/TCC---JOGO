using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform DetectaChao;
    public float distancia = 3;
    public bool olhandoParaDireita;
    public float velocidade = 4;
    public float velocidadePerseguicao = 7;
    public Playerhealth playerHealth;
    public bool spot = false; //booleana para saber se o jogador esta dentro do campo de vis�o
    public Transform target; //alvo que o inimigo vai perseguir, nesse caso o jogador
    public Transform inicioCP; //inicio do campo de vis�o 
    public Transform fimCP; //final do campo de vis�o 
    public int EnemySpeed = 025;
    public int health = 50;
    public Rigidbody2D rb;
    public float groundCheckRadius = 0.2f;
    public LayerMask ground;

    private bool isDead = false; // Adicione uma vari�vel para rastrear se o inimigo est� morto.
    private Animator animator;


    void Start()
    {
        olhandoParaDireita = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtenha o componente Animator.
    }   

    void Update()
    {
        Patrulha();
        Raycasting();
        Persegue();
        rb.velocity = transform.right * EnemySpeed;

        if (health <= 0 && !isDead)
        {
            isDead = true; // Marque o inimigo como morto.
            animator.SetBool("IsDead", true); // Ative a anima��o de morte.
            rb.velocity = Vector2.zero; // Pare o movimento do inimigo.
            rb.isKinematic = true; // Torne o Rigidbody kinematic para evitar colis�es.

            // Desative a detec��o de colis�es para que o inimigo n�o cause mais dano.
            GetComponent<Collider2D>().enabled = false;

            // Agende a destrui��o do objeto ap�s a dura��o da anima��o de morte.
            float deathAnimationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, deathAnimationDuration);
        }
    }

    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(DetectaChao.position, Vector2.down, distancia);
        if (groundInfo.collider == false)
        {
            if (olhandoParaDireita == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                olhandoParaDireita = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                olhandoParaDireita = true;
            }
        }
    }

    public void Raycasting()
    {
        Debug.DrawLine(inicioCP.position, fimCP.position, Color.green);
        spot = Physics2D.Linecast(inicioCP.position, fimCP.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void Persegue()
    {
        if (spot == true)
        {
            velocidade = velocidadePerseguicao;
        }

        else if (spot == false)
        {
            velocidade = 4;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(1);
        }


    }

    private void FixedUpdate()
    {
        // Verifique se o inimigo est� no ch�o
        bool isGrounded = IsGrounded();

        // Se o inimigo estiver no ch�o, aplique a for�a da gravidade
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Zerar a velocidade vertical
        }
    }

    private bool IsGrounded()
    {
        // Use um c�rculo de colis�o para verificar se o inimigo est� no ch�o
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, groundCheckRadius, ground);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }
    public void ReceberDano(int dano)
    {
        // Reduza a sa�de do inimigo ou adicione l�gica personalizada para lidar com o dano.
        health -= dano;

        // Verifique se o inimigo est� morto e tome as a��es apropriadas, como destru�-lo.
        if (health <= 0)
        {
            // Adicione aqui o c�digo para destruir o inimigo ou executar outras a��es de morte.
            Destroy(gameObject);
        }

        // Verifique se o inimigo est� morto.
        if (!isDead)
        {
            // Reduza a sa�de do inimigo ou adicione l�gica personalizada para lidar com o dano.
            health -= dano;
        }
    }
}