using System.Collections;
using UnityEngine;

public class Abelha : MonoBehaviour, IDamageable
{
    public int side = 1;
    public GameObject player;
    public Transform bulletPrefab;
    public Transform pivot;
    public float agroRange;
    public Transform castPoint;
    public float speed;
    public Animator animator;
    public float distanceToGround = 1f;
    public int health = 3;

    private bool canDestroy = false; // Flag para verificar se o GameObject pode ser destruído
    private bool canLaunchBullet = true;
    private bool isCooldown = false; // Flag para verificar o cooldown
    private bool isDying = false; // Flag para verificar se a abelha está morrendo
    private Transform groundCheck;
    private float timer;
    private Rigidbody2D rb;
    private bool IsFacingLeft;
    private bool isGrounded;
    private bool isDead = false;
    private Vector3 lastPosition; // Armazena a última posição da abelha

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck não encontrado. Certifique-se de criar um objeto vazio como filho da abelha e nomeá-lo como GroundCheck.");
        }

        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (isDead || isDying)
            return;

        timer += Time.deltaTime;
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < agroRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            FlipTowardsPlayer(player.transform.position.x);

            if (!isCooldown && timer > 3)
            {
                LaunchStinger(); // Novo método para lançar o Ferrão
                timer = 0;
                StartCoroutine(Cooldown());
            }

            if (timer < 1)
            {
                animator.SetBool("beAtacando", false);
            }

            if (timer > 2.8)
            {
                animator.SetBool("beAtacando", true);
            }
        }
        else
        {
            EncerrarAcoes();
        }

        if (!isGrounded)
        {
            rb.velocity = new Vector2(side * speed, rb.velocity.y);
        }
    }



    bool CanSeePlayer(float distance)
    {
        float castDist = distance;

        if (IsFacingLeft)
        {
            castDist = -distance;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));

        if (isGrounded)
        {
            rb.velocity = new Vector2(side * speed, 0f);
        }
    }

    // Novo método para lançar o Ferrão
    void LaunchStinger()
    {
        if (!isDead && canLaunchBullet && bulletPrefab != null && pivot != null)
        {
            Transform bullet = Instantiate(bulletPrefab, pivot.position, transform.rotation);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("MorrerTrigger");
        }

        EncerrarAcoes();

        lastPosition = transform.position; // Armazena a última posição antes de iniciar a destruição

        StartCoroutine(DelayedDestroy());
    }

    // Adicione esse método se você precisar interromper o processo de destruição
    public void AllowDestruction()
    {
        canDestroy = true;
    }

    IEnumerator DelayedDestroy()
    {
        // Espere até que a animação de morte seja concluída
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Agora a flag pode ser configurada para true, indicando que o GameObject pode ser destruído
        canDestroy = true;

        Debug.Log("Aguardando cooldown");
        yield return new WaitForSeconds(4f); // Tempo de cooldown após a morte (ajuste conforme necessário)

        // Verifique se a flag canDestroy está ativada (pode ser útil se algo interromper o processo)
        if (canDestroy)
        {
            Debug.Log("Destruição do GameObject");

            // Congela (freeze) a última posição antes de destruir o GameObject
            transform.position = lastPosition;

            // Garante que a rotação também seja congelada, se necessário
            transform.rotation = Quaternion.identity;

            Destroy(gameObject);
        }
    }

    IEnumerator MorrerCoroutine()
    {
        isDead = true; // Define a flag de morte como verdadeira
        animator.SetTrigger("MorrerTrigger"); // Ativar o trigger de morte

        // Aguarde o tempo da animação de morte
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("Método MorrerCoroutine chamado...");

        // Apenas desative a abelha ao invés de destruir o GameObject
        gameObject.SetActive(false);
    }

    IEnumerator DestruirAposAnimacao()
    {
        // Congela a posição da abelha durante a animação de morte
        rb.bodyType = RigidbodyType2D.Static;

        // Aguarde o tempo da animação de morte
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destrua o GameObject apenas se não estiver no Editor
        if (!Application.isEditor)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No Editor, o GameObject não será destruído após a animação.");
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(4f);
        isCooldown = false;
    }

    void EncerrarAcoes()
    {
        // Pare as animações da abelha
        if (animator != null)
        {
            animator.enabled = false; // Isso interromperá as animações
        }

        // Desative os colisores da abelha
        Collider2D[] coliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in coliders)
        {
            collider.enabled = false; // Isso desativará os colisores
        }

        // Pare de lançar Ferrao
        canLaunchBullet = false;

        // Interrompa todas as corrotinas em execução (se houver alguma)
        StopAllCoroutines();
    }

    void FlipTowardsPlayer(float playerX)
    {
        if (playerX > transform.position.x && !IsFacingLeft)
        {
            Flip();
        }
        else if (playerX < transform.position.x && IsFacingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        IsFacingLeft = !IsFacingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
