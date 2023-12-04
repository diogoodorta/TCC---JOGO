using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        // L�gica para anima��o de dano.
        GetComponent<Animator>().SetTrigger("Hurt");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // L�gica para anima��o de morte.
        GetComponent<Animator>().SetTrigger("Die");

        // Desativa o GameObject ap�s 2 segundos (ajuste conforme necess�rio).
        float delay = 2f;
        Invoke("DeactivateGameObject", delay);
    }

    private void DeactivateGameObject()
    {
        // Desativa o GameObject, tornando-o invis�vel e inativo no jogo.
        gameObject.SetActive(false);
    }
}
