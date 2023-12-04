using UnityEngine;

public interface IDamable
{
    void SofrerDano(int dano);
    void TakeDamage(int damage);
}

public class Playerhealth : MonoBehaviour, IDamable
{
    public int health;
    public int maxHealth = 1;

    void Start()
    {
        health = maxHealth;
    }

    public void SofrerDano(int quantidade)
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

    public void TakeDamage(int damage)
    {
        Debug.Log("M�todo TakeDamage chamado com dano: " + damage);
        SofrerDano(damage);
    }

    // M�todo para destruir o jogador
    void DestruirJogador()
    {
        // Adicione qualquer outra l�gica desejada antes de destruir o jogador
        Debug.Log("Destruindo o jogador.");
        Destroy(gameObject);
    }
}