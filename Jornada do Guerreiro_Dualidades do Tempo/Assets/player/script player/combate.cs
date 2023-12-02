using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public Animator animator;
    public bool isAttackable = true;
    public Transform PontoDeAtaque;
    public string enemyTag = "Enemy";
    public float ataqueRange = 0.5f;
    public int danoDeAtaque = 40;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Ataque();
        }
    }

    void Ataque()
    {
        Debug.Log("Ataque chamado");  // Adicione essa linha
        animator.SetTrigger("Ataque");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontoDeAtaque.position, ataqueRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                // Se o inimigo for a abelha, aplique dano
                Abelha abelha = enemy.GetComponent<Abelha>();
                if (abelha != null)
                {
                    abelha.TakeDamage(danoDeAtaque);
                    Debug.Log("Abelha atingida: " + abelha.name);
                }
            }

            // Adicione uma verificação para evitar chamar métodos em fantasmas diretamente
            GhostController ghost = enemy.GetComponent<GhostController>();
            if (ghost != null)
            {
                // Se o inimigo for um fantasma, aplique dano
                ghost.TakeDamage(danoDeAtaque);
                Debug.Log("Fantasma atingido: " + ghost.name);
            }

            // Adicione uma verificação para o BombadoController
            BombadoController bombado = enemy.GetComponent<BombadoController>();
            if (bombado != null)
            {
                // Se o inimigo for o bombado, aplique dano
                bombado.TakeDamage(danoDeAtaque);
                Debug.Log("Bombado atingido: " + bombado.name);
            }

            // Adicione uma verificação para a Larva
            Larva larva = enemy.GetComponent<Larva>();
            if (larva != null)
            {
                // Se o inimigo for a larva, aplique dano
                larva.TakeDamage(danoDeAtaque);
                Debug.Log("Larva atingida: " + larva.name);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (PontoDeAtaque == null)
            return;
        Gizmos.DrawWireSphere(PontoDeAtaque.position, ataqueRange);
    }
}
