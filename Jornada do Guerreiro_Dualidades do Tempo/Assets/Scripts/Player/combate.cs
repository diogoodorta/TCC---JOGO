using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combate : MonoBehaviour
{
     
     public Animator animator;

     public Transform PontoDeAtaque;
     public LayerMask enemyLayers;

     public float ataqueRange = 0.5f;
     public int danoDeAtaque = 40;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Z))
       {
          Ataque();
       }
    }

    void Ataque()
    {
        //inicia a animação de ataque
        animator.SetTrigger("Ataque");

        //detecta os inimigos no range do ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontoDeAtaque.position, ataqueRange, enemyLayers);

        //calculo do dano
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().ReceberDano(danoDeAtaque);
        }

    }

    void OnDrawGizmosSelected()
    {   
        if(PontoDeAtaque == null)
        return;
        Gizmos.DrawWireSphere(PontoDeAtaque.position, ataqueRange);
    }
}
