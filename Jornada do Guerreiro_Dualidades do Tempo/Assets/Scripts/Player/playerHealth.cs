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
            health = 0; // Garante que a vida não seja negativa
            Debug.Log("Player derrotado!");
            DestruirJogador();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Método TakeDamage chamado com dano: " + damage);
        SofrerDano(damage);
    }

    // Método para destruir o jogador
    void DestruirJogador()
    {
        // Adicione qualquer outra lógica desejada antes de destruir o jogador
        Debug.Log("Destruindo o jogador.");
        Destroy(gameObject);
    }
}