using System.Collections;
using UnityEngine;

public class BombadoController : MonoBehaviour
{
    public int maxHealth = 40;
    public float moveSpeed = 2f;
    public float kickCooldown = 3f;
    public float detectionRange = 5f;
    public float kickRange = 1.5f;

    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;
    private bool canKick = true;
    private Transform player;
    private Collider2D bombadoCollider;
    public Transform kickPoint;  // Ponto de origem do chute
    public LayerMask playerLayer;  // Camada do jogador

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bombadoCollider = GetComponent<Collider2D>();
        bombadoCollider.isTrigger = false; // ou true, dependendo da sua necessidade
        rb.isKinematic = false; // ou true, dependendo da sua necessidade
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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return canKick && distanceToPlayer < detectionRange && distanceToPlayer < kickRange;
    }

    private void Kick()
    {
        Debug.Log("Kick method called");

        animator.SetTrigger("Kick");

        // Use Physics2D.OverlapCircle para detectar colisões com o jogador
        Collider2D hitPlayer = Physics2D.OverlapCircle(kickPoint.position, kickRange, playerLayer);

        if (hitPlayer != null)
        {
            PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamageKick(2);
            }
            else
            {
                Debug.LogWarning("O objeto atingido não possui o script PlayerHealth.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum jogador detectado dentro do alcance do chute.");
        }
    }


    private IEnumerator KickCooldown()
    {
        canKick = false;

        yield return new WaitForSeconds(kickCooldown);

        canKick = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player");
        }
    }
}
