using UnityEngine;
using System.Collections;

public class Clérigo : MonoBehaviour
{
    public float vidaMaxima = 20;
    public float vida = 10;
    public float cura = 2;
    public float dano = 1;
    public PlayerHealth playerHealth;
    public float tempoParaDestruir = 3.0f;
    public float deteccaoDistancia = 15f;
    public float projétilVelocidade = 12f;
    public float cooldown = 1.5f;
    public GameObject projétilDeCuraPrefab;

    private Animator animator;
    private bool estaMorto = false;
    private bool lancandoProjétil = false;
    private float cooldownTimer = 1f;
    private float cooldownMorte = 2f;
    private float cooldownMorteTimer = 0f;
    public float cooldownDestruição = 2.0f; // Tempo de cooldown após a animação de morte
    private bool destruiçãoAgendada = false;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!estaMorto)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                DetectarAliadosEmPerigo();

                if (alvo != null)
                {
                    animator.SetTrigger("lançar");
                    LancarProjétilDeCura();
                    cooldownTimer = cooldown;
                }
                else
                {
                    animator.SetTrigger("respirar");
                }

                if (lancandoProjétil && !animator.GetCurrentAnimatorStateInfo(0).IsName("LancandoProjétil"))
                {
                    lancandoProjétil = false;
                }
            }

            if (vida <= 0)
            {
                tempoParaDestruir -= Time.deltaTime;

                if (tempoParaDestruir <= 0)
                {
                    Morrer();
                }
            }
        }
    }

    void DestruirGameObject()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
        }
    }

    public void TerminarLancamentoDeProjétil()
    {
        // Lógica após a animação de lançamento do projétil, se necessário
    }

    private Transform alvo;

    void DetectarAliadosEmPerigo()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, deteccaoDistancia);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ally"))
            {
                SoldadoPossuido soldade = collider.GetComponent<SoldadoPossuido>();
                if (soldade != null && soldade.vida < soldade.vidaMaxima)
                {
                    alvo = soldade.transform;
                    Debug.Log("Clérigo detectou aliado em perigo!");
                    Debug.Log("Vida do aliado: " + soldade.vida);
                    break;
                }
            }
            else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                SoldadoPossuido soldado = collider.GetComponent<SoldadoPossuido>();
                if (soldado != null && soldado.vida < 10)
                {
                    alvo = soldado.transform;
                    Debug.Log("Clérigo detectou SoldadoPossuído em perigo!");
                    Debug.Log("Vida do SoldadoPossuído: " + soldado.vida);
                    break;
                }
            }
        }
    }

    void LancarProjétilDeCura()
    {
        Vector3 direcao = alvo.position - transform.position;
        direcao.Normalize();

        GameObject projétil = Instantiate(projétilDeCuraPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projétil.GetComponent<Rigidbody2D>();
        rb.velocity = direcao * projétilVelocidade;
        Debug.Log("Clérigo lançou um projétil de cura!");

        Invoke("TerminarCura", 2f);
    }

    void TerminarCura()
    {
        Debug.Log("Cura terminou.");
        alvo = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                int danoInt = Mathf.RoundToInt(dano);
                player.ReceberDano(danoInt);
                Debug.Log("Clérigo causou dano ao jogador!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // Adicione uma verificação de tipo aqui
            if (other.TryGetComponent<SoldadoPossuido>(out SoldadoPossuido soldade))
            {
                if (soldade.vida < soldade.vidaMaxima)  // Adicionei esta condição para curar apenas se a vida estiver abaixo da vida máxima
                {
                    soldade.ReceberCura(cura);
                    Debug.Log("Clérigo curou um Aliado!");
                }
            }
            else if (other.TryGetComponent<SoldadoPossuido>(out SoldadoPossuido soldado))
            {
                if (soldado.vida < soldado.vidaMaxima)  // Adicionei esta condição para curar apenas se a vida estiver abaixo da vida máxima
                {
                    soldado.ReceberCura(cura);
                    Debug.Log("Clérigo curou um Soldado!");
                }
            }
        }
    }

    public void ReceberCura(float quantidadeDeCura)
    {
        vida = Mathf.Min(vida + quantidadeDeCura, vidaMaxima); // Garante que a vida não ultrapasse o máximo
        // Adicione outras lógicas relacionadas à cura, se necessário.
    }

    public void TakeDamage(float amount)
    {
        if (!estaMorto)
        {
            vida -= amount;
            Debug.Log("Clérigo foi atingido! Dano: " + amount);

            if (vida <= 0)
            {
                Morrer();
            }
        }
    }

    void Morrer()
    {
        if (!estaMorto)
        {
            animator.SetTrigger("Morte");
            estaMorto = true;
            cooldownMorteTimer = cooldownMorte;
            Debug.Log("O Clérigo morreu!");

            if (!destruiçãoAgendada)
            {
                StartCoroutine(ExecutarCooldownDestruição());
                destruiçãoAgendada = true;
            }
        }
    }

    IEnumerator ExecutarCooldownDestruição()
    {
        yield return new WaitForSeconds(cooldownDestruição);

        DestruirGameObject();
    }
}
