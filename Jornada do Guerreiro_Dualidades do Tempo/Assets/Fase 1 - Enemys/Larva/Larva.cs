using System.Collections;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public int vidaMaxima = 3;
    private int vidaAtual;

    private Animator animator;
    private bool morrendo = false;
    private float tempoParaDestruirInicial = 3f;
    private float tempoParaDestruir; // Usaremos essa variável para controlar o tempo restante

    void Start()
    {
        vidaAtual = vidaMaxima;
        animator = GetComponent<Animator>();
        tempoParaDestruir = tempoParaDestruirInicial;

        // Inicie o tempo de destruição quando o objeto é instanciado
        StartCoroutine(DestruirAposTempo());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Obtenha a referência do script Playerhealth
            Playerhealth playerHealth = collision.gameObject.GetComponent<Playerhealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);

                // Reduza a vida da larva
                vidaAtual--;

                if (vidaAtual <= 0)
                {
                    // Se a vida da larva chegar a 0, inicie a animação de morte
                    Morrer();
                }
            }
        }
    }

    IEnumerator DestruirAposTempo()
    {
        while (tempoParaDestruir > 0)
        {
            // Aguarde um pequeno intervalo
            yield return new WaitForSeconds(0.1f);

            // Reduza o tempo de destruição
            tempoParaDestruir -= 0.1f;
        }

        // Inicie a animação de morte e destrua o GameObject
        Morrer();
    }

    private void Morrer()
    {
        if (!morrendo && animator != null)
        {
            morrendo = true; // Marque a larva como morrendo
            animator.SetTrigger("Morrer"); // Assumindo que você tenha uma trigger "Morrer" na sua animação
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!morrendo) // Verifique se a larva não está morrendo
        {
            // Coloque o restante do código aqui
            if (vidaAtual <= 0)
            {
            }
        }

        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                transform.localScale = new Vector3(3, 3, 3);
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                transform.localScale = new Vector3(-3, 3, 3);
                patrolDestination = 0;
            }
        }
    }
}
