using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Lógica para animação de dano.
        GetComponent<Animator>().SetTrigger("Hurt");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica para animação de morte.
        GetComponent<Animator>().SetTrigger("Die");

        // Desativa o GameObject após 2 segundos (ajuste conforme necessário).
        float delay = 2f;
        Invoke("DeactivateGameObject", delay);
    }

    private void DeactivateGameObject()
    {
        // Desativa o GameObject, tornando-o invisível e inativo no jogo.
        gameObject.SetActive(false);
    }
}
