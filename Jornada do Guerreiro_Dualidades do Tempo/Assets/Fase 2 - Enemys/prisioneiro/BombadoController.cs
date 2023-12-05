using System.Collections;
using UnityEngine;

public class BombadoController : MonoBehaviour
{
    public int maxHealth = 40;
    public float moveSpeed = 2f;
    public float kickCooldown = 3f;
    public float detectionRange = 5f;
    public float kickRange = 1.5f;
    public float attackDamage = 5f;
    public float attackCooldown = 2f;

    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool canKick = true;
    private bool canAttack = true;
    private Transform player;
    private Collider2D bombadoCollider;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private PlayerHealth playerHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bombadoCollider = GetComponent<Collider2D>();
        bombadoCollider.isTrigger = false; // ou true, dependendo da sua necessidade
        rb.isKinematic = false; // ou true, dependendo da sua necessidade

        // Encontrar o componente PlayerHealth no cenário
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogWarning("O jogador não possui o script PlayerHealth.");
        }
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

            if (CanAttack() && distanceToPlayer < detectionRange)
            {
                StartCoroutine(AttackCooldown());

                if (distanceToPlayer < detectionRange)
                {
                    Attack();
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

    private bool CanAttack()
    {
        return canAttack;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        // Detectar o jogador na área de ataque
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, detectionRange, LayerMask.GetMask("Player"));

        // Aplicar dano ao jogador encontrado
        foreach (Collider2D playerCollider in hitPlayers)
        {
            if (playerCollider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamageKick(2);
                    Debug.Log("Dano causado ao jogador");
                }
                else
                {
                    Debug.LogWarning("O componente PlayerHealth não foi encontrado no jogador.");
                }
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public void TakeDamage(int amount)
    {
        if (!isDead)
        {
            currentHealth -= amount;
            Debug.Log("Bombado atingido! Vida do Bombado: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
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
