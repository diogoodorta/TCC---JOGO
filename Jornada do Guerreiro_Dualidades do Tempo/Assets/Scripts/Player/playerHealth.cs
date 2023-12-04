using UnityEngine;

public class Playerhealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 1;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int quantidade)
    {
        health -= quantidade;
        Debug.Log("Player recebeu " + quantidade + " de dano. Vida restante: " + health);

        if (health <= 0)
        {
            health = 0; // Garante que a vida n�o seja negativa
            Debug.Log("Player derrotado!");
            DestruirJogador();
        }
    }

    // Remova esse m�todo se n�o estiver usando
    void DestruirJogador()
    {
        // Adicione qualquer outra l�gica desejada antes de destruir o jogador
        Debug.Log("Destruindo o jogador.");
        Destroy(gameObject);
    }
}
