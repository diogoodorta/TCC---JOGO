using UnityEngine;

public class GhostController : MonoBehaviour, IDamageable
{
    public int maxHealth = 3;
    public float flyingSpeed = 5f;
    public float deathCooldown = 4f;
    public int damageOnCollision = 1;

    private int currentHealth;
    private Transform player;
    private Animator animator;
    private bool isCooldown = false;
    private bool isFacingLeft = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentHealth > 0 && player != null && !isCooldown)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += new Vector3(direction.x * flyingSpeed * Time.deltaTime, direction.y * flyingSpeed * Time.deltaTime, 0f);

            FlipTowardsPlayer(player.position.x);
        }
    }

    void FlipTowardsPlayer(float playerX)
    {
        if (playerX > transform.position.x && isFacingLeft)
        {
            Flip();
        }
        else if (playerX < transform.position.x && !isFacingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageOnCollision);
                Debug.Log("Fantasma causou dano ao jogador durante a colisão.");
            }
        }
    }

    void EncerrarAcoes()
    {
        // Adicione qualquer lógica adicional para encerrar as ações do fantasma aqui.
        Debug.Log("Fantasma: Ações encerradas após receber dano do jogador.");
    }

    System.Collections.IEnumerator DestroyAfterCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(deathCooldown);

        // Destruir o GameObject após o cooldown.
        Destroy(gameObject);
        Debug.Log("Fantasma: GameObject destruído após cooldown de morte.");
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Fantasma recebeu dano: " + amount);

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // Encerra as ações após receber dano.
            EncerrarAcoes();

            // Inicia o cooldown de morte.
            StartCoroutine(DestroyAfterCooldown());
        }
    }
}
