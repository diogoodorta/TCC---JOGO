using UnityEngine;

public class Player : MonoBehaviour
{
    public float vida = 1; // Ajuste a vida inicial do jogador conforme necess�rio

    void Update()
    {
        // Adicione aqui a l�gica de movimento e outras a��es do jogador
    }

    public void ReceberDano(float quantidadeDeDano)
    {
        vida -= quantidadeDeDano;
        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        gameObject.SetActive(false);
    }
}