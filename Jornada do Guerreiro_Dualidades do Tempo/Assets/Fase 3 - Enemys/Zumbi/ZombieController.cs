using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 10;

    private Transform player;
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null && !isDead)
        {
            // L�gica para detectar o jogador e seguir em sua dire��o.
            Vector2 direction = (player.position - transform.position).normalized;

            // L�gica para anima��o de caminhada.
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);

            // Atualiza a posi��o usando direction e speed.
            transform.Translate(direction * speed * Time.deltaTime);

            // Rota��o do zumbi para enfrentar o jogador.
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);

                // Se o zumbi colidir com o jogador, inicia o ataque.
                Attack();
            }
        }
    }

    private void Attack()
    {
        // L�gica de ataque.
        player.GetComponent<PlayerHealth>().ReceberDano(damage);
    }

    public void Die()
    {
        // L�gica para a morte.
        isDead = true;
        animator.SetTrigger("morte");

        // Desativa o GameObject ap�s 5 segundos (ajuste conforme necess�rio).
        float delay = 5f;
        Invoke("DeactivateGameObject", delay);
    }

    private void DeactivateGameObject()
    {
        // Desativa o GameObject, tornando-o invis�vel e inativo no jogo.
        gameObject.SetActive(false);
    }
}
