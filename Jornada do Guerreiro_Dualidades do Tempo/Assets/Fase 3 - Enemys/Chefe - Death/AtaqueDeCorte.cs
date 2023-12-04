using UnityEngine;

public class AtaqueDeCorte : MonoBehaviour
{
    public int danoDoCorte = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ataque de Corte atingiu o jogador!");

            // Chame o método TakeDamage em vez de LevarDano
            Playerhealth playerHealth = other.GetComponent<Playerhealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(danoDoCorte);
                Debug.Log("Jogador sofreu " + danoDoCorte + " de dano. Vida restante: " + playerHealth.health);
            }
        }
    }
}