using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public Animator animator;
    public bool isAttackable = true;
    public Transform PontoDeAtaque;
    public string bossTag = "Boss"; // Altere para a tag correta do Boss
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
        Debug.Log("Ataque chamado");
        animator.SetTrigger("Ataque");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontoDeAtaque.position, ataqueRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(bossTag))
            {
                // Verifique se o componente IDamable está presente no BossMorte
                BossMorte boss = enemy.GetComponent<BossMorte>();
                if (boss != null)
                {
                    boss.SofrerDano(danoDeAtaque);  // Correção aqui: Troque TakeDamage por SofrerDano
                    Debug.Log("Boss atingido: " + boss.name);
                }
                else
                {
                    Debug.LogError("Componente BossMorte não encontrado no Boss.");
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
