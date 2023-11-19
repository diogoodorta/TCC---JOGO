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
            // Lógica para detectar o jogador e seguir em sua direção.
            Vector2 direction = (player.position - transform.position).normalized;

            // Lógica para animação de caminhada.
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);

            // Atualiza a posição usando direction e speed.
            transform.Translate(direction * speed * Time.deltaTime);

            // Rotação do zumbi para enfrentar o jogador.
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDead)
        {
            // Se o zumbi colidir com o jogador, inicia o ataque.
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
            Attack();
        }
    }

    private void Attack()
    {
        // Lógica de ataque.
        player.GetComponent<Playerhealth>().ReceberDano(damage);
    }

    public void Die()
    {
        // Lógica para a morte.
        isDead = true;
        animator.SetTrigger("Die");

        // Desativa o GameObject após 5 segundos (ajuste conforme necessário).
        float delay = 5f;
        Invoke("DeactivateGameObject", delay);
    }

    private void DeactivateGameObject()
    {
        // Desativa o GameObject, tornando-o invisível e inativo no jogo.
        gameObject.SetActive(false);
    }
}
