using UnityEngine;

public class Player : MonoBehaviour
{
    public float vida = 1; // Ajuste a vida inicial do jogador conforme necessário

    void Update()
    {
        // Adicione aqui a lógica de movimento e outras ações do jogador
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