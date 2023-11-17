using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float flyingSpeed = 5f;
    public int damage = 10;

    private Transform player;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            // L�gica para detectar o jogador e voar em sua dire��o.
            Vector3 direction = (player.position - transform.position).normalized;

            // L�gica para anima��o de movimento.
            animator.SetBool("isFlying", true);
            animator.SetBool("isAttacking", false);

            // Atualiza a posi��o usando direction e flyingSpeed, mantendo o eixo Z constante.
            transform.position += new Vector3(direction.x * flyingSpeed * Time.deltaTime, direction.y * flyingSpeed * Time.deltaTime, 0f);

            // Rota��o do fantasma para enfrentar o jogador.
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Se o fantasma colidir com o jogador, inicia o ataque.
            animator.SetBool("isFlying", false);
            animator.SetBool("isAttacking", true);
        }
    }
}
