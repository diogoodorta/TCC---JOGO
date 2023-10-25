using UnityEngine;

public class Clerigo : MonoBehaviour
{
    public float vida = 22;
    public float cura = 10;
    public float dano = 1;
    public Playerhealth playerHealth;
    public float tempoParaDestruir = 3.0f;
    public float deteccaoDistancia = 15f;
    public float projétilVelocidade = 12f;
    public float cooldown = 1.5f;
    public GameObject projétilDeCuraPrefab;

    private Animator animator;


    private bool estaMorto = false;
    private bool lancandoProjétil = false;
    private float cooldownTimer = 1f;
    private float cooldownMorte = 2f;  // Cooldown para destruir o objeto após a animação de morte
    private float cooldownMorteTimer = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!estaMorto) // Verifique se o Clérigo não está morto
        {
            vida -= Time.deltaTime; // Reduz a vida ao longo do tempo.

            if (vida <= 0)
            {
                // Inicia a contagem regressiva para destruição
                tempoParaDestruir -= Time.deltaTime;

                if (tempoParaDestruir <= 0)
                {
                    // Destrua o GameObject após o tempo definido
                    Morrer(); // Chame a função de morte em vez de Destroy(gameObject)
                }
            }
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            DetectarAliadosEmPerigo();

            if (alvo != null)
            {
                // Defina o gatilho da animação para "LancandoProjétil"
                animator.SetTrigger("LancandoProjétil");
                LancarProjétilDeCura();
                cooldownTimer = cooldown;
            }

            // Verifique se a animação de lançamento do projétil está em andamento
            if (lancandoProjétil && !animator.GetCurrentAnimatorStateInfo(0).IsName("LancandoProjétil"))
            {
                lancandoProjétil = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(1);
        }


    }


    // Método chamado quando a animação de lançamento do projétil termina
    public void TerminarLancamentoDeProjétil()
    {
        // Posso colocar:
        // A lógica após a animação de lançamento do projétil
        // Por exemplo, criar o projétil aqui, se necessário
    }



    private Transform alvo;

    void DetectarAliadosEmPerigo()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, deteccaoDistancia);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ally"))
            {
                Aliado aliado = collider.GetComponent<Aliado>();
                if (aliado != null && aliado.vida < 20)
                {
                    alvo = aliado.transform;
                    Debug.Log("Clérigo detectou aliado em perigo!");
                    Debug.Log("Vida do aliado: " + aliado.vida);
                    break;
                }
            }
        }
    }


    void LancarProjétilDeCura()
    {
        Vector3 direcao = alvo.position - transform.position;
        direcao.Normalize();

        // Cria um projétil de cura
        GameObject projétil = Instantiate(projétilDeCuraPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projétil.GetComponent<Rigidbody2D>();
        rb.velocity = direcao * projétilVelocidade;
        Debug.Log("Clérigo lançou um projétil de cura!");

        Invoke("TerminarCura", 2f);  // Termina o processo de cura após 2 segundos
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
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ReceberDano(dano);
                Debug.Log("Clérigo causou dano ao jogador!");
            }
        }
    }


    void Morrer()
    {
        if (!estaMorto)
        {
            // Ative a animação de morte no Animator
            animator.SetBool("Morte",true);
            estaMorto = true;

            // Inicie o cooldown para destruir o objeto após a animação
            cooldownMorteTimer = cooldownMorte;

            // Execute qualquer lógica adicional relacionada à morte do Clérigo
            Debug.Log("O Clérigo morreu!"); // Exemplo de mensagem de depuração

            
        }
    }
}


