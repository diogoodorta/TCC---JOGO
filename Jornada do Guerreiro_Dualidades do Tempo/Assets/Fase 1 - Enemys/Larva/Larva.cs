using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public int vidaMaxima = 3; // Ajuste conforme necessário
    private int vidaAtual;

    private Animator animator;


    void Start()
    {
        vidaAtual = vidaMaxima;
        vidaAtual -= playerHealth.health;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Playerhealth playerHealth = collision.gameObject.GetComponent<Playerhealth>();

            if (playerHealth != null)
            {
                // Recebe dano do player
                vidaAtual -= playerHealth.GetCurrentHealth();

                // Verifica se a vida chegou a zero
                if (vidaAtual <= 0)
                {
                    Morrer(); // Inicie a animação de morte e destrua o GameObject
                }
            }
        }
    }

    // Função para a larva morrer
    private void Morrer()
    {
        // Inicie a animação de morte ou qualquer lógica necessária
        if (animator != null)
        {
            animator.SetTrigger("Morrer"); // Assumindo que você tenha uma trigger "Morrer" na sua animação
        }

        // Aguarde um tempo antes de destruir o GameObject (opcional)
        StartCoroutine(DestruirAposAnimacao());
    }

    IEnumerator DestruirAposAnimacao()
    {
        // Aguarde o tempo da animação de morte
        yield return new WaitForSeconds(2f); // Ajuste conforme necessário

        // Destrua o GameObject
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Coloque o restante do código aqui
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
