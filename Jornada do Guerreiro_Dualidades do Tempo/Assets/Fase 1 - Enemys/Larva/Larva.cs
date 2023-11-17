using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public int vidaMaxima = 3; // Ajuste conforme necess�rio
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
                    Morrer(); // Inicie a anima��o de morte e destrua o GameObject
                }
            }
        }
    }

    // Fun��o para a larva morrer
    private void Morrer()
    {
        // Inicie a anima��o de morte ou qualquer l�gica necess�ria
        if (animator != null)
        {
            animator.SetTrigger("Morrer"); // Assumindo que voc� tenha uma trigger "Morrer" na sua anima��o
        }

        // Aguarde um tempo antes de destruir o GameObject (opcional)
        StartCoroutine(DestruirAposAnimacao());
    }

    IEnumerator DestruirAposAnimacao()
    {
        // Aguarde o tempo da anima��o de morte
        yield return new WaitForSeconds(2f); // Ajuste conforme necess�rio

        // Destrua o GameObject
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Coloque o restante do c�digo aqui
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
