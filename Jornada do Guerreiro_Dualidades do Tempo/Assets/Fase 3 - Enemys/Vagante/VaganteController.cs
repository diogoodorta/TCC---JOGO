using UnityEngine;

public class VaganteController : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 100;
    public int knifeDamage = 10;
    public Transform pointA;
    public Transform pointB;

    private int currentHealth;
    private Transform player;
    private Animator animator;
    private bool isDead = false;

    public Transform knifeSpawnPoint;
    public GameObject knifePrefab;
    public float knifeCooldown = 5f;
    private float knifeTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            if (knifeTimer <= 0f)
            {
                ThrowKnife();
                knifeTimer = knifeCooldown;
            }
            else
            {
                knifeTimer -= Time.deltaTime;
            }

            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (distanceToPlayer < 5f)
                {
                    StopMovingAndThrow();
                }
                else
                {
                    MoveBetweenPoints();
                }
            }
        }
    }

    private void MoveBetweenPoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            pointB.position = new Vector2(pointA.position.x, pointB.position.y);
        }

        animator.SetBool("isWalking", true);
    }

    private void StopMovingAndThrow()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isThrowing", true);
        transform.LookAt(player.position);
    }

    private void ThrowKnife()
    {
        Instantiate(knifePrefab, knifeSpawnPoint.position, knifeSpawnPoint.rotation);
        animator.SetTrigger("ThrowKnife");
    }

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

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Invoke("DestroyGameObject", 5f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
