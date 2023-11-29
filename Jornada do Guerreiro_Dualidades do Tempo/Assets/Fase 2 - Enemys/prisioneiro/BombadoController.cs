using System.Collections;
using UnityEngine;

public class BombadoController : MonoBehaviour, IDamageable
{
    public int maxHealth = 200;
    public float moveSpeed = 2f;
    public float kickCooldown = 3f;

    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool canKick = true;

    public Transform kickPoint;  // Posi��o do ponto de ataque (p�)

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();

            if (CanKick())
            {
                StartCoroutine(KickCooldown());
                Kick();
            }
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", true);
    }

    private bool CanKick()
    {
        return canKick;
    }

    private void Kick()
    {
        // Ativa a anima��o de chut�o
        animator.SetTrigger("Kick");
    }

    // Implementa��o do m�todo da interface IDamageable
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator KickCooldown()
    {
        // Define a vari�vel de cooldown para false
        canKick = false;

        // Aguarda o tempo de cooldown
        yield return new WaitForSeconds(kickCooldown);

        // Reset a vari�vel de cooldown para true
        canKick = true;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");


        // Desativa o GameObject ap�s 5 segundos (ajuste conforme necess�rio)
        StartCoroutine(DestroyGameObjectAfterDelay());
    }

    private IEnumerator DestroyGameObjectAfterDelay()
    {
        // Aguarde um tempo antes de destruir o GameObject (opcional)
        yield return new WaitForSeconds(5f);

        // Destrua o GameObject
        Destroy(gameObject);
    }
}
