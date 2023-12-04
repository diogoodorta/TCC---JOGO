using UnityEngine;

public class Clerigo : MonoBehaviour
{
    public float vida = 22;
    public float cura = 10;
    public float dano = 1;
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

            if (estaMorto)
            {
                cooldownMorteTimer -= Time.deltaTime;

                if (cooldownMorteTimer <= 0)
                {
                    // Após o cooldown da animação de morte, destrua o objeto
                    Destroy(gameObject);
                }

                return;  // Não faça mais nada se o Clérigo estiver morto
            }
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
            animator.SetTrigger("Morte");
            estaMorto = true;

            // Inicie o cooldown para destruir o objeto após a animação
            cooldownMorteTimer = cooldownMorte;

            // Execute qualquer lógica adicional relacionada à morte do Clérigo
            Debug.Log("O Clérigo morreu!"); // Exemplo de mensagem de depuração

            
        }
    }
}


