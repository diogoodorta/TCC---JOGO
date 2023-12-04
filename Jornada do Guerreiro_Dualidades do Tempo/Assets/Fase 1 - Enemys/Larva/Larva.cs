using System.Collections;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public float moveSpeed;

    public int vidaMaxima = 3;
    private int vidaAtual;

    private Animator animator;

    private bool morrendo = false;
    private float cooldownParaMorte = 4f;
    private bool podeMorrer = true;
    

    private Vector3 direcaoMovimento = Vector3.right; // Direção inicial para a direita

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
        StartCoroutine(MovimentacaoLoop());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisão com jogador detectada pela Larva.");

            int danoDaLarva = 1;

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Chamando TakeDamageLarva no jogador.");
                playerHealth.TakeDamageLarva(danoDaLarva);
            }
        }
    }

    public void ForcarFlipEInverterDirecao()
    {
        // Inverte a direção e flipa a escala
        direcaoMovimento *= -1;
        Flip();
    }

    IEnumerator MovimentacaoLoop()
    {
        while (!morrendo)
        {
            // Move em direção à próxima posição
            transform.Translate(direcaoMovimento * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    IEnumerator MorrerComCooldown()
    {
        morrendo = true; // Marque a larva como morrendo
        animator.SetTrigger("morte"); // Inicie a animação de morte

        podeMorrer = false; // Impede que a morte seja chamada novamente enquanto está no cooldown

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Congela a posição antes de desativar os colisores
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Desativa todos os colisores da Larva
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(cooldownParaMorte);

        podeMorrer = true; // Permite chamar a morte novamente após o cooldown

        // Destrói o GameObject após o cooldown
        Destroy(gameObject);
    }


    public void TakeDamage(int amount)
    {
        if (!morrendo)
        {
            vidaAtual -= amount;
            Debug.Log("Larva recebeu dano: " + vidaAtual);

            if (vidaAtual <= 0 && podeMorrer)
            {
                StartCoroutine(MorrerComCooldown());
            }
        }
    }
}
