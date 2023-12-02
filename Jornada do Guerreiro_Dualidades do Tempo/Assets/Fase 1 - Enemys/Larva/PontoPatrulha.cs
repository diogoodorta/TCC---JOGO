using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoPatrulha : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a colisão é com a Larva
        Larva larva = collision.gameObject.GetComponent<Larva>();
        if (larva != null)
        {
            // Força a Larva a flipar e mudar de direção
            larva.ForcarFlipEInverterDirecao();
        }
    }
}

