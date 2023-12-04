using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoPatrulha : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a colis�o � com a Larva
        Larva larva = collision.gameObject.GetComponent<Larva>();
        if (larva != null)
        {
            // For�a a Larva a flipar e mudar de dire��o
            larva.ForcarFlipEInverterDirecao();
        }
    }
}

