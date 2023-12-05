using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.C))
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

            
            if (enemy.CompareTag(enemyTag))
            {
               // Adicione verificações para diferentes tipos de inimigos aqui

               // Morte
               Morte morte = enemy.GetComponent<Morte>();
               if (morte != null)
               {
                   morte.TakeDamage(danoDeAtaque);
                   Debug.Log("Morte atingida: " + morte.name);
               }
            }

            if (enemy.CompareTag("Clérigo")) // Certifique-se de usar a tag correta do Clérigo
            {
                Clérigo clérigo = enemy.GetComponent<Clérigo>();
                if (clérigo != null)
                {
                    clérigo.TakeDamage(danoDeAtaque);

                    if (clérigo.vida <= 0)
                    {
                        // O Clérigo morreu, você pode adicionar lógica adicional aqui se necessário
                        Debug.Log("Clérigo morreu!");
                 
                    }
                }
            }

            // Adicione uma verificação para o ZombieController
            ZombieController zombie = enemy.GetComponent<ZombieController>();
            if (zombie != null)
            {
                // Se o inimigo for o zumbi, aplique dano
                zombie.TakeDamage(danoDeAtaque);
                Debug.Log("Zumbi atingido: " + zombie.name);
            }

            // Adicione uma verifica��o para evitar chamar m�todos em fantasmas diretamente
            GhostController ghost = enemy.GetComponent<GhostController>();
            if (ghost != null)
            {
                // Se o inimigo for um fantasma, aplique dano
                ghost.TakeDamage(danoDeAtaque);
                Debug.Log("Fantasma atingido: " + ghost.name);
            }

            // Adicione uma verifica��o para o BombadoController
            BombadoController bombado = enemy.GetComponent<BombadoController>();
            if (bombado != null)
            {
                // Se o inimigo for o bombado, aplique dano
                bombado.TakeDamage(danoDeAtaque);
                Debug.Log("Bombado atingido: " + bombado.name);
            }

            // Adicione uma verifica��o para a Larva
            Larva larva = enemy.GetComponent<Larva>();
            if (larva != null)
            {
                // Se o inimigo for a larva, aplique dano
                larva.TakeDamage(danoDeAtaque);
                Debug.Log("Larva atingida: " + larva.name);
            }

            if (enemy.CompareTag(enemyTag))
            {
                // Se o inimigo for SoldadoPossuido, aplique dano
                SoldadoPossuido soldado = enemy.GetComponent<SoldadoPossuido>();
                if (soldado != null)
                {
                    soldado.ReceberDano(danoDeAtaque);
                    Debug.Log("SoldadoPossuido atingido: " + soldado.name);
                }
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


    
    
