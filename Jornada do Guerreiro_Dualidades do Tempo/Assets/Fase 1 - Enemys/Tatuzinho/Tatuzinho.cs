using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatuzinho : MonoBehaviour, IDamageable
{
    public Transform DetectaChao;
    public float distancia = 3;
    public bool olhandoParaDireita;
    public float velocidade = 4;
    public float velocidadePerseguicao = 7;
    public bool spot = false;
    public Transform inicioCP;
    public Transform fimCP;
    public int EnemySpeed = 025;
    public Rigidbody2D rb;
    public float groundCheckRadius = 0.2f;
    public LayerMask ground;

    private Animator animator;

    void Start()
    {
        olhandoParaDireita = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Patrulha();
        Raycasting();
        Persegue();
        rb.velocity = transform.right * EnemySpeed;
    }

    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(DetectaChao.position, Vector2.down, distancia);
        if (!groundInfo.collider)
        {
            if (olhandoParaDireita)
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
        if (spot)
        {
            velocidade = velocidadePerseguicao;
        }
        else
        {
            velocidade = 4;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("Player does not implement IDamageable interface.");
            }
        }
    }

    // Implementação da interface IDamageable
    public void TakeDamage(int amount)
    {
        // Lógica para o Tatuzinho receber dano vai aqui
        // Por exemplo: EnemyHealth -= amount;
        Debug.Log("Tatuzinho recebeu dano: " + amount);
    }

    private void FixedUpdate()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private bool IsGrounded()
    {
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
}
