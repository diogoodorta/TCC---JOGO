using System.Collections;
using UnityEngine;

public class BombadoController : MonoBehaviour
{
    public int maxHealth = 200;
    public float moveSpeed = 2f;
    public float kickCooldown = 3f;
    public float detectionRange = 5f;
    public float kickRange = 1.5f;

    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool canKick = true;
    private bool isCooldownActive = false;
    private Transform player;

    public Transform kickPoint;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (CanKick() && distanceToPlayer < detectionRange)
            {
                StartCoroutine(KickCooldown());

                if (distanceToPlayer < kickRange)
                {
                    Kick();
                }
            }

            FlipSprite();
        }
    }

    private void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        animator.SetBool("isWalking", true);
    }

    private void FlipSprite()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private bool CanKick()
    {
        return canKick;
    }

    private void Kick()
    {
        animator.SetTrigger("Kick");

        // Encontrar o componente PlayerHealth no cenário
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamageKick(2);
        }
        else
        {
            Debug.LogWarning("O jogador não possui o script PlayerHealth.");
        }
    }

    private IEnumerator KickCooldown()
    {
        canKick = false;

        yield return new WaitForSeconds(kickCooldown);

        canKick = true;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        StartCoroutine(DestroyGameObjectAfterDelay());
    }

    private IEnumerator DestroyGameObjectAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void PararTodasAcoes()
    {
        // Implemente a lógica para parar todas as ações do jogador aqui
    }
}
