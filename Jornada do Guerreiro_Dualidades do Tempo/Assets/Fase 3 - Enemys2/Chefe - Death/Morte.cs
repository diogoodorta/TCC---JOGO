using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Morte : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject prefabBolaDaMorte;
    public GameObject prefabFantasma;
    public float tempoRecargaBolaDaMorte = 5f;
    public float tempoRecargaLancarFantasma = 10f;

    public Transform pontoA;
    public Transform pontoB;
    public Transform pontoC;
    public Transform pontoE;
    public Transform pontoF;
    public float velocidadeMovimento = 2.0f;

    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private bool ataque;
    private bool voando;

    private Transform playerTransform;
    private int health;
    private bool bolaDaMorteDisponivel = true;
    private bool lancarFantasmaDisponivel = true;
    public Transform pontoDeOrigemBolaDaMorte;
    public Transform pontoDeOrigemLancarFantasma;



    void Start()
    {
        health = maxHealth;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Inicia a rotina de movimenta��o
        StartCoroutine(MovimentarEntrePontos());

        // Inicia os loops de ataques
        StartCoroutine(LoopAtaque(tempoRecargaBolaDaMorte, AtaqueBolaDaMorte, bolaDaMorteDisponivel));
        StartCoroutine(LoopAtaque(tempoRecargaLancarFantasma, LancarFantasma, lancarFantasmaDisponivel));
    }

    IEnumerator MovimentarEntrePontos()
    {
        while (true)
        {
            yield return MovimentarParaPonto(pontoB);
            yield return MovimentarParaPonto(pontoC);
            yield return MovimentarParaPonto(pontoE);
            yield return MovimentarParaPonto(pontoF);
        }
    }

    IEnumerator MovimentarParaPonto(Transform destino)
    {
        
        while (Vector2.Distance(transform.position, destino.position) > 0.1f)
        {   
            
            transform.position = Vector2.MoveTowards(transform.position, destino.position, velocidadeMovimento * Time.deltaTime);
            yield return null;
        }
    }

    void InstantiatePrefab(GameObject prefab, Vector3 position)
    {
        GameObject instanciado = Instantiate(prefab, position, Quaternion.identity);

        // Se necess�rio, fa�a algo com o objeto instanciado
    }

    IEnumerator LoopAtaque(float tempoRecarga, System.Action ataque, bool disponivelFlag)
    {
        while (true)
        {
            if (disponivelFlag)
            {
                ataque.Invoke();
                disponivelFlag = false;
                yield return new WaitForSeconds(tempoRecarga);
                disponivelFlag = true;
            }
            yield return null;
        }
    }

    void AtaqueBolaDaMorte()
    {
        Debug.Log("Ataque Bola da Morte chamado");
        InstantiatePrefab(prefabBolaDaMorte, pontoDeOrigemBolaDaMorte.position);
    }

    void LancarFantasma()
    {
        Debug.Log("Lan�ar Fantasma chamado");
        InstantiatePrefab(prefabFantasma, pontoDeOrigemLancarFantasma.position);
    }

    void PararTodasAcoes()
    {
        Debug.Log("O inimigo est� parando todas as a��es devido � sa�de baixa.");
        animator.SetTrigger("morte");

        // Agora, voc� pode destruir o GameObject ap�s um pequeno atraso (por exemplo, 2 segundos)
        StartCoroutine(DestruirAposDelay(6f));
    }

    IEnumerator DestruirAposDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destruir o GameObject ap�s o atraso
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        if (!isDead)
        {
            health -= amount;
            Debug.Log("Inimigo foi atingido! Dano: " + amount);

            if (health <= 0)
            {
                PararTodasAcoes();
            }
        }
    }
}
