using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    public int health = 100;
    public float speed = 3f;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;

    private Transform player;
    private Animator animator;
    private bool isDead = false;
    private bool canAttack = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null && !isDead)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Move(direction);

            if (direction != Vector2.zero)
            {
                Flip(direction.x);
            }

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (CanAttack() && distanceToPlayer < attackRange)
            {
                StartCoroutine(AttackCooldown());
                Attack();
            }
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    private void Flip(float directionX)
    {
        if ((directionX > 0 && transform.localScale.x < 0) || (directionX < 0 && transform.localScale.x > 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private bool CanAttack()
    {
        return canAttack;
    }

    private void Attack()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamageZombie(damage);
        }
        else
        {
            Debug.LogWarning("O jogador não possui o script PlayerHealth.");
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
            health -= amount;
            Debug.Log("Zumbi atingido! Vida do Zumbi: " + health);

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("morte");

        // Desativa os colisores
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // Congela a posição
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        Invoke("DeactivateGameObject", 5f);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
