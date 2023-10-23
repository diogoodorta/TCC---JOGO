using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void ReceberDano(int damage)
    {
        
        vidaAtual -= damage;
        
        if(vidaAtual <= 0)
        {
           Die();
        }
      
    }

    void Die()
    {
       Debug.Log("o inimigo morreu!");
       Destroy(this.gameObject);
    }
}
